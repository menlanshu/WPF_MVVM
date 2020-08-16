using Newtonsoft.Json;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class MajorIndexService : IMajorIndexService
    {
        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {
            using (FinancialModelingPrepHttpClient client = FinancialModelingPrepHttpFactory.CreateFinancialModelingPrepHttpClient())
            {
                string uri = $"majors-indexes/{GetUriSuffix(indexType)}";

                //client.GetAsync("https://financialmodelingprep.com/api/v3/majors-indexes?apikey=6986ededa1c12a3f6a07798afffd69ed");
                MajorIndex majorIndex= await client.GetAsync<MajorIndex>(uri);
                majorIndex.Type = indexType;
                return majorIndex;
            }
        }

        private string GetUriSuffix(MajorIndexType indexType)
        {
            switch (indexType)
            {
                case MajorIndexType.DowJones:
                    return ".DJI";
                case MajorIndexType.Nasdaq:
                    return ".IXIC";
                case MajorIndexType.SP500:
                    return ".INX";
                default:
                    throw new Exception("MajorIndexType does not have a suffix to find");
            }
        }
    }
}
