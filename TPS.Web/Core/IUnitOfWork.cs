using System;
using TPS.Web.Core.Repositories;

namespace TPS.Web.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IImageRepository Images { get; }
        ITourRepository Tours { get; }
        ICommentRepository Comments { get; }
        IAuditRepository AuditRecords { get; }

        void Complete();
    }
}