using System.Linq;
using TPS.Web.Core.Models;
using TPS.Web.Core.Repositories;

namespace TPS.Web.Persistence.Repositories
{
    public class StarRepository : IStarRepository
    {
        private readonly IApplicationDbContext _context;

        public StarRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddStar(Image image, string userId)
        {
            var star = _context.StarredImages.SingleOrDefault(s => s.Image.Id == image.Id && s.User.Id == userId);

            if (star != null)
                return false;

            star = new StarredImages
            {
                Image = image,
                User = _context.Users.Find(userId)
            };

            _context.StarredImages.Add(star);

            return true;
        }

        public bool RemoveStar(Image image, string userId)
        {
            var star = _context.StarredImages.SingleOrDefault(s => s.Image.Id == image.Id && s.User.Id == userId);

            if (star != null)
                return false;

            star = new StarredImages
            {
                Image = image,
                User = _context.Users.Find(userId)
            };

            _context.StarredImages.Remove(star);

            return true;
        }
    }
}