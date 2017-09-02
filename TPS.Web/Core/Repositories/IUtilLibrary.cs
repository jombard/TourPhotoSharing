using Image = TPS.Web.Core.Models.Image;

namespace TPS.Web.Core.Repositories
{
    public interface IUtilLibrary
    {
        bool Rotate(Image image);

        bool Delete(Image image);
    }
}