using System.Collections.Generic;
using TPS.Web.Core.Dtos;
using TPS.Web.Core.Models;

namespace TPS.Web.Core.Repositories
{
    public interface IImageRepository
    {
        IEnumerable<Image> GetUserImages(string userId);
        Image GetImage(string imageId);
        void Remove(Image image);
        string AddImage(ImageDto imageDto);
    }
}
