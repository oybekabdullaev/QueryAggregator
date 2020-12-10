using System.Web.Mvc;
using QueryAggregator.Persistence;
using System.Data.Entity;
using System.Linq;
using QueryAggregator.Core;

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
            var query = _unitOfWork.Queries.GetQueryByQueryStringWithLinks("hello");

            return View(query);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();

            base.Dispose(disposing);
        }
    }
}