using Payroll.Services;
using System;
using System.Threading;
using Payroll.Services.POCO;

namespace Payroll.Core
{
    public class PayrollCalculator
    {
        private readonly ITimeCardService _timeCardService;
        private readonly IEmployeeService _emplyeeService;
        private readonly ITaxService _taxService;

        public PayrollCalculator(ITimeCardService timeCardService, IEmployeeService employeeService,
            ITaxService taxService)
        {
            _timeCardService = timeCardService;
            _emplyeeService = employeeService;
            _taxService = taxService;
        }

        public double CalculatePayroll(DateTime startDate, DateTime endDate, string employeeId)
        {
            Employee employee = _emplyeeService.GetById(employeeId);

            TimeCard timeCard = _timeCardService.GetEmployeeTimeCard(employeeId, startDate, endDate);

            TaxBracket taxBracket = _taxService.GetTaxBracket(employee.HourlyRate);

            double bonus = 0;
            if (employee.Seniority)
            {
                bonus = 100;
            }

            double payPeriodBasePay = employee.HourlyRate * timeCard.TotalHours;

            return (payPeriodBasePay + bonus) * (1 - taxBracket.TaxRate);
        }
    }
}