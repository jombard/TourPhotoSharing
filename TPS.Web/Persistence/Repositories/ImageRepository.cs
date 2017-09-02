using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using TPS.Web.Core.Models;
using TPS.Web.Core.Repositories;
using Image = TPS.Web.Core.Models.Image;

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
                .Where(i => i.OwnerId == userId).Include(u => u.Owner).ToList();
        }

        public Image GetImage(string imageId)
        {
            return _context.Images.SingleOrDefault(i => i.Id.ToString() == imageId);
        }

        public void Remove(Image image)
        {
            _context.Images.Remove(image);

            var fullFilePath = HostingEnvironment.MapPath(image.ImageUrl);
            if (fullFilePath != null && File.Exists(fullFilePath))
            {
                File.Delete(fullFilePath);
            }
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
            var fullFilePath = HostingEnvironment.MapPath(image.ImageUrl);
            if (fullFilePath != null && File.Exists(fullFilePath))
            {
                using (var i = System.Drawing.Image.FromFile(fullFilePath))
                {
                    i.RotateFlip(RotateFlipType.Rotate90FlipNone);

                    i.Save(fullFilePath);
                }

                var width = image.Width;
                image.Width = image.Height;
                image.Height = width;
            }
        }
    }
}