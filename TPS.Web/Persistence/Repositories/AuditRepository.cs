using TPS.Web.Core.Models;
using TPS.Web.Core.Repositories;

namespace TPS.Web.Persistence.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly IApplicationDbContext _context;

        public AuditRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Audit audit)
        {
            _context.AuditRecords.Add(audit);
        }
    }
}