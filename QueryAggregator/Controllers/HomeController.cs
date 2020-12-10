using System.Web.Mvc;
using QueryAggregator.Persistence;
using System.Data.Entity;
using System.Linq;
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

        public ActionResult Links(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return HttpNotFound("Please provide a query.");
            
            var queryInDb = _unitOfWork.Queries.GetQueryByQueryStringWithLinks(query);

            if (queryInDb == null)
                return HttpNotFound("No such query in the database.");

            return View(queryInDb);
        }
    }
}