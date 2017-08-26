using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Security.Principal;
using System.Web.Mvc;
using TPS.Web.Controllers;
using TPS.Web.Core;
using TPS.Web.Core.Models;
using TPS.Web.Core.Repositories;
using TPS.Web.Core.ViewModels;

namespace TourPhotoSharing.Tests.Controllers
{
    [TestClass]
    public class TourControllerTests
    {
        private TourController _controller;
        private Mock<ITourRepository> _mockRepository;
        private string _username;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<ITourRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(u => u.Tours).Returns(_mockRepository.Object);

            _controller = new TourController(mockUoW.Object);
            _username = "user1@domain.com";

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns(_username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            _controller.ControllerContext = controllerContext.Object;
        }

        [TestMethod]
        public void Index_View_ShouldReturnListOfTours()
        {
            var result = _controller.Index() as ViewResult;

            result.Model.Should().NotBeNull();
            result.ViewName.Should().Be("Index");
        }

        [TestMethod]
        public void New_View_ShouldReturnTourForm()
        {
            var result = _controller.New() as ViewResult;

            result.ViewName.Should().Be("TourForm");
            result.Model.Should().NotBeNull();
        }
        [TestMethod]
        public void Edit_NoTourFound_ShouldReturnNotFound()
        {
            var result = _controller.Edit("1");

            result.Should().NotBeNull();
            result.Should().BeOfType<HttpNotFoundResult>();
        }
        [TestMethod]
        public void Edit_View_ShouldReturnTourObject()
        {
            var tour = new Tour();
            _mockRepository.Setup(r => r.GetTour("1")).Returns(tour);

            var result = _controller.Edit("1") as ViewResult;

            result.ViewName.Should().Be("TourForm");
        }
        [TestMethod]
        public void Save_ValidTour_ShouldRedirectToView()
        {
            var tour = new Tour();
            _mockRepository.Setup(r => r.GetTour("1")).Returns(tour);

            var tourFormViewModel = new TourFormViewModel {Id = "1", Name = "Name"};

            var result = _controller.Save(tourFormViewModel);

            result.Should().BeOfType<RedirectToRouteResult>();
        }
        [TestMethod]
        public void Save_NoNameSpecified_ShouldReturnForm()
        {
            _controller.ViewData.ModelState.Clear();

            var tour = new Tour();
            _mockRepository.Setup(r => r.GetTour("1")).Returns(tour);

            var tourFormViewModel = new TourFormViewModel {Id = "1"};

            var result = _controller.Save(tourFormViewModel) as ViewResult;

            result.ViewName.Should().Be("TourForm");
        }
        [TestMethod]
        public void ViewTour_TourNotFound_ShouldReturnNull()
        {
            var result = _controller.ViewTour("1") as ViewResult;

            result.Should().BeNull();
        }
        [TestMethod]
        public void ViewTour_ValidTour_ShouldReturnView()
        {
            var tour = new Tour();
            _mockRepository.Setup(r => r.GetTour("1")).Returns(tour);

            var result = _controller.ViewTour("1") as ViewResult;

            result.Should().NotBeNull();
        }
    }
}
