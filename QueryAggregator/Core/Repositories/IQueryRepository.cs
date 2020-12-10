using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QueryAggregator.Core.Domain;

namespace QueryAggregator.Core.Repositories
{
    public interface IQueryRepository : IRepository<Query>
    {
        Query GetQueryByQueryStringWithLinks(string queryString);
    }
}