using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TaxCalculator
{
    [DataContract]
    public class YourTaxDetails
    {

        [DataMember(Order = 0)]
        public string TimePeriod { get; set; }

        [DataMember(Order = 1)]
        public decimal Earnings { get; set; }

        [DataMember(Order = 2)]
        public decimal RaContrib { get; set; }

        [DataMember(Order = 3)]
        public short AgeInYears { get; set; }

        [DataMember(Order = 4)]
        public MedicalDetails Medical { get; set; }

    }
}