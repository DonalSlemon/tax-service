using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxCalculator.Tax
{
    public struct TaxRebateStruct
    {
        public double PrimaryRebate, PrimaryRebate65, PrimaryRebate75;

        public TaxRebateStruct(double primaryrebate, double primaryrebate65, double primaryrebate75)
        {
            PrimaryRebate = primaryrebate;
            PrimaryRebate65 = primaryrebate65;
            PrimaryRebate75 = primaryrebate75;
        }
    }
}