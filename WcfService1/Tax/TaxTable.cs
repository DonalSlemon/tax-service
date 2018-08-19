using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxCalculator
{
    public class TaxTable : ITaxTable
    {
        private decimal medicalTaxCreditRebate;

        public double GetTaxAmountPayable(double incomeAmount, short yourage, double RaAmount = 0, bool annual = true, MedicalDetails medicaldetails = null)
        {
            //double taxpayable = 0;
            //double basetax;
            //double incomeAmountAboveThreshold;
            //double primaryrebate = Helpers.GetPrimaryRebate(yourage);
            //double effectiveTaxableIncome;

            //medicalTaxCreditRebate = medicaldetails != null ? Helpers.GetMedicalTaxRebate(medicaldetails, annual) : 0;

            ////List of the lower & upper limits, base tax and percentage of taxation
            //var incomeBands = Helpers.GetIncomeRanges();

            ////User selected Annual (default)
            //if (annual)
            //{
            //    effectiveTaxableIncome = incomeAmount - primaryrebate - medicalTaxCreditRebate;
            //    effectiveTaxableIncome = effectiveTaxableIncome - RaAmount;

            //    for (int i = 0; i < incomeBands.Count; i++)
            //    {
            //        if (effectiveTaxableIncome.IsBetween(incomeBands[i].Item1, incomeBands[i].Item2))
            //        {
            //            incomeAmountAboveThreshold = effectiveTaxableIncome - (incomeBands[i].Item1 - 1);
            //            basetax = incomeBands[i].Item3;

            //            taxpayable = basetax + (incomeAmountAboveThreshold * incomeBands[i].Item4);
            //        }
            //    }
            //}
            //else
            //{
            //    //Medicaltaxrebates are not considered for monthly calculations
            //    effectiveTaxableIncome = incomeAmount - (primaryrebate / 12);
            //    effectiveTaxableIncome = effectiveTaxableIncome - RaAmount;

            //    for (int i = 0; i < incomeBands.Count; i++)
            //    {
            //        if (effectiveTaxableIncome.IsBetween(incomeBands[i].Item1 / 12, incomeBands[i].Item2 / 12))
            //        {
            //            incomeAmountAboveThreshold = effectiveTaxableIncome - ((incomeBands[i].Item1 / 12) - 1);
            //            basetax = incomeBands[i].Item3 / 12;

            //            taxpayable = basetax + (incomeAmountAboveThreshold * incomeBands[i].Item4);
            //        }
            //    }

            //}
            //return taxpayable;
            return 0;
        }

        /// <summary>
        /// Uses Struct Helper instead of Tuple. Uses Named properties so more readable
        /// </summary>
        /// <param name="incomeAmount"></param>
        /// <param name="yourage"></param>
        /// <param name="RaAmount"></param>
        /// <param name="annual"></param>
        /// <param name="medicaldetails"></param>
        /// <returns></returns>
        public decimal GetTaxAmountPayableStruct(decimal incomeAmount, short yourage, decimal RaAmount = 0, bool annual = true, MedicalDetails medicaldetails = null)
        {
            decimal taxpayable = 0;
            decimal basetax;
            decimal incomeAmountAboveThreshold;
            decimal primaryrebate = Helpers.GetPrimaryRebateAsDecimal(yourage);
            decimal effectiveTaxableIncome;

            medicalTaxCreditRebate = medicaldetails != null ? Helpers.GetMedicalTaxRebateAsDecimal(medicaldetails, annual) : 0;

            //List of the lower & upper limits, base tax and percentage of taxation
            var incomeBands = Helpers.GetIncomeRangesStruct();

            //User selected Annual (default)
            if (annual)
            {
                effectiveTaxableIncome = incomeAmount - primaryrebate - medicalTaxCreditRebate;
                effectiveTaxableIncome = RaAmount > 0 ? (effectiveTaxableIncome - RaAmount) : effectiveTaxableIncome;

                for (int i = 0; i < incomeBands.Count; i++)
                {
                    if (effectiveTaxableIncome.IsBetween(incomeBands[i].LowerLimit, incomeBands[i].UpperLimit))
                    {
                        incomeAmountAboveThreshold = effectiveTaxableIncome - (incomeBands[i].LowerLimit - 1);
                        basetax = incomeBands[i].BaseTaxAmount;

                        taxpayable = basetax + (incomeAmountAboveThreshold * incomeBands[i].TaxRate);
                    }
                }
            }
            else
            {
                //Medicaltaxrebates and Primary rebates are not considered for monthly calculations
                effectiveTaxableIncome = incomeAmount;// - (primaryrebate / 12);
                effectiveTaxableIncome = RaAmount > 0 ? (effectiveTaxableIncome - RaAmount) : effectiveTaxableIncome;

                for (int i = 0; i < incomeBands.Count; i++)
                {
                    if (effectiveTaxableIncome.IsBetween(incomeBands[i].LowerLimit / 12, incomeBands[i].UpperLimit / 12))
                    {
                        incomeAmountAboveThreshold = effectiveTaxableIncome - ((incomeBands[i].LowerLimit / 12) - 1);
                        basetax = incomeBands[i].BaseTaxAmount / 12;

                        decimal taxrate = incomeBands[i].TaxRate;
                        taxpayable = basetax + (incomeAmountAboveThreshold * taxrate);
                    }
                }

            }
            return taxpayable;
        }

    }
}