using ImageResizer;
using System;
using System.Drawing; // TODO remove this ref http://imageresizing.net/docs/v4/docs/best-practices
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using TPS.Web.Core.Models;
using TPS.Web.Core.Repositories;
using Image = TPS.Web.Core.Models.Image;

namespace TPS.Web.Core.Libraries
{
    public class UtilLibrary : IUtilLibrary
    {
        public bool Rotate(Image image)
        {
            try
            {
                var fullFilePath = GetFullFilePath(image.ImageUrl);
                if (fullFilePath != null && File.Exists(fullFilePath))
                {
                    using (var i = System.Drawing.Image.FromFile(fullFilePath))
                    {
                        i.RotateFlip(RotateFlipType.Rotate90FlipNone);

                        i.Save(fullFilePath);
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public bool Delete(Image image)
        {
            try
            {
                var fullFilePath = GetFullFilePath(image.ImageUrl);
                if (fullFilePath != null && File.Exists(fullFilePath))
                {
                    File.Delete(fullFilePath);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        private static string GetFullFilePath(string imageUrl)
        {
            return HostingEnvironment.MapPath(imageUrl);
        }

        public ProcessImage ProcessImage(HttpPostedFileBase file, string filepath)
        {
            var resizeSettings = new ResizeSettings
            {
                MaxWidth = 2048,
                MaxHeight = 2048,
                Format = "jpg"
            };
            resizeSettings.Set("autorotate", "true");

            var instructions = new Instructions(resizeSettings);

            var imageJob = new ImageJob(file, filepath + "<guid>", instructions, true, true)
            {
                CreateParentDirectory = true
            };

            var image = ImageBuilder.Current.Build(imageJob);

            return new ProcessImage
            {
                Path = filepath + image.FinalPath.Split('\\').Last(),
                Width = image.FinalWidth,
                Height = image.FinalHeight
            };
        }

        public ImageProperties ImageProperties(HttpPostedFileBase file)
        {
            return new ImageProperties(file);
        }
    }
}