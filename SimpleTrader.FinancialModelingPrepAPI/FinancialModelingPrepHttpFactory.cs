using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace SimpleTrader.FinancialModelingPrepAPI
{
    public class FinancialModelingPrepHttpFactory
    {
        private static readonly string _apikeyConfigName = "apikey";

        public static FinancialModelingPrepHttpClient CreateFinancialModelingPrepHttpClient()
        {
            string apikey = ConfigurationManager.AppSettings.Get(_apikeyConfigName);
            return new FinancialModelingPrepHttpClient(apikey);
        }
    }
}
