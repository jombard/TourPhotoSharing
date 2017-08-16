using System.Collections.Generic;
using System.Linq;
using TPS.Web.Core.Dtos;
using TPS.Web.Core.Models;
using TPS.Web.Core.Repositories;

namespace TPS.Web.Persistence.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IApplicationDbContext _context;

        public ImageRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Image> GetUserImages(string userId)
        {
            return _context.Images
                .Where(i => i.OwnerId == userId).ToList();
        }

        public Image GetImage(string imageId, string userId)
        {
            return _context.Images
                .SingleOrDefault(i => i.Id.ToString() == imageId && i.OwnerId == userId);
        }

        public void Remove(Image image)
        {
            _context.Images.Remove(image);
        }

        public string AddImage(ImageDto imageDto)
        {
            var image = new Image(imageDto);
            var savedImage = _context.Images.Add(image);
            return savedImage.Id.ToString();
        }
    }
}