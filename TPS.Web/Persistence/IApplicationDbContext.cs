using System.Data.Entity;
using TPS.Web.Core.Models;

namespace TPS.Web.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Image> Images { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }
    }
}
