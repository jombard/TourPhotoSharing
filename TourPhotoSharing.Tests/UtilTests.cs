using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPS.Web.Core.Models;

namespace TourPhotoSharing.Tests
{
    [TestClass]
    public class UtilTests
    {
        [TestMethod]
        public void SanitiseString_ValidString_ShouldReturnSanitisedString()
        {
            // Arrange
            var invalidString = "Spaces and !nvalid & Chars * that won:t work for ^ urls";

            // Act
            var result = invalidString.Sanitise();

            // Assert
            result.Should()
                .NotContain("!").And
                .NotContain("&").And
                .NotContain("*").And
                .NotContain(":").And
                .NotContain("^");
        }

        [TestMethod]
        public void SanitiseString_ValidProblemString_ShouldReturnSanitisedString()
        {
            // Arrange
            var invalidString = "A&L FB.jpg";

            // Act
            var result = invalidString.Sanitise();
            
            // Assert
            result.Should()
                .NotContain("!").And
                .NotContain("&").And
                .NotContain("*").And
                .NotContain(":").And
                .NotContain("^").And
                .Should().NotBe("jpg");
        }
    }
}
