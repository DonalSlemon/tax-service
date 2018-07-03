using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TestService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TestService.svc or TestService.svc.cs at the Solution Explorer and start debugging.
    public class TestService : ITestService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public double Add(double n1, double n2)
        {
            return n1 + n2;
        }

        public async Task<double> DoSumsAsync(double n1, double n2)
        {
            try
            {
                return await Task<double>.Factory.StartNew(() =>
                     {
                         return n1 + n2;
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
        public double Subtract(double n1, double n2)
        {
            return n1 - n2;
        }
        public double Multiply(double n1, double n2)
        {
            return n1 * n2;
        }
        public double Divide(double n1, double n2)
        {
            return n1 / n2;
        }
        public double ShowMyTakeHome(double earnings, double taxrate)
        {
            var divisor = taxrate / 100;
            return earnings - (earnings * divisor);
        }
    }
}
