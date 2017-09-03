using System.Collections.Generic;
using TPS.Web.Core.Models;

namespace TPS.Web.Core.Repositories
{
    public interface IAuditRepository
    {
        void Add(Audit audit);
        IEnumerable<Audit> GetAudits();
        Audit GetAudit(string auditId);
    }
}