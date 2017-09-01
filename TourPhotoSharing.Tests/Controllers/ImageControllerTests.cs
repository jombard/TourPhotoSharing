using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using TPS.Web.App_Start;
using TPS.Web.Controllers;
using TPS.Web.Core;
using TPS.Web.Core.Models;
using TPS.Web.Core.Repositories;

namespace TourPhotoSharing.Tests.Controllers
{
    [TestClass]
    public class ImageControllerTests
    {
        private ImageController _controller;
        private Mock<IImageRepository> _mockRepository;
        private string _username;
        private Mock<HttpPostedFileBase> _postedfile;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IImageRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(u => u.Images).Returns(_mockRepository.Object);

            _controller = new ImageController(mockUoW.Object);

            AutoMapperConfiguration.Configure();
            _username = "user1@domain.com";

            HttpContext.Current = ApiControllerExtensions.FakeHttpContext();

            var request = new Mock<HttpRequestBase>();
            var context = new Mock<HttpContextBase>();
            _postedfile = new Mock<HttpPostedFileBase>();

            var postedfilesKeyCollection = new Mock<HttpFileCollectionBase>();
            var fakeFileKes = new List<string>() {"file"};

            context.SetupGet(x => x.Request).Returns(request.Object);
            request.SetupGet(r => r.Files).Returns(postedfilesKeyCollection.Object);

            postedfilesKeyCollection.As<IEnumerable>().Setup(k => k.GetEnumerator()).Returns(fakeFileKes.GetEnumerator());
            postedfilesKeyCollection.SetupGet(k => k["file"]).Returns(_postedfile.Object);

            _postedfile.SetupGet(f => f.ContentLength).Returns(8192).Verifiable();
            _postedfile.SetupGet(f => f.FileName).Returns("foo.jpeg").Verifiable();

            //postedfile.SetupGet<string>(f => f.SaveAs(It.IsAny<string>())).Verifiable();

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns(_username);
            //controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            _controller.ControllerContext = controllerContext.Object;


            context.SetupGet(x => x.User).Returns(principal.Object);

            //_controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), _controller);
            controllerContext.SetupGet(c => c.HttpContext).Returns(context.Object);
        }

        [TestMethod]
        public void ConfirmUpload_View_ReturnViewResult()
        {
            var result = _controller.ConfirmUpload();

            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public void Index_ViewOwnImages_ReturnViewResult()
        {
            _mockRepository.Setup(r => r.GetUserImages("1")).Returns(new List<Image>());

            var result = _controller.Index();

            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public void Confirm_ViewPendingImages_ReturnViewResult()
        {
            _mockRepository.Setup(r => r.GetPending()).Returns(new List<Image>());

            var result = _controller.Confirm();

            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public void Upload_ViewUploadPage_ReturnViewResult()
        {
            var result = _controller.Upload();

            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public void Upload_SaveImage_ReturnImageId()
        {
            _mockRepository.Setup(x => x.UploadUserImage(_postedfile.Object, "1"));
            var result = _controller.UploadImage();

            result.Should().BeOfType<JsonResult>();
        }
    }
}
