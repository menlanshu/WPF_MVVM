﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI
{
    public class FinancialModelingPrepHttpClient : HttpClient
    {
        private readonly string _apikey;

        public FinancialModelingPrepHttpClient(string apikey)
        {
            this.BaseAddress = new Uri("https://financialmodelingprep.com/api/v3/");
            _apikey = apikey;
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            HttpResponseMessage response = await GetAsync($"{uri}?apikey={_apikey}");
            string jsonResponse = await response.Content.ReadAsStringAsync();

            //If a list of T is passed back ,then error may happen
            T result = JsonConvert.DeserializeObject<T>(jsonResponse);

            return result;
        }

    }
}
