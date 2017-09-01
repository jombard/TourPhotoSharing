using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Web.Hosting;
using System.Web.Http.Results;
using TPS.Web.App_Start;
using TPS.Web.Controllers.Api;
using TPS.Web.Core;
using TPS.Web.Core.Dtos;
using TPS.Web.Core.Models;
using TPS.Web.Core.Repositories;

namespace TourPhotoSharing.Tests.Api
{
    [TestClass]
    public class ImageControllerTests
    {
        private ImageController _controller;
        private Mock<IImageRepository> _mockRepository;
        private Mock<ITourRepository> _mockTourRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IImageRepository>();
            _mockTourRepository = new Mock<ITourRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(u => u.Images).Returns(_mockRepository.Object);
            mockUoW.Setup(u => u.Tours).Returns(_mockTourRepository.Object);

            _controller = new ImageController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");

            AutoMapperConfiguration.Configure();
        }

        [TestMethod]
        public void GetImage_ValidRequest_ShouldReturnImage()
        {
            var image = new Image();
            _mockRepository.Setup(r => r.GetImage("1")).Returns(image);

            var result = _controller.GetImage("1");

            result.Should().BeOfType<OkNegotiatedContentResult<ImageDto>>();
        }

        [TestMethod]
        public void GetImage_NoImageFound_ShouldReturnNotound()
        {
            var result = _controller.GetImage("1");

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void UpdateImage_ValidRequest_ShouldReturnOk()
        {
            var image = new Image();
            _mockRepository.Setup(r => r.GetImage("1")).Returns(image);

            var imageDto = new ImageDto {Title = "Updated Title", Caption = "Caption", Query = "Query", Confirmed = true};

            var result = _controller.UpdateImage("1", imageDto);

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void UpdateImage_AddImageToTour_ShouldReturnOk()
        {
            var image = new Image();
            var tour = new Tour();
            _mockRepository.Setup(r => r.GetImage("1")).Returns(image);
            _mockTourRepository.Setup(r => r.GetTour("1")).Returns(tour);

            var imageDto = new ImageDto {TourId = "1"};

            var result = _controller.UpdateImage("1", imageDto);

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void UpdateImage_TourDoesNotExist_ShouldReturnNotFound()
        {
            var image = new Image();
            _mockRepository.Setup(r => r.GetImage("1")).Returns(image);

            var imageDto = new ImageDto {TourId = "1"};

            var result = _controller.UpdateImage("1", imageDto);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void UpdateImage_NoImageToUpdate_ShouldReturnNotFound()
        {
            var imageDto = new ImageDto{Title = "Test"};

            var result = _controller.UpdateImage("1", imageDto);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void DeleteImage_NoImageToDelete_ShouldReturnNotFound()
        {
            var result = _controller.DeleteImage("1");

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void DeleteImage_ValidRequest_ShouldReturnOk()
        {
            var image = new Image();
            _mockRepository.Setup(r => r.GetImage("1")).Returns(image);

            var result = _controller.DeleteImage("1");

            result.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [TestMethod]
        public void AddImage_ValidRequest_ShouldReturnOk()
        {
            var image = new Image();
            _mockRepository.Setup(i => i.AddImage(image)).Returns("1");

            var imageDto = new ImageDto();
            var result = _controller.AddImage(imageDto);

            result.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [TestMethod]
        public void TempTestMethod()
        {
            var path = HostingEnvironment.MapPath(
                @"~\uploads\images\dc1dfe37-b189-4ec3-8fdf-dab93f9848f5\2ef0a8649aa642eda64f42e7a5177fb7.jpg");

            Console.WriteLine(path);
            Assert.IsTrue(true);
        }
    }
}
