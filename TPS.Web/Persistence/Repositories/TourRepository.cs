using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TPS.Web.Core.Models;
using TPS.Web.Core.Repositories;

namespace TPS.Web.Persistence.Repositories
{
    public class TourRepository : ITourRepository
    {
        private readonly IApplicationDbContext _context;

        public TourRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Tour> GetTours(string userId)
        {
            return _context.Tours.ToList();
        }

        public Tour GetTour(string id)
        {
            return _context.Tours.SingleOrDefault(t => t.Id.ToString() == id);
        }

        public void AddTour(Tour tour)
        {
            _context.Tours.Add(tour);
        }

        public List<TourImages> GetImages(string id)
        {
            return _context.TourImages
                .Where(t => t.Tour.Id.ToString() == id && t.Image.Confirmed)
                .Include(i => i.Image)
                .Include(u => u.Image.Owner)
                .Include(s => s.Image.StarredUsers)
                .OrderBy(t => t.Image.CreatedDate)
                .ToList();
        }
    }
}