using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxCalculator
{
    using System.IO;
    using System.Xml.Serialization;

    /// <summary>
    /// Loads Data from XML File in the specified 'paths' array
    /// </summary>
    public static class XmlLoader
    {
        public static T Load<T>(string filename) where T : class
        {
            var paths = new[]
                {
                    AppDomain.CurrentDomain.SetupInformation.PrivateBinPath,
                    AppDomain.CurrentDomain.BaseDirectory,
                    AppDomain.CurrentDomain.RelativeSearchPath,
                    @"C:\Users\Donals\Documents\Visual Studio 2017\Projects\WcfService1\WcfService1\Income",
                    @"C:\Users\Donals\Documents\Visual Studio 2017\Projects\WcfService1\WcfService1\Tax",
                    ".\\"
                };

            foreach (var path in paths)
            {
                if (path == null)
                    continue;

                //string src = string.Empty;
                var thefile = Path.Combine(path, filename);

                if (File.Exists(thefile))
                {
                    using (var thefilestream = File.OpenRead(thefile))
                    {
                        var serialiser = new XmlSerializer(typeof(T));
                        try
                        {
                            var src = serialiser.Deserialize(thefilestream);

                            return (T)src;
                        }
                        catch (Exception ex)
                        {

                            throw new Exception($"Exception: {ex.Message}");
                        }
                    }
                }
            }
            throw new Exception($"Unable to load {filename}. Searched in the following paths: {string.Join(", ", paths)}");
        }
    }
}