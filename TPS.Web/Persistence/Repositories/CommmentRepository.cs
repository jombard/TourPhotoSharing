using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TPS.Web.Core.Models;
using TPS.Web.Core.Repositories;

namespace TPS.Web.Persistence.Repositories
{
    public class CommmentRepository : ICommentRepository
    {
        private readonly IApplicationDbContext _context;

        public CommmentRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Comment> GetTourComments(string tourId)
        {
            return _context.Comments
                .Where(c => c.TourId.ToString() == tourId)
                .Include(u => u.Commenter)
                .ToList();
        }

        public string AddComment(Comment comment)
        {
            var savedComment = _context.Comments.Add(comment);
            return savedComment.Id.ToString();
        }

        public Comment GetComment(string commentId, string userId)
        {
            return _context.Comments
                .SingleOrDefault(i => i.Id.ToString() == commentId);
        }

        public void Remove(Comment comment)
        {
            _context.Comments.Remove(comment);
        }
    }
}