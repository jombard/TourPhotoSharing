using System.Collections.Generic;
using TPS.Web.Core.Models;

namespace TPS.Web.Core.ViewModels
{
    public class ImageListViewModel
    {
        public readonly List<ImageViewModel> ConfirmedImages;
        public readonly List<ImageViewModel> PendingImages;

        public ImageListViewModel(IEnumerable<Image> images)
        {
            ConfirmedImages = new List<ImageViewModel>();
            PendingImages = new List<ImageViewModel>();

            foreach (var image in images)
            {
                if (image.Confirmed)
                {
                    ConfirmedImages.Add(new ImageViewModel(image));
                }
                else
                {
                    PendingImages.Add(new ImageViewModel(image));
                }
            }
        }
    }
}