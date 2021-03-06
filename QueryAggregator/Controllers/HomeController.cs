﻿using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
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
        private readonly IList<IApiHelper> _apis;

        public HomeController(IUnitOfWork unitOfWork, IList<IApiHelper> apis)
        {
            _unitOfWork = unitOfWork;
            _apis = apis;
        }

        public ActionResult Index()
        {
            return View(new QueryViewModel());
        }

        public async Task<ActionResult> Links(QueryViewModel queryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", queryViewModel);
            }

            var query = queryViewModel.Query;
            
            var loadedQuery = _unitOfWork.Queries.GetQueryByQueryStringWithLinks(query);

            if (loadedQuery == null)
            {
                loadedQuery = await LoadFromApiAsync(query);
                
                _unitOfWork.Queries.Add(loadedQuery);
                _unitOfWork.Complete();
            }

            return View(loadedQuery);
        }

        private async Task<Query> LoadFromApiAsync(string query)
        {
            var tasks = _apis.Select(a => a.GetLinksAsync(query)).ToList();

            var finishedTask = await Task.WhenAny(tasks);

            var links = await finishedTask;

            return new Query
            {
                Links = links,
                QueryString = query
            };
        }
    }
}