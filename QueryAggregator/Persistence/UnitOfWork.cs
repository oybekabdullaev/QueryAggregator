using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QueryAggregator.Core;
using QueryAggregator.Core.Repositories;
using QueryAggregator.Persistence.Repositories;

namespace QueryAggregator.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QueryAggregatorContext _context;

        public UnitOfWork(QueryAggregatorContext context)
        {
            _context = context;
            Queries = new QueryRepository(_context);
        }

        public IQueryRepository Queries { get; }
        
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}