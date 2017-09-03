using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TPS.Web.Core.Models;
using TPS.Web.Core.Repositories;
using Image = TPS.Web.Core.Models.Image;

namespace TPS.Web.Persistence.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly IUtilLibrary _util;

        public ImageRepository(IApplicationDbContext context, IUtilLibrary util)
        {
            _context = context;
            _util = util;
        }

        public IEnumerable<Image> GetUserImages(string userId)
        {
            return _context.Images
                .Where(i => i.OwnerId == userId).Include(u => u.Owner).ToList();
        }

        public Image GetImage(string imageId)
        {
            return _context.Images.SingleOrDefault(i => i.Id.ToString() == imageId);
        }

        public void Remove(Image image)
        {
            _context.Images.Remove(image);

            _util.Delete(image);
        }

        public string AddImage(Image image)
        {
            var savedImage = _context.Images.Add(image);
            return savedImage.Id.ToString();
        }

        public IEnumerable<Image> GetPending()
        {
            return _context.Images.Where(i => !i.Confirmed).Include(u => u.Owner).ToList();
        }

        public Image UploadUserImage(HttpPostedFileBase file, string userId)
        {
            var filepath = $@"~\uploads\images\{userId}\";

            var compressImage = _util.ProcessImage(file, filepath);

            var imageProps = _util.ImageProperties(file);

            var image = new Image
            {
                ImageUrl = compressImage.Path,
                Height = compressImage.Height,
                Width = compressImage.Width,
                Title = file.FileName,
                ImageMimeType = file.ContentType,
                Confirmed = false,
                OwnerId = userId,
                Latitude = imageProps.Latitude,
                Longitude = imageProps.Longitude,
                CreatedDate = imageProps.DateCreated,
                UploadDate = DateTime.Now
            };

            return image;
        }

        public void AddImageToTour(Image image, Tour tour)
        {
            var tourImage = new TourImages
            {
                Image = image,
                Tour = tour
            };
            _context.TourImages.Add(tourImage);
        }

        public void Rotate(Image image)
        {
            var result = _util.Rotate(image);

            if (result)
            {
                var width = image.Width;
                image.Width = image.Height;
                image.Height = width;
            }
        }
    }
}