using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Audit> GetAudits()
        {
            return _context.AuditRecords.OrderByDescending(t => t.Timestamp).ToList();
        }

        public Audit GetAudit(string auditId)
        {
            return _context.AuditRecords.Find(auditId);
        }
    }
}