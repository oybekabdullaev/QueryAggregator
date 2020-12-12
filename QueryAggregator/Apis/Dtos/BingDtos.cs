using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueryAggregator.Apis.Dtos
{
    public class BingResponse
    {
        public BingWebPages WebPages { get; set; }
    }

    public class BingWebPages
    {
        public List<BingLink> Value { get; set; }
    }

    public class BingLink
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string Snippet { get; set; }
    }
}