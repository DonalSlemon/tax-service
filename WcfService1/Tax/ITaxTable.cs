namespace TaxCalculator
{
    public interface ITaxTable
    {
        double GetTaxAmountPayable(double incomeAmount, short yourage, double RaAmount = 0, bool annual = true, MedicalDetails medicaldetails = null);
        decimal GetTaxAmountPayableStruct(decimal incomeAmount, short yourage, decimal RaAmount = 0, bool annual = true, MedicalDetails medicaldetails = null);
    }
}