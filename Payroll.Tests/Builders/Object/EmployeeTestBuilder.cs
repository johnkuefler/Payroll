using Payroll.Services.POCO;

namespace Payroll.Tests.Builders.Object
{
    public class EmployeeTestBuilder
    {
        Employee employee;

        public EmployeeTestBuilder()
        {
            employee = new Employee
            {
                Id = "1",
                Name = "Test Employee",
                HourlyRate = 40,
                Seniority = false
            };
        }

        public EmployeeTestBuilder WithHourlyRate(double rate)
        {
            employee.HourlyRate = rate;
            return this;
        }

        public EmployeeTestBuilder WithSeniority(bool seniority)
        {
            employee.Seniority = seniority;
            return this;
        }

        public Employee Build()
        {
            return employee;
        }
    }
}
