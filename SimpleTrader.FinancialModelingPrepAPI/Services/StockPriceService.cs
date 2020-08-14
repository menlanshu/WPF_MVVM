using Newtonsoft.Json;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.FinancialModelingPrepAPI.Results;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class StockPriceService : IStockPriceService
    {
        public async Task<double> GetPrice(string symbol)
        {
            using(FInancialModelingPrepHttpClient client = new FInancialModelingPrepHttpClient())
            {
                string uri = $"stock/real-time-price/{symbol}?apikey=6986ededa1c12a3f6a07798afffd69ed";

                StockPriceResult stockPriceResult = await client.GetAsync<StockPriceResult>(uri);

                if(stockPriceResult.Price == 0)
                {
                    throw new InvalidSymbolException(symbol);
                }

                return stockPriceResult.Price;
            }
        }
    }
}
