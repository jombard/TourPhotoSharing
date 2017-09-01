using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace TPS.Web.Core.Models
{
    public class ImageProperties
    {
        private readonly System.Drawing.Image _sourceImage;

        public ImageProperties(HttpPostedFileBase file)
        {
            file.InputStream.Seek(0, SeekOrigin.Begin);
            _sourceImage = System.Drawing.Image.FromStream(file.InputStream, true, true);
        }

        public float? Latitude => GetImageProperties(1,2);

        public float? Longitude => GetImageProperties(3,4);

        public DateTime? DateCreated => GetImageDate();


        private float? GetImageProperties(int propRef, int propVal)
        {
            if (_sourceImage.PropertyIdList.All(x => x != propRef))
                return null;

            var propItemRef = _sourceImage.GetPropertyItem(propRef);
            var propItemVal = _sourceImage.GetPropertyItem(propVal);
            return ExifGpsToFloat(propItemRef, propItemVal);
        }

        private static float ExifGpsToFloat(PropertyItem propItemRef, PropertyItem propItem)
        {
            var degrees = GetGpsCoord(propItem, 0, 4);
            var minutes = GetGpsCoord(propItem, 8, 12);
            var seconds = GetGpsCoord(propItem, 16, 20);

            var coordinate = degrees + (minutes / 60f) + (seconds / 3600f);
            var gpsRef = Encoding.ASCII.GetString(new[] { propItemRef.Value[0] }); // N, S, E, or W
            if (gpsRef == "S" || gpsRef == "W")
                coordinate = 0 - coordinate;

            return coordinate;
        }

        private static float GetGpsCoord(PropertyItem propItem, int numIdx, int denIdx)
        {
            var numerator = BitConverter.ToUInt32(propItem.Value, numIdx);
            var denominator = BitConverter.ToUInt32(propItem.Value, denIdx);
            return numerator / (float)denominator;
        }

        private DateTime? GetImageDate()
        {
            var r = new Regex(":");
            try
            {
                if (_sourceImage.PropertyIdList.All(p => p != 36867))
                    return null;

                var propItem = _sourceImage.GetPropertyItem(36867);
                var dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                return DateTime.Parse(dateTaken);
            }
            catch (ArgumentException)
            {
                return null;
            }

        }
    }
}