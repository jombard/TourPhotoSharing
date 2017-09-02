using System.Collections.Generic;
using System.Web;
using TPS.Web.Core.Models;

namespace TPS.Web.Core.Repositories
{
    public interface IImageRepository
    {
        IEnumerable<Image> GetUserImages(string userId);
        Image GetImage(string imageId);
        void Remove(Image image);
        string AddImage(Image image);
        IEnumerable<Image> GetPending();
        Image UploadUserImage(HttpPostedFileBase file, string userId);
        void AddImageToTour(Image image, Tour tour);
        void Rotate(Image image);
    }
}
