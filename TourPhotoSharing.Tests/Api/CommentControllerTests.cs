using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
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
    public class CommentControllerTests
    {
        private CommentsController _controller;
        private Mock<ICommentRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<ICommentRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(u => u.Comments).Returns(_mockRepository.Object);

            _controller = new CommentsController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");

            AutoMapperConfiguration.Configure();
        }

        [TestMethod]
        public void GetTourComments_ValidRequest_ShouldReturnComments()
        {
            var comments = new List<Comment> {new Comment(), new Comment(), new Comment()};

            _mockRepository.Setup(r => r.GetTourComments("1")).Returns(comments);

            var result = _controller.GetTourComments("1");

            result.Should().BeOfType<OkNegotiatedContentResult<IEnumerable<Comment>>>();
        }

        [TestMethod]
        public void AddComment_ValidRequest_ShouldReturnOk()
        {
            var comment = new Comment();
            _mockRepository.Setup(i => i.AddComment(comment)).Returns("1");

            var commentDto = new CommentDto();
            var result = _controller.AddComment(commentDto);

            result.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [TestMethod]
        public void DeleteComment_ValidRequest_ShouldReturnOk()
        {
            var comment = new Comment();
            _mockRepository.Setup(r => r.GetComment("1", _userId)).Returns(comment);

            var result = _controller.DeleteComment("1");

            result.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [TestMethod]
        public void DeleteComment_CommentNotFOund_ShouldReturnNotFoundError()
        {
            var result = _controller.DeleteComment("1");

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
