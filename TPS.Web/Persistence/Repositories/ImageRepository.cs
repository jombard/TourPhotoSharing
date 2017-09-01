using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPS.Web.Core.Models;
using TPS.Web.Core.Repositories;

namespace TPS.Web.Persistence.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IApplicationDbContext _context;

        public ImageRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Image> GetUserImages(string userId)
        {
            return _context.Images
                .Where(i => i.OwnerId == userId).ToList();
        }

        public Image GetImage(string imageId)
        {
            return _context.Images.SingleOrDefault(i => i.Id.ToString() == imageId);
        }

        public void Remove(Image image)
        {
            _context.Images.Remove(image);
        }

        public string AddImage(Image image)
        {
            var savedImage = _context.Images.Add(image);
            return savedImage.Id.ToString();
        }

        public IEnumerable<Image> GetPending()
        {
            return _context.Images.Where(i => !i.Confirmed).ToList();
        }

        public Image UploadUserImage(HttpPostedFileBase file, string userId)
        {
            var filepath = $@"~\uploads\images\{userId}\";

            var compressImage = new ProcessImage(file, filepath);

            var imageProps = new ImageProperties(file);

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
    }
}