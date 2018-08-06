using System;
using System.Collections.Generic;
using System.Text;
using Payroll.Services.POCO;

namespace Payroll.Services
{
    public interface ITaxService
    {
        TaxBracket GetTaxBracket(double hourlyRate);
    }
}
