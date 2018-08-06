using System;
using System.Diagnostics.CodeAnalysis;
using Moq;
using Payroll.Core;
using Payroll.Services;
using Payroll.Services.POCO;

namespace Payroll.Tests.Builders.Sut
{
    public class PayrollCalculatorSutBuilder
    {
        Mock<ITaxService> taxServiceMock;
        Mock<IEmployeeService> employeeServiceMock;
        Mock<ITimeCardService> timeCardServiceMock;

        TaxBracket taxBracket;
        Employee employee;
        TimeCard timeCard;

        public PayrollCalculatorSutBuilder()
        {
            taxServiceMock = new Mock<ITaxService>();
            employeeServiceMock = new Mock<IEmployeeService>();
            timeCardServiceMock = new Mock<ITimeCardService>();

            taxBracket = new TaxBracket
            {
                TaxRate = 0
            };

            employee = new Employee
            {
                HourlyRate = 0,
                Seniority = false
            };

            timeCard = new TimeCard
            {
                TotalHours = 0
            };
        }

        public PayrollCalculatorSutBuilder WithTaxBracketTaxRate(double taxRate)
        {
            taxBracket.TaxRate = taxRate;
            return this;
        }

        public PayrollCalculatorSutBuilder WithEmployeeHourlyRate(double rate)
        {
            employee.HourlyRate = rate;
            return this;
        }

        public PayrollCalculatorSutBuilder WithEmployeeSeniority(bool seniority)
        {
            employee.Seniority = seniority;
            return this;
        }

        public PayrollCalculatorSutBuilder WithTimeCardHours(int hours)
        {
            timeCard.TotalHours = hours;
            return this;
        }

        public PayrollCalculator Build()
        {
            taxServiceMock.Setup(x => x.GetTaxBracket(It.IsAny<double>())).Returns(taxBracket);
            employeeServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(employee);
            timeCardServiceMock.Setup(x => x.GetEmployeeTimeCard(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(timeCard);

            return new PayrollCalculator(timeCardServiceMock.Object, employeeServiceMock.Object,
                taxServiceMock.Object);
        }
    }
}
