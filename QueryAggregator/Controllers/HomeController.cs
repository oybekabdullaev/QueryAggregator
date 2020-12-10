using System.Web.Mvc;
using QueryAggregator.Persistence;
using System.Data.Entity;
using System.Linq;

namespace QueryAggregator.Controllers
{
    public class HomeController : Controller
    {
        private readonly QueryAggregatorContext _context;

        public HomeController()
        {
            _context = new QueryAggregatorContext();
        }

        public ActionResult Index()
        {
            var query = _context.Queries.Include(q => q.Links).FirstOrDefault();

            return View(query);
        }
    }
}