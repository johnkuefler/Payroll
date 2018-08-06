using System;
using Payroll.Services.POCO;

namespace Payroll.Services
{
    public interface ITimeCardService
    {
        TimeCard GetEmployeeTimeCard(string employeeId, DateTime startDate, DateTime endDate);
    }
}
