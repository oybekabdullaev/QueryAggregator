using QueryAggregator.Controllers;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using QueryAggregator.Core;
using QueryAggregator.Core.Domain;

namespace QueryAggregator.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        private readonly string _query = "queryString";
        private Mock<IUnitOfWork> _unitOfWork;
        private HomeController _controller;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _controller = new HomeController(_unitOfWork.Object, null);
        }

        [Test]
        public void Index_WhenCalled_ReturnsViewResult()
        {
            var result = _controller.Index();

            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        //[Test]
        //[TestCase(null)]
        //[TestCase("")]
        //[TestCase(" ")]
        //public void Links_QueryStringIsNullOrWhitespace_ReturnsHttpNotFoundResult(string query)
        //{
        //    var result = _controller.Links(query);

        //    Assert.That(result, Is.TypeOf<HttpNotFoundResult>());
        //}

        //[Test]
        //public void Links_QueryDoesNotExistInDatabase_ReturnsHttpNotFound()
        //{
        //    _unitOfWork
        //        .Setup(uof => uof.Queries.GetQueryByQueryStringWithLinks(_query))
        //        .Returns(() => null);
            
        //    var result = _controller.Links(_query);

        //    Assert.That(result, Is.TypeOf<HttpNotFoundResult>());
        //}

        //[Test]
        //public void Links_QueryExistsInDatabase_ReturnsViewResult()
        //{
        //    _unitOfWork
        //        .Setup(uof => uof.Queries.GetQueryByQueryStringWithLinks(_query))
        //        .Returns(new Query());

        //    var result = _controller.Links(_query);
             
        //    Assert.That(result, Is.TypeOf<ViewResult>());
        //}
    }
}
