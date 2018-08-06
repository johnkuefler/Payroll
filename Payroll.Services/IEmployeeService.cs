using System;
using System.Collections.Generic;
using System.Text;
using Payroll.Services.POCO;

namespace Payroll.Services
{
    public interface IEmployeeService
    {
        Employee GetById(string id);
    }
}
