using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryAggregator.Core.Domain;
using QueryAggregator.Core.Repositories;

namespace QueryAggregator.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IQueryRepository Queries { get; }
        int Complete();
    }
}
