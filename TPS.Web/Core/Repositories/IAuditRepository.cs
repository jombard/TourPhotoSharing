using TPS.Web.Core.Models;

namespace TPS.Web.Core.Repositories
{
    public interface IAuditRepository
    {
        void Add(Audit audit);
    }
}