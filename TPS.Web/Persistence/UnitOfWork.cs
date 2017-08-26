using TPS.Web.Core;
using TPS.Web.Core.Repositories;
using TPS.Web.Persistence.Repositories;

namespace TPS.Web.Persistence
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IImageRepository Images { get; }
        public ITourRepository Tours { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Images = new ImageRepository(context);
            Tours = new TourRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}