using System.Runtime.Serialization;

namespace TaxCalculator
{
    [DataContract]
    public class MedicalDetails
    {
        [DataMember]
        public bool HaveMedicalAid { get; set; }

        [DataMember]
        public short Dependants { get; set; }
    }
}