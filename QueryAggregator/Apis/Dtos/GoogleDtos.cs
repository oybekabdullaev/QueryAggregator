using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueryAggregator.Apis.Dtos
{
    public class GoogleResponse
    {
        public List<GoogleLink> Items { get; set; }
    }

    public class GoogleLink
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Snippet { get; set; }
    }
}