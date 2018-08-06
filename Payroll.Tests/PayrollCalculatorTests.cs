using System;
using Payroll.Tests.Builders.Sut;
using Xunit;

namespace Payroll.Tests
{
    public class PayrollCalculatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CalculatePayroll_LessThanOrEqualZeroTimecardHours_ShouldReturnZero(int hours)
        {
            // arrange
            var sut = new PayrollCalculatorSutBuilder().WithTimeCardHours(hours).Build();

            // act
            double payroll = sut.CalculatePayroll(DateTime.Now, DateTime.Now.AddDays(7), "1");

            // assert
            Assert.Equal(0, payroll);
        }

        [Theory]
        [InlineData(0.2, 40, 40, false, 1280)]
        [InlineData(0.15, 7.25, 20, false, 123.25)]
        [InlineData(0.30, 60, 50, true, 2170)]
        [InlineData(0, 10, 40, true, 500)]
        public void CalculatePayroll_ValidInputData_ShouldProduceCorrectPayroll(double taxRate, 
            double hourlyRate, int timeCardHours, bool seniority, double payroll)
        {
            // arrange
            var sut = new PayrollCalculatorSutBuilder()
                .WithTaxBracketTaxRate(taxRate)
                .WithEmployeeHourlyRate(hourlyRate)
                .WithEmployeeSeniority(seniority)
                .WithTimeCardHours(timeCardHours).Build();

            // act
            double amount = sut.CalculatePayroll(DateTime.Now, DateTime.Now.AddDays(7), "1");

            // assert
            Assert.Equal(payroll, amount);
        }
    }
}
