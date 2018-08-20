using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using WcfService1;

namespace TaxCalculator
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TaxService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TaxService.svc or TaxService.svc.cs at the Solution Explorer and start debugging.
    /// Svc needs 2 ctors: One is the Default, other one takes the object needing resolution (ITaxTable) in as a param
    public class TaxService : ITaxService
    {
        private readonly ITaxTable _taxTable;

        /// <summary>
        /// The Default ctor. Pass-through implementation. Uses the Unity facade to retrieve and resolve dependency.
        /// </summary>
        public TaxService() : this(WcfServiceFactory.Resolve<ITaxTable>())
        {
        }

        public TaxService(ITaxTable taxTable)
        {
            this._taxTable = taxTable;
        }

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}

        //public YourTaxDetails ShowTaxDetails(YourTaxDetails details, short age, bool annual = true)
        //{
        //    //var tt = new TaxTable(taxTable);
        //    var earnings = details.Earnings;
        //    var racontribution = details.RaContrib;
        //    var yourtax = taxTable.GetTaxAmountPayable(earnings, age, racontribution, annual);

        //    details.AgeInYears = age;
        //    details.TimePeriod = annual ? "Annual" : "Monthly";

        //    return details;
        //}

        //public YourTaxDetailsResponse ShowTaxPayableTakeHome(YourTaxDetails details, short age, bool annual = true, bool medical = false)
        //{
        //    var tt = new TaxTable(taxTable);
        //    var earnings = details.Earnings;
        //    var racontribution = details.RaContrib;
        //    age = details.AgeInYears;

        //    medical = details.Medical.HaveMedicalAid ? true : false;
        //    var numberOfDependants = medical ? details.Medical.Dependants : 0;

        //    var yourtax = tt.GetTaxAmountPayableStruct(earnings, age, racontribution, annual, details.Medical);

        //    YourTaxDetailsResponse response = new YourTaxDetailsResponse();

        //    response.TaxPayable = Math.Round(yourtax, 2);
        //    response.TakeHome = Math.Round(earnings - yourtax, 2);
        //    response.TakeHomeLessRA = Math.Round(earnings - (yourtax + racontribution), 2);
        //    //response.EffectiveTaxRate = Math.Round((yourtax + racontribution)/ earnings * 100/1, 2);

        //    return response;
        //}

        public async Task<YourTaxDetailsResponse> ShowTaxPayableTakeHomeAsync(YourTaxDetails details, short age, bool annual = true, bool medical = false)
        {
            var earnings = details.Earnings;
            var racontribution = details.RaContrib;
            age = details.AgeInYears;
            medical = details.Medical.HaveMedicalAid ? true : false;
            var numberOfDependants = medical ? details.Medical.Dependants : 0;
            decimal yourtax;

            try
            {
                return await Task<YourTaxDetailsResponse>.Factory.StartNew(() =>
                {
                    yourtax = _taxTable.GetTaxAmountPayableStruct(earnings, age, racontribution, annual, details.Medical);
                    var effectiveRate = Math.Round(Math.Round(yourtax, 2) / Math.Round(earnings - yourtax, 2) * 100 / 1, 2);

                    var response = new YourTaxDetailsResponse
                    {
                        TaxableIncome = Math.Round(earnings - racontribution, 2),
                        TaxPayable = Math.Round(yourtax, 2),
                        TakeHome = Math.Round(earnings - yourtax, 2),
                        TakeHomeLessRA = Math.Round(earnings - (yourtax + racontribution), 2),
                        EffectiveTaxRate = effectiveRate
                    };
                    return response;
                });
            }
            catch (AggregateException aEx)
            {

                throw aEx;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
