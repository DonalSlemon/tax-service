using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxCalculator.Income
{
    public struct IncomeBandStruct
    {
        public decimal LowerLimit, UpperLimit, BaseTaxAmount, TaxRate;

        public IncomeBandStruct(decimal lowerLimit, decimal upperLimit, decimal baseTax, decimal taxRate)
        {
            LowerLimit = lowerLimit;
            UpperLimit = upperLimit;
            BaseTaxAmount = baseTax;
            TaxRate = taxRate;
        }
    }
}