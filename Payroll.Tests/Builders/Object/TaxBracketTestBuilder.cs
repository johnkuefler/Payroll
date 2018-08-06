using Payroll.Services.POCO;

namespace Payroll.Tests.Builders.Object
{
    public class TaxBracketTestBuilder
    {
        TaxBracket taxBracket;

        public TaxBracketTestBuilder()
        {
            taxBracket = new TaxBracket  
            {
               TaxRate = 0
            };
        }

        public TaxBracketTestBuilder WithTaxRate(double taxRate)
        {
            taxBracket.TaxRate = taxRate;
            return this;
        }

      
        public TaxBracket Build()
        {
            return taxBracket;
        }
    }
}
