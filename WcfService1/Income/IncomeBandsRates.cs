using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace TaxCalculator.Income
{
    [XmlRoot("IncomeBands", Namespace = "TaxCalculator.Income")]
    public class IncomeBandsRates
    {
        [XmlElement("IncomeBand")]
        public List<IncomeBand> IncomeBands { get; set; }
    }

    public class IncomeBand
    {
        [XmlElement("LowerLimit")]
        public string LowerLimit { get; set; }

        [XmlElement("UpperLimit")]
        public string UpperLimit { get; set; }

        [XmlElement("BaseTaxAmount")]
        public string BaseTaxAmount { get; set; }

        [XmlElement("TaxRate")]
        public string TaxRate { get; set; }
    }
}