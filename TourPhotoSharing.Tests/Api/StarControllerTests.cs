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
        private Mock<IStarRepository> _mockRepository;
        private Mock<IImageRepository> _mockImageRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IStarRepository>();
            _mockImageRepository = new Mock<IImageRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(u => u.StarredImages).Returns(_mockRepository.Object);
            mockUoW.Setup(u => u.Images).Returns(_mockImageRepository.Object);

            _controller = new StarController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");

            AutoMapperConfiguration.Configure();
        }

        [TestMethod]
        public void Add_StarAnImage_ReturnOk()
        {
            var image = new Image();
            _mockRepository.Setup(i => i.AddStar(image, _userId)).Returns(true);
            _mockImageRepository.Setup(i => i.GetImage("1")).Returns(image);

            var result = _controller.Star("1");

            result.Should().BeOfType<OkNegotiatedContentResult<bool>>();
        }

        [TestMethod]
        public void Add_UnableToFindImage_ReturnNotFound()
        {
            var result = _controller.Star("1");

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Remove_UnstarAnImage_ReturnOk()
        {
            var image = new Image();
            _mockRepository.Setup(i => i.RemoveStar(image, _userId)).Returns(true);
            _mockImageRepository.Setup(i => i.GetImage("1")).Returns(image);

            var result = _controller.UnStar("1");

            result.Should().BeOfType<OkNegotiatedContentResult<bool>>();
        }

        [TestMethod]
        public void Remove_UnableToFindImage_ReturnNotFound()
        {
            var result = _controller.UnStar("1");

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
