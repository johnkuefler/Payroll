using System;
using Moq;
using Payroll.Core;
using Payroll.Services;
using Payroll.Services.POCO;
using Xunit;

namespace Payroll.Tests
{
    public class PayrollCalculatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CalculatePayroll_LessThanOrEqualZeroTimeCardHours_ShouldReturnZero(int hours)
        {
            // arrange
            TaxBracket taxBracket = new TaxBracket
            {
                TaxRate = 0.2
            };

            Employee employee = new Employee
            {
                Seniority = false,
                HourlyRate = 25
            };

            TimeCard timeCard = new TimeCard
            {
                TotalHours = hours
            };

            Mock<ITaxService> taxServiceMock = new Mock<ITaxService>();
            Mock<IEmployeeService> employeeServiceMock = new Mock<IEmployeeService>();
            Mock<ITimeCardService> timeCardServiceMock = new Mock<ITimeCardService>();

            taxServiceMock.Setup(x => x.GetTaxBracket(It.IsAny<double>())).Returns(taxBracket);
            employeeServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(employee);
            timeCardServiceMock
                .Setup(x => x.GetEmployeeTimeCard(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(timeCard);

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
            TaxBracket taxBracket = new TaxBracket
            {
                TaxRate = taxRate
            };

            Employee employee = new Employee
            {
                Seniority = seniority,
                HourlyRate = hourlyRate
            };

            TimeCard timeCard = new TimeCard
            {
                TotalHours = timeCardHours
            };

            Mock<ITaxService> taxServiceMock = new Mock<ITaxService>();
            Mock<IEmployeeService> employeeServiceMock = new Mock<IEmployeeService>();
            Mock<ITimeCardService> timeCardServiceMock = new Mock<ITimeCardService>();

            taxServiceMock.Setup(x => x.GetTaxBracket(It.IsAny<double>())).Returns(taxBracket);
            employeeServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(employee);
            timeCardServiceMock
                .Setup(x => x.GetEmployeeTimeCard(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(timeCard);

            var sut = new PayrollCalculator(timeCardServiceMock.Object, employeeServiceMock.Object,
                taxServiceMock.Object);

            // act
            double amount = sut.CalculatePayroll(DateTime.Now, DateTime.Now.AddDays(7), "1");

            // assert
            Assert.Equal(payroll, amount);
        }
    }
}
