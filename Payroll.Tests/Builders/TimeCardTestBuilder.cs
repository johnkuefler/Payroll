using System;
using System.Collections.Generic;
using System.Text;
using Payroll.Services.POCO;

namespace Payroll.Tests.Builders
{
    public class TimeCardTestBuilder
    {
        private TimeCard _timeCard;

        public TimeCardTestBuilder()
        {
            _timeCard = new TimeCard()
            {
                TotalHours = 40,
            };
        }

        public TimeCardTestBuilder WithTotalHours(int totalHours)
        {
            _timeCard.TotalHours = totalHours;
            return this;
        }

        public TimeCard Build()
        {
            return _timeCard;
        }
    }
}
