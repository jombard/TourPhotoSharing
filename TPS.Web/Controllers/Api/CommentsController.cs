using Microsoft.AspNet.Identity;
using System.Web.Http;
using TPS.Web.Core;
using TPS.Web.Core.Dtos;

namespace TPS.Web.Controllers.Api
{
    [Authorize]
    public class CommentsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Comments/5
        public IHttpActionResult GetTourComments(string id)
        {
            var comments = _unitOfWork.Comments.GetTourComments(id);

            return Ok(comments);
        }

        // POST: api/Comments
        public IHttpActionResult AddComment(CommentDto commentDto)
        {
            var userId = User.Identity.GetUserId();

            commentDto.CommenterId = userId;

            var id = _unitOfWork.Comments.AddComment(commentDto);
            _unitOfWork.Complete();

            return Ok(id);
        }

        // DLETE: api/Comments/id
        public IHttpActionResult DeleteComment(string id)
        {
            var userId = User.Identity.GetUserId();

            var comment = _unitOfWork.Comments.GetComment(id, userId);

            if (comment == null)
                return NotFound();

            _unitOfWork.Comments.Remove(comment);
            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}
