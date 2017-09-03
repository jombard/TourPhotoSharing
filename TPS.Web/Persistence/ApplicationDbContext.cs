using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TPS.Web.Core.Models;

namespace TPS.Web.Persistence
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<Image> Images { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourImages> TourImages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Audit> AuditRecords { get; set; }
        public DbSet<StarredImages> StarredImages { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}