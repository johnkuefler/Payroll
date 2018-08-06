using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Services.POCO
{
    public class Employee
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public bool Seniority { get; set; }
        public double HourlyRate { get; set; }
    }
}
