using System.Collections.Generic;
using TPS.Web.Core.Models;

namespace TPS.Web.Core.ViewModels
{
    public class ImageListViewModel
    {
        public readonly List<ImageViewModel> Images;

        public ImageListViewModel(IEnumerable<Image> images)
        {
            Images = new List<ImageViewModel>();

            foreach (var image in images)
            {
                Images.Add(new ImageViewModel(image));
            }
        }
    }
}