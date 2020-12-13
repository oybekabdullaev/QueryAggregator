using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using QueryAggregator.Core.Domain;

namespace QueryAggregator.Apis
{
    public interface IApiHelper
    {
        Task<List<Link>> GetLinksAsync(string query);
    }
}