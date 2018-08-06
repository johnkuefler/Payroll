using System;
using System.Collections.Generic;
using System.Text;
using Payroll.Services.POCO;

namespace Payroll.Tests.Builders
{
    public class TaxBracketTestBuilder
    {
        private TaxBracket _taxBracket;

        public TaxBracketTestBuilder()
        {
            _taxBracket = new TaxBracket  
            {
               TaxRate = 0.15
            };
        }

        public TaxBracketTestBuilder WithTaxRate(double taxRate)
        {
            _taxBracket.TaxRate = taxRate;
            return this;
        }

      
        public TaxBracket Build()
        {
            return _taxBracket;
        }
    }
}
