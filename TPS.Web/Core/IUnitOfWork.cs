using TPS.Web.Core.Repositories;

namespace TPS.Web.Core
{
    public interface IUnitOfWork
    {
        IImageRepository Images { get; }
        ITourRepository Tours { get; }
        ICommentRepository Comments { get; }

        void Complete();
    }
}