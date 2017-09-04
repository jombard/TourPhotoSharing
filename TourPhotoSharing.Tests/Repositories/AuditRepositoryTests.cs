using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;
using TPS.Web.Core.Models;
using TPS.Web.Persistence;
using TPS.Web.Persistence.Repositories;

namespace TourPhotoSharing.Tests.Repositories
{
    [TestClass]
    public class AuditRepositoryTests
    {
        private AuditRepository _repository;
        private Mock<DbSet<Audit>> _mockAudits;

        [TestInitialize]
        public void TestInitialise()
        {
            _mockAudits = new Mock<DbSet<Audit>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.AuditRecords).Returns(_mockAudits.Object);

            _repository = new AuditRepository(mockContext.Object);
        }

        //[TestMethod]
        //public void Add_AddAuditToDb()
        //{
        //    var guid = Guid.NewGuid();
        //    var audit = new Audit
        //    {
        //        AuditId = guid,
        //        AreaAccessed = "",
        //        Data = "",
        //        IPAddress = "",
        //        SessionId = "",
        //        Timestamp = DateTime.Now,
        //        UserName = "TestUser"
        //    };
        //    _repository.Add(audit);

        //    var auditInDb = _mockAudits.Object.SingleOrDefault(a => a.AuditId == guid);

        //    auditInDb.Should().BeSameAs(audit);
        //}

        [TestMethod]
        public void GetAudits_ReturnListOfAudits()
        {
            var mockAudits = new[]
            {
                new Audit {Timestamp = DateTime.Now},
                new Audit {Timestamp = DateTime.Now.AddDays(-1)},
                new Audit {Timestamp = DateTime.Now.AddDays(1)}
            };
            _mockAudits.SetSource(mockAudits);

            var audits = _repository.GetAudits();

            audits.Should().BeInDescendingOrder(a => a.Timestamp);
        }

        //[TestMethod]
        //public void GetAudit_ReturnSpecifiedAudit()
        //{
        //    var guid = Guid.NewGuid();
        //    var mockAudits = new[]
        //    {
        //        new Audit {AuditId = Guid.NewGuid(), Timestamp = DateTime.Now},
        //        new Audit {AuditId = Guid.NewGuid(), Timestamp = DateTime.Now.AddDays(-1)},
        //        new Audit {AuditId = guid, Timestamp = DateTime.Now.AddDays(1)}
        //    };
        //    _mockAudits.SetSource(mockAudits);

        //    var audit = _repository.GetAudit(guid.ToString());

        //    audit.Should().NotBeNull();
        //}
    }
}
