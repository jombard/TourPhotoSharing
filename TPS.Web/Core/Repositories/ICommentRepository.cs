using System.Collections.Generic;
using TPS.Web.Core.Models;

namespace TPS.Web.Core.Repositories
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetTourComments(string tourId);
        string AddComment(Comment commentDto);
        Comment GetComment(string commentId, string userId);
        void Remove(Comment comment);
    }
}