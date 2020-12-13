using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace QueryAggregator.Apis
{
    public static class HttpClientService
    {
        private static HttpClient _instance;

        public static HttpClient Instance
        {
            get { return _instance ?? (_instance = new HttpClient()); }
        }
    }
}