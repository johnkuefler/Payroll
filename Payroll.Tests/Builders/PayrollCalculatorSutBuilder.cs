using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Payroll.Core;
using Payroll.Services;
using Payroll.Services.POCO;

namespace Payroll.Tests.Builders
{
    public class PayrollCalculatorSutBuilder
    {
        readonly Mock<ITaxService> _taxServiceMock;
        readonly Mock<IEmployeeService> _employeeServiceMock;
        readonly Mock<ITimeCardService> _timeCardServiceMock;

        private readonly TaxBracket _taxBracket;
        private readonly Employee _employee;
        private readonly TimeCard _timeCard;

        public PayrollCalculatorSutBuilder()
        {
            _taxServiceMock = new Mock<ITaxService>();
            _employeeServiceMock = new Mock<IEmployeeService>();
            _timeCardServiceMock = new Mock<ITimeCardService>();

            _taxBracket = new TaxBracket
            {
                TaxRate = 0.2
            };

            _employee = new Employee
            {
                HourlyRate = 25,
                Seniority = false
            };

            _timeCard = new TimeCard
            {
                TotalHours = 40
            };
        }

        public PayrollCalculatorSutBuilder WithTaxBracketTaxRate(double taxRate)
        {
            _taxBracket.TaxRate = taxRate;
            return this;
        }

        public PayrollCalculatorSutBuilder WithEmployeeHourlyRate(double rate)
        {
            _employee.HourlyRate = rate;
            return this;
        }

        public PayrollCalculatorSutBuilder WithEmployeeSeniority(bool seniority)
        {
            _employee.Seniority = seniority;
            return this;
        }

        public PayrollCalculatorSutBuilder WithTimeCardHours(int hours)
        {
            _timeCard.TotalHours = hours;
            return this;
        }


        public PayrollCalculator Build()
        {
            _taxServiceMock.Setup(x => x.GetTaxBracket(It.IsAny<double>())).Returns(_taxBracket);
            _employeeServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(_employee);
            _timeCardServiceMock.Setup(x => x.GetEmployeeTimeCard(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(_timeCard);

            return new PayrollCalculator(_timeCardServiceMock.Object, _employeeServiceMock.Object,
                _taxServiceMock.Object);
        }
    }
}
