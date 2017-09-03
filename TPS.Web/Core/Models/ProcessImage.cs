using ImageResizer;
using System.Linq;
using System.Web;

namespace TPS.Web.Core.Models
{
    public class ProcessImage
    {
        public int? Width => _compressedImg.FinalWidth;

        public int? Height => _compressedImg.FinalHeight;

        public string Path => _filePath + _compressedImg.FinalPath.Split('\\').Last();

        private readonly HttpPostedFileBase _image;

        private readonly string _filePath;

        private ImageJob _compressedImg;

        public ProcessImage(HttpPostedFileBase file, string filePath)
        {
            _image = file;
            _filePath = filePath;

            Process();
        }

        private void Process()
        {
            var resizeSettings = GetResizeSettings();

            var instructions = new Instructions(resizeSettings);

            var imageJob = new ImageJob(_image, _filePath + "<guid>", instructions, true, true)
            {
                CreateParentDirectory = true
            };

            _compressedImg = ImageBuilder.Current.Build(imageJob);
        }

        private static ResizeSettings GetResizeSettings()
        {
            var resizeSettings = new ResizeSettings
            {
                MaxWidth = 1900,
                MaxHeight = 1900,
                Format = "jpg"
            };
            resizeSettings.Set("autorotate", "true");
            return resizeSettings;
        }
    }
}