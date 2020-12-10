using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueryAggregator.Core.Domain
{
    public class Query
    {
        public int Id { get; set; }
        public string QueryString { get; set; }
        public List<Link> Links { get; set; }
    }
}