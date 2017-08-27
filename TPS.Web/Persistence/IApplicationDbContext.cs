using System.Data.Entity;
using TPS.Web.Core.Models;

namespace TPS.Web.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Image> Images { get; set; }
        DbSet<Tour> Tours { get; set; }
        DbSet<TourImages> TourImages { get; set; }
        DbSet<Comment> Comments { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }
    }
}
