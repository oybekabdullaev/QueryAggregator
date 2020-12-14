using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QueryAggregator.Controllers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Moq;
using NUnit.Framework;
using QueryAggregator.Apis;
using QueryAggregator.Core;
using QueryAggregator.Core.Domain;
using QueryAggregator.ViewModels;

namespace QueryAggregator.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IApiHelper> _api1;
        private Mock<IApiHelper> _api2;
        private HomeController _controller;

        private readonly QueryViewModel _queryViewModel = new QueryViewModel {Query = "hello"};
        private readonly Query _query = new Query
        {
            Links = new List<Link>()
        };

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _api1 = new Mock<IApiHelper>();
            _api2 = new Mock<IApiHelper>();

            _controller = new HomeController(_unitOfWork.Object, 
                new List<IApiHelper> { _api1.Object, _api2.Object });
        }

        [Test]
        public void Index_WhenCalled_ReturnsViewResultWithModel()
        {
            var result = _controller.Index();

            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.That(((ViewResult) result).Model, Is.Not.Null);
        }

        [Test]
        public async Task Links_QueryStringIsNotSpecified_ReturnsIndexViewWithProvidedQueryViewModel()
        {
            _controller.ModelState.AddModelError("Query", "Query is required");

            var result = await _controller.Links(_queryViewModel);

            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.That(((ViewResult)result).ViewName, Is.EqualTo("Index"));
            Assert.That(((ViewResult)result).Model, Is.EqualTo(_queryViewModel));
        }

        [Test]
        public async Task Links_QueryStringIsNotSpecified_DoesNotRequestDatabase()
        {
            _controller.ModelState.AddModelError("Query", "Query is required");

            var result = await _controller.Links(_queryViewModel);

            _unitOfWork.Verify(
                uow => 
                    uow.Queries.GetQueryByQueryStringWithLinks(It.IsAny<string>()), 
                Times.Never);
        }

        [Test]
        public async Task Links_QueryStringIsNotSpecified_DoesNotRequestApis()
        {
            _controller.ModelState.AddModelError("Query", "Query is required");

            var result = await _controller.Links(_queryViewModel);

            _api1.Verify(api => api.GetLinksAsync(It.IsAny<string>()), Times.Never);
            _api2.Verify(api => api.GetLinksAsync(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task Links_QueryIsSpecified_RequestsQueryFromDatabase()
        {
            _unitOfWork
                .Setup(uow => 
                    uow.Queries.GetQueryByQueryStringWithLinks(It.IsAny<string>()))
                .Returns(_query);

            var result = await _controller.Links(_queryViewModel);

            _unitOfWork.Verify(uow => 
                uow.Queries.GetQueryByQueryStringWithLinks(_queryViewModel.Query));
        }

        [Test]
        public async Task Links_QueryExistsInDatabase_ReturnsViewResultWithQueryRetrievedFromDatabase()
        {
            _unitOfWork
                .Setup(uow =>
                    uow.Queries.GetQueryByQueryStringWithLinks(It.IsAny<string>()))
                .Returns(_query);

            var result = await _controller.Links(_queryViewModel);

            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.That(((ViewResult) result).Model, Is.EqualTo(_query));
        }

        [Test]
        public async Task Links_QueryExistsInDatabase_DoesNotRequestApis()
        {
            _unitOfWork
                .Setup(uow =>
                    uow.Queries.GetQueryByQueryStringWithLinks(_queryViewModel.Query))
                .Returns(_query);

            var result = await _controller.Links(_queryViewModel);

            _api1.Verify(api => api.GetLinksAsync(It.IsAny<string>()), Times.Never);
            _api2.Verify(api => api.GetLinksAsync(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task Links_QueryDoesNotExistInDatabase_RequestsAllApis()
        { 
            _unitOfWork
                .Setup(uow =>
                    uow.Queries.GetQueryByQueryStringWithLinks(It.IsAny<string>()))
                .Returns(() => null);

            var result = await _controller.Links(_queryViewModel);

            _api1.Verify(api => api.GetLinksAsync(_queryViewModel.Query));
            _api2.Verify(api => api.GetLinksAsync(_queryViewModel.Query));
        }

        [Test]
        public async Task Links_QueryDoesNotExistInDatabase_AddsQueryToDatabase()
        {
            _unitOfWork
                .Setup(uow =>
                    uow.Queries.GetQueryByQueryStringWithLinks(It.IsAny<string>()))
                .Returns(() => null);
            
            _api1.Setup(api => api.GetLinksAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Link>());

            var result = await _controller.Links(_queryViewModel);

            _unitOfWork.Verify(uow => uow.Queries.Add(It.IsAny<Query>()));
            _unitOfWork.Verify(uow => uow.Complete());
        }

        [Test]
        public async Task Links_QueryDoesNotExistInDatabase_ReturnsViewResultWithModel()
        {
            _unitOfWork
                .Setup(uow =>
                    uow.Queries.GetQueryByQueryStringWithLinks(_queryViewModel.Query))
                .Returns(() => null);
            
            _api1.Setup(api => api.GetLinksAsync(_queryViewModel.Query))
                .ReturnsAsync(new List<Link>());

            var result = await _controller.Links(_queryViewModel);

            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.That(((ViewResult) result).Model, Is.Not.Null);
        }
    }
}
