using TPS.Web.Core;
using TPS.Web.Core.Libraries;
using TPS.Web.Core.Repositories;
using TPS.Web.Persistence.Repositories;

namespace TPS.Web.Persistence
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IImageRepository Images { get; }
        public ITourRepository Tours { get; }
        public ICommentRepository Comments { get; }
        public IAuditRepository AuditRecords { get; }
        public IStarRepository StarredImages { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            var utilLibrary = new UtilLibrary();

            Images = new ImageRepository(context, utilLibrary);
            Tours = new TourRepository(context);
            Comments = new CommmentRepository(context);
            AuditRecords = new AuditRepository(context);
            StarredImages = new StarRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}