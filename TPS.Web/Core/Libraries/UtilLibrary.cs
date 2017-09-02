using System;
using System.Drawing;
using System.IO;
using System.Web.Hosting;
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
                var fullFilePath = HostingEnvironment.MapPath(image.ImageUrl);
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
                var fullFilePath = HostingEnvironment.MapPath(image.ImageUrl);
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
    }
}