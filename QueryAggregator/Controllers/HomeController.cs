using System.Web.Mvc;
using QueryAggregator.Persistence;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using QueryAggregator.Apis;
using QueryAggregator.Core;
using QueryAggregator.Core.Domain;
using QueryAggregator.ViewModels;

namespace QueryAggregator.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View(new QueryViewModel());
        }

        public async Task<ActionResult> Links(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return HttpNotFound("Please provide a query.");
            
            var loadedQuery = _unitOfWork.Queries.GetQueryByQueryStringWithLinks(query);

            if (loadedQuery == null)
                loadedQuery = await LoadFromApiAsync(query);
            
            return View(loadedQuery);
        }

        private async Task<Query> LoadFromApiAsync(string query)
        {
            var api = new GoogleApi(ApiHelper.HttpClient);
            var links = await api.GetLinksAsync(query);

            return new Query
            {
                Links = links,
                QueryString = query
            };
        }
    }
}