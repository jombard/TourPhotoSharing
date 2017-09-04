using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;
using System.Linq;
using TPS.Web.Core.Models;
using TPS.Web.Core.Repositories;
using TPS.Web.Persistence;
using TPS.Web.Persistence.Repositories;

namespace TourPhotoSharing.Tests.Repositories
{
    [TestClass]
    public class ImageRepositoryTests
    {
        private ImageRepository _repository;
        private Mock<DbSet<Image>> _mockImages;

        [TestInitialize]
        public void TestInitialise()
        {
            _mockImages = new Mock<DbSet<Image>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Images).Returns(_mockImages.Object);

            var util = new Mock<IUtilLibrary>();

            _repository = new ImageRepository(mockContext.Object, util.Object);
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
        public void GetImage_GetRequestedImageDetails_ShouldBeReturned()
        {
            var guid = Guid.Parse("48773252-fab0-4aaa-ba7a-30f6f1c8b73a");
            var mockImage = new Image{OwnerId = "1",Id = guid };
            var mockOtherImage = new Image{OwnerId = "2", Id= Guid.NewGuid()};
            var mockImages = new[]{mockImage,mockOtherImage};
            _mockImages.SetSource(mockImages);

            var image = _repository.GetImage(guid.ToString());

            image.Should().BeSameAs(mockImage);
            image.Should().NotBeSameAs(mockOtherImage);
        }

        [TestMethod]
        public void GetUserImages_UsersOwnImages_ShouldBeReturned()
        {
            var mockImage = new Image{OwnerId = "1"};
            var mockOtherImage = new Image{OwnerId = "2"};
            var mockImages = new[]{mockImage,mockOtherImage};
            _mockImages.SetSource(mockImages);

            var images = _repository.GetUserImages("1").ToList();

            images.Should().Contain(mockImage);
            images.Should().NotContain(mockOtherImage);
        }

    }
}