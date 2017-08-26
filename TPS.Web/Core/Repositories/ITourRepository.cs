using System.Collections.Generic;
using TPS.Web.Core.Models;

namespace TPS.Web.Core.Repositories
{
    public interface ITourRepository
    {
        IEnumerable<Tour> GetTours(string userId);
        Tour GetTour(string id);
        void AddTour(Tour tour);
        List<TourImages> GetImages(string id);
    }
}