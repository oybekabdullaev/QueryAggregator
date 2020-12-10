using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using QueryAggregator.Core.Domain;
using QueryAggregator.Core.Repositories;

namespace QueryAggregator.Persistence.Repositories
{
    public class QueryRepository : Repository<Query>, IQueryRepository
    {
        public QueryRepository(QueryAggregatorContext context) : base(context)
        {
        }

        public Query GetQueryByQueryStringWithLinks(string queryString)
        {
            return QueryAggregatorContext.Queries
                .Include(q => q.Links)
                .SingleOrDefault(q => q.QueryString == queryString);
        }

        private QueryAggregatorContext QueryAggregatorContext
        {
            get { return Context as QueryAggregatorContext; }
        }
    }
}