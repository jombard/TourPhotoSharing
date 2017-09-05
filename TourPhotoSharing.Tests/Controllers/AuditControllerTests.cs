using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using TPS.Web.Controllers;

namespace TourPhotoSharing.Tests.Controllers
{
    [TestClass]
    public class AuditControllerTests
    {
        private AuditController _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            _controller = new AuditController();
        }

        [TestMethod]
        public void Index_View_ShouldReturnView()
        {
            var result = _controller.Index() as ViewResult;

            result.Should().NotBeNull();
            result.ViewName.Should().Be("");
        }
    }
}
