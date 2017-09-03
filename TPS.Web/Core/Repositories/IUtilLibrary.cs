using System.Web;
using TPS.Web.Core.Models;
using Image = TPS.Web.Core.Models.Image;

namespace TPS.Web.Core.Repositories
{
    public interface IUtilLibrary
    {
        bool Rotate(Image image);

        bool Delete(Image image);

        ProcessImage ProcessImage(HttpPostedFileBase file, string filepath);

        ImageProperties ImageProperties(HttpPostedFileBase file);
    }
}