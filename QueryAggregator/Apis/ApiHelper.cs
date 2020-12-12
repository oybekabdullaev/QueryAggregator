using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace QueryAggregator.Apis
{
    public static class ApiHelper
    {
        private static HttpClient _httpClient;

        public static HttpClient HttpClient
        {
            get { return _httpClient ?? (_httpClient = new HttpClient()); }
        }
    }
}