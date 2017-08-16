using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using TPS.Web.Controllers;

namespace TourPhotoSharing.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
