using System.Collections.Generic;
using System.Xml.Serialization;

namespace TaxCalculator.Tax
{
    [XmlRoot("TaxRebates", Namespace = "WcfService1.Tax")]
    public class TaxRebateRates
    {
        [XmlElement("TaxRebate")]
        public List<TaxRebate> TaxRebates { get; set; }
    }

    public class TaxRebate
    {
        [XmlElement("PrimaryRebate")]
        public double PrimaryRebate { get; set; }

        [XmlElement("PrimaryRebate65")]
        public double PrimaryRebate65 { get; set; }

        [XmlElement("PrimaryRebate75")]
        public double PrimaryRebate75 { get; set; }

        [XmlElement("MedicalTaxCredit")]
        public double MedicalTaxCredit { get; set; }

        [XmlElement("MedicalTaxCreditPlus")]
        public double MedicalTaxCreditPlus { get; set; }
    }
}