using TPS.Web.Core.Models;

namespace TPS.Web.Core.Repositories
{
    public interface IStarRepository
    {
        bool AddStar(Image image, string userId);
        bool RemoveStar(Image image, string userId);
    }
}