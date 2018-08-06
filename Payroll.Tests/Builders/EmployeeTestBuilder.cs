using Payroll.Services.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Tests.Builders
{
    public class EmployeeTestBuilder
    {
        private Employee _employee;

        public EmployeeTestBuilder()
        {
            _employee = new Employee
            {
                Id = "1",
                Name = "Test Employee",
                HourlyRate = 40,
                Seniority = false
            };
        }

        public EmployeeTestBuilder WithHourlyRate(double rate)
        {
            _employee.HourlyRate = rate;
            return this;
        }

        public EmployeeTestBuilder WithSeniority(bool seniority)
        {
            _employee.Seniority = seniority;
            return this;
        }

        public Employee Build()
        {
            return _employee;
        }
    }
}
