using ImageResizer;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using TPS.Web.Core.Models;
using TPS.Web.Core.ViewModels;
using TPS.Web.Persistence;
using Image = TPS.Web.Core.Models.Image;

namespace TPS.Web.Controllers
{
    [Authorize]
    public class ImageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string Filepath = "~\\uploads\\images\\";
        private const string Guid = "<guid>";

        public ImageController()
        {
            _context = new ApplicationDbContext();
        }

        public FileContentResult GetThumbnailImage(string id)
        {
            var image = _context.Images.FirstOrDefault(i => i.Id.ToString() == id);
            return image == null ? null : File(image.Thumbnail, image.ImageMimeType);
        }

        public ActionResult Upload()
        {
            return View();
        }

        public ActionResult SaveUploadedFile()
        {
            var fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    var file = Request.Files[fileName];

                    //Save file content goes here
                    if (file != null && file.ContentLength > 0)
                    {
                        fName = file.FileName;

                        var sourceImage = System.Drawing.Image.FromStream(file.InputStream, true, true);
                        var latitude = GetLatitude(sourceImage);
                        var longitude = GetLongitude(sourceImage);
                        var dateTaken = GetDateTaken(sourceImage);
                        
                        var resizeSettings = new ResizeSettings
                        {
                            MaxWidth = 1900,
                            MaxHeight = 1900,
                            Format = "jpg"
                        };

                        file.InputStream.Seek(0, SeekOrigin.Begin);

                        var imageJob = new ImageJob(file, Filepath + Guid, new Instructions(resizeSettings), true, true);
                        var compressedImg = ImageBuilder.Current.Build(imageJob);

                        var url = Filepath + compressedImg.FinalPath.Split('\\').Last();
                        
                        var image = new Image
                        {
                            ImageUrl = url,
                            Title = file.FileName,
                            CreatedDate = dateTaken,
                            ImageMimeType = file.ContentType,
                            Confirmed = false,
                            OwnerId = User.Identity.GetUserId(),
                            Latitude = latitude,
                            Longitude = longitude,
                            UploadDate = DateTime.Now
                        };
                        
                        _context.Images.Add(image);

                        var lejogTour = _context.Tours.SingleOrDefault(t => t.Name == "LEJOG");

                        var tourImage = new TourImages
                        {
                            Image = image,
                            Tour = lejogTour
                        };

                        _context.TourImages.Add(tourImage);
                    }
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Error in saving file: " + ex.Message });
            }

            return Json(new { FileName = fName });
        }

        private static DateTime? GetDateTaken(System.Drawing.Image sourceImage)
        {
            var r = new Regex(":");
            try
            {
                if (!sourceImage.PropertyIdList.Any(p => p == 36867))
                    return null;

                var propItem = sourceImage.GetPropertyItem(36867);
                var dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                return DateTime.Parse(dateTaken);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        private static float? GetLatitude(System.Drawing.Image targetImage)
        {
            try
            {
                var propItemLatRef = targetImage.GetPropertyItem(1);
                var propItemLat = targetImage.GetPropertyItem(2);
                return ExifGpsToFloat(propItemLatRef, propItemLat);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        private static float? GetLongitude(System.Drawing.Image targetImage)
        {
            try
            {
                var propItemLongRef = targetImage.GetPropertyItem(3);
                var propItemLong = targetImage.GetPropertyItem(4);
                return ExifGpsToFloat(propItemLongRef, propItemLong);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        private static float ExifGpsToFloat(PropertyItem propItemRef, PropertyItem propItem)
        {
            var degreesNumerator = BitConverter.ToUInt32(propItem.Value, 0);
            var degreesDenominator = BitConverter.ToUInt32(propItem.Value, 4);
            var degrees = degreesNumerator / (float)degreesDenominator;

            var minutesNumerator = BitConverter.ToUInt32(propItem.Value, 8);
            var minutesDenominotor = BitConverter.ToUInt32(propItem.Value, 12);
            var minutes = minutesNumerator / (float) minutesDenominotor;

            var secondsNumerator = BitConverter.ToUInt32(propItem.Value, 16);
            var secondsDenominotor = BitConverter.ToUInt32(propItem.Value, 20);
            var seconds = secondsNumerator / (float)secondsDenominotor;

            var coordinate = degrees + (minutes / 60f) + (seconds / 3600f);
            var gpsRef = System.Text.Encoding.ASCII.GetString(new byte[1] {propItemRef.Value[0]}); // N, S, E, or W
            if(gpsRef == "S" || gpsRef == "W")
                coordinate = 0 - coordinate;

            return coordinate;
        }

        public ActionResult Confirm()
        {
            var userId = User.Identity.GetUserId();
            var images = _context.Images.Where(i => i.OwnerId == userId && !i.Confirmed).ToList();

            var viewModel = new List<ImageViewModel>();
            foreach (var image in images)
            {
                viewModel.Add(new ImageViewModel(image));
            }

            return View(viewModel);
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var images = _context.Images
                .Where(i => i.OwnerId == userId && i.Confirmed)
                .OrderByDescending(d => d.CreatedDate)
                .ToList();

            var viewModel = new List<ImageViewModel>();
            foreach (var image in images)
            {
                viewModel.Add(new ImageViewModel(image));
            }

            return View(viewModel);
        }

    }
}