using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;
using TPS.Web.App_Start;
using TPS.Web.Controllers.Api;
using TPS.Web.Core;
using TPS.Web.Core.Models;
using TPS.Web.Core.Repositories;

namespace TourPhotoSharing.Tests.Api
{
    [TestClass]
    public class StarControllerTests
    {
        private StarController _controller;
        private Mock<IImageRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IImageRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(u => u.Images).Returns(_mockRepository.Object);

            _controller = new StarController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");

            AutoMapperConfiguration.Configure();
        }

        [TestMethod]
        public void AddStar_ValidRequest_ShouldReturnImage()
        {
            var image = new Image();
            _mockRepository.Setup(r => r.GetImage("1")).Returns(image);

            var result = _controller.AddStar("1");

            result.Should().BeOfType<OkNegotiatedContentResult<bool>>();
        }

        [TestMethod]
        public void AddStar_NoImageFound_ShouldReturnNotound()
        {
            var result = _controller.AddStar("1");

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void DeleteStar_NoImageFound_ShouldReturnNotound()
        {
            var result = _controller.DeleteStar("1");

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void DeleteStar_RemoveUserStar_ShouldReturnOk()
        {
            var image = new Image();
            _mockRepository.Setup(r => r.GetImage("1")).Returns(image);

            var result = _controller.DeleteStar("1");

            result.Should().BeOfType<OkNegotiatedContentResult<bool>>();
        }

        
    }
}