using System;
using Moq;
using Payroll.Core;
using Payroll.Services;
using Payroll.Tests.Builders.Object;
using Xunit;

namespace Payroll.Tests
{
    public class PayrollCalculatorTests
    {
        [Fact]
        public void CalculatePayroll_ZeroHours_ShouldReturnZero()
        {
            // arrange
            Mock<ITaxService> taxServiceMock = new Mock<ITaxService>();
            Mock<IEmployeeService> employeeServiceMock = new Mock<IEmployeeService>();
            Mock<ITimeCardService> timeCardServiceMock = new Mock<ITimeCardService>();

            taxServiceMock.Setup(x => x.GetTaxBracket(It.IsAny<double>())).Returns(new TaxBracketTestBuilder().Build());

            employeeServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(new EmployeeTestBuilder().Build());

            timeCardServiceMock
                .Setup(x => x.GetEmployeeTimeCard(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(new TimeCardTestBuilder().WithTotalHours(0).Build());

            var sut = new PayrollCalculator(timeCardServiceMock.Object, employeeServiceMock.Object,
                taxServiceMock.Object);

            // act
            double payroll = sut.CalculatePayroll(DateTime.Now, DateTime.Now.AddDays(7), "1");

            // assert
            Assert.Equal(0, payroll);
        }


        [Theory]
        [InlineData(0.2, 40, 40, false, 1280)]
        [InlineData(0.15, 7.25, 20, false, 123.25)]
        [InlineData(0.30, 60, 50, true, 2170)]
        [InlineData(0, 10, 40, true, 500)]
        public void CalculatePayroll_ValidInputData_ShouldProduceCorrectPayroll(double taxRate, 
            double hourlyRate, int timeCardHours, bool seniority, double payroll)
        {
            // arrange
            Mock<ITaxService> taxServiceMock = new Mock<ITaxService>();
            Mock<IEmployeeService> employeeServiceMock = new Mock<IEmployeeService>();
            Mock<ITimeCardService> timeCardServiceMock = new Mock<ITimeCardService>();

            taxServiceMock.Setup(x => x.GetTaxBracket(It.IsAny<double>()))
                .Returns(new TaxBracketTestBuilder().WithTaxRate(taxRate).Build());

            employeeServiceMock.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(new EmployeeTestBuilder().WithHourlyRate(hourlyRate).WithSeniority(seniority).Build());

            timeCardServiceMock
                .Setup(x => x.GetEmployeeTimeCard(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(new TimeCardTestBuilder().WithTotalHours(timeCardHours).Build());

            var sut = new PayrollCalculator(timeCardServiceMock.Object, employeeServiceMock.Object,
                taxServiceMock.Object);
        
            // act
            double amount = sut.CalculatePayroll(DateTime.Now, DateTime.Now.AddDays(7), "1");

            // assert
            Assert.Equal(payroll, amount);
        }


        [Fact]
        public void CalculatePayroll_NegativeHours_ShouldReturnZero()
        {
            // arrange
            Mock<ITaxService> taxServiceMock = new Mock<ITaxService>();
            Mock<IEmployeeService> employeeServiceMock = new Mock<IEmployeeService>();
            Mock<ITimeCardService> timeCardServiceMock = new Mock<ITimeCardService>();

            taxServiceMock.Setup(x => x.GetTaxBracket(It.IsAny<double>()))
                .Returns(new TaxBracketTestBuilder().Build());

            employeeServiceMock.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(new EmployeeTestBuilder().Build());

            timeCardServiceMock
                .Setup(x => x.GetEmployeeTimeCard(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(new TimeCardTestBuilder().WithTotalHours(-1).Build());

            var sut = new PayrollCalculator(timeCardServiceMock.Object, employeeServiceMock.Object,
                taxServiceMock.Object);

            // act
            double amount = sut.CalculatePayroll(DateTime.Now, DateTime.Now.AddDays(7), "1");

            // assert
            Assert.Equal(0, amount);
        }
    }
}
