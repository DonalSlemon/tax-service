using TaxCalculator.Income;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxCalculator.Tax;
using System.Security;

namespace TaxCalculator
{
    public static class Helpers
    {
        public static bool IsBetween<T>(this T item, T lowernumber, T uppernumber) where T: IComparable<T>
        {
            return Comparer<T>.Default.Compare(item, lowernumber) >= 0 && Comparer<T>.Default.Compare(item, uppernumber) <= 0;
        }

        /// <summary>
        /// Loads Data From XML File, returning Income bands, base tax amounts and taxation percentage rates
        /// </summary>
        /// <params>First double ('item1') is LOWER boundary of income</params>
        /// <params>Second double ('item2') is UPPER boundary of income</params>
        /// <params>Third double ('item3') is base tax amount which applies to this band</params>
        /// <params>Fourth double ('item4') is tax percentage which applies to amount above threshold</params>
        /// <returns>List of <see cref="Tuple"/> containing lowerbound, upperbound, base tax, taxation perecentage</returns>
        public static List<Tuple<double, double, double, double>> GetIncomeRanges()
        {
            List<Tuple<double, double, double, double>> ranges = new List<Tuple<double, double, double, double>>();
            double lowerlimit, upperlimit, basetaxamount, taxrate;

            var arrayOfRangesFromXml = XmlLoader.Load<IncomeBandsRates>("IncomeBandsRates.xml");

            foreach (var item in arrayOfRangesFromXml.IncomeBands.ToArray())
            {
                var lower = double.TryParse(item.LowerLimit, out lowerlimit) ? lowerlimit : 0;
                var upper = double.TryParse(item.UpperLimit, out upperlimit) ? upperlimit : 0;
                var basetax = double.TryParse(item.BaseTaxAmount, out basetaxamount) ? basetaxamount : 0;
                var percenttax = double.TryParse(item.TaxRate, out taxrate) ? taxrate : 0;

                var range = new Tuple<double, double, double, double>(lower, upper, basetax, percenttax);
                ranges.Add(range);
            }
            return ranges;
        }

        /// <summary>
        /// Loads Data From XML File, returning Income bands, base tax amounts and taxation percentage rates
        /// Uses List of type Struct instead of Tuple
        /// </summary>
        /// <params>The <see cref="IncomeBandStruct"/>.</params>
        /// <returns>The <see cref="List{IncomeBandStruct}"/>. </returns>
        public static List<IncomeBandStruct> GetIncomeRangesStruct()
        {
            List<IncomeBandStruct> ranges = new List<IncomeBandStruct>();
            decimal lowerlimit, upperlimit, basetaxamount;
            float taxrate;

            var arrayOfRangesFromXml = XmlLoader.Load<IncomeBandsRates>("IncomeBandsRates.xml");

            foreach (var item in arrayOfRangesFromXml.IncomeBands.ToArray())
            {
                var lower = decimal.TryParse(item.LowerLimit, out lowerlimit) ? lowerlimit : 0;
                var upper = decimal.TryParse(item.UpperLimit, out upperlimit) ? upperlimit : 0;
                var basetax = decimal.TryParse(item.BaseTaxAmount, out basetaxamount) ? basetaxamount : 0;

                double val = double.Parse(item.TaxRate, System.Globalization.CultureInfo.InvariantCulture);
                val = Math.Round(val, 2);
                taxrate = (float)val;

                var range = new IncomeBandStruct(lower, upper, basetax, (decimal)taxrate);
                ranges.Add(range);
            }
            return ranges;
        }

        internal static decimal GetMedicalTaxRebate(MedicalDetails medicaldetails, bool isAnnual = false)
        {
            decimal rebateamount = (decimal)MedicalRebate(medicaldetails.HaveMedicalAid, medicaldetails.Dependants);
            return isAnnual ? rebateamount * 12 : rebateamount;
        }

        internal static decimal GetPrimaryRebate(short yourage)
        {
            return (decimal)PrimaryRebate(yourage);
        }

        private static double PrimaryRebate(short yourage)
        {
            TaxRebateRates rebatesdata = LoadRebatesData();

            var primary = rebatesdata.TaxRebates[0].PrimaryRebate;
            var primary65 = rebatesdata.TaxRebates[0].PrimaryRebate65;
            var primary75 = rebatesdata.TaxRebates[0].PrimaryRebate75;

            var rebatestruct = new TaxRebateStruct(primary, primary65, primary75);

            double totalrebates;

            if (yourage > 17 && yourage < 65)
                totalrebates = rebatestruct.PrimaryRebate;
            else if (yourage < 75)
                totalrebates = rebatestruct.PrimaryRebate + rebatestruct.PrimaryRebate65;
            else
                totalrebates = rebatestruct.PrimaryRebate + rebatestruct.PrimaryRebate65 + rebatestruct.PrimaryRebate75;

            return totalrebates;
        }

        private static TaxRebateRates LoadRebatesData()
        {
            try
            {
                return XmlLoader.Load<TaxRebateRates>("PrimaryTaxRebates.xml");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static double MedicalRebate(bool hasMedical, short numdependants)
        {
            double totalrebate = 0;

            try
            {
                TaxRebateRates rebatesdata = LoadRebatesData();

                var medicaltaxcredit = rebatesdata.TaxRebates[0].MedicalTaxCredit;
                var medicaltaxcreditplus = rebatesdata.TaxRebates[0].MedicalTaxCreditPlus;

                if (hasMedical)
                {
                    if (numdependants == 0)
                    {
                        totalrebate = medicaltaxcredit;
                    }
                    else if (numdependants == 1)
                    {
                        totalrebate = medicaltaxcredit * 2;
                    }
                    else
                    {
                        totalrebate = (medicaltaxcredit * 2) + (medicaltaxcreditplus * (numdependants - 1));
                    }
                    return totalrebate;
                }
            }
            catch (Exception exload)
            {
                throw new Exception(exload.Message);
            }
            return totalrebate;
        }
    }
}