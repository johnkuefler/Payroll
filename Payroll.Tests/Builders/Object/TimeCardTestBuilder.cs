using Payroll.Services.POCO;

namespace Payroll.Tests.Builders.Object
{
    public class TimeCardTestBuilder
    {
        TimeCard timeCard;

        public TimeCardTestBuilder()
        {
            timeCard = new TimeCard()
            {
                TotalHours = 40,
            };
        }

        public TimeCardTestBuilder WithTotalHours(int totalHours)
        {
            timeCard.TotalHours = totalHours;
            return this;
        }

        public TimeCard Build()
        {
            return timeCard;
        }
    }
}
