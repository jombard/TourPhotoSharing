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
    public class AuditControllerTests
    {
        private AuditsController _controller;
        private Mock<IAuditRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IAuditRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(u => u.AuditRecords).Returns(_mockRepository.Object);

            _controller = new AuditsController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");

            AutoMapperConfiguration.Configure();
        }

        [TestMethod]
        public void GetAudits_ValidRequest_ShouldReturnAudits()
        {
            IEnumerable<Audit> audits = new List<Audit>();
            _mockRepository.Setup(r => r.GetAudits()).Returns(audits);

            var result = _controller.GetAuditRecords();

            result.Should().BeOfType<OkNegotiatedContentResult<IEnumerable<AuditDto>>>();
        }

        [TestMethod]
        public void GetAudit_ValidRequest_ShouldReturnAudit()
        {
            var audit = new Audit();
            _mockRepository.Setup(r => r.GetAudit("1")).Returns(audit);

            var result = _controller.GetAudit("1");

            result.Should().BeOfType<OkNegotiatedContentResult<AuditDto>>();
        }

        [TestMethod]
        public void GetAudit_AuditNotFound_ShouldReturnNotFound()
        {
            var result = _controller.GetAudit("1");

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
