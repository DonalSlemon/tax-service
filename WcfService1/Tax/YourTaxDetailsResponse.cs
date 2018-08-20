using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TaxCalculator
{
    [DataContract]
    public class YourTaxDetailsResponse
    {
        [DataMember(Order = 0)]
        public decimal TaxPayable { get; set; }

        [DataMember(Order = 1)]
        public decimal TakeHome { get; set; }

        [DataMember(Order = 2)]
        public decimal TakeHomeLessRA { get; set; }

        [DataMember(Order = 3)]
        public decimal EffectiveTaxRate { get; set; }

        [DataMember(Order = 4)]
        public decimal TaxableIncome { get; set; }
    }
}