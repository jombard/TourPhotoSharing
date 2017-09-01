using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Http;
using TPS.Web.Core;
using TPS.Web.Core.Dtos;
using TPS.Web.Core.Models;

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
        [Audit]
        public IHttpActionResult AddComment(CommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var userId = User.Identity.GetUserId();

            commentDto.CommenterId = userId;
            commentDto.CommentDate = DateTime.Now.ToString("s");

            var comment = Mapper.Map<CommentDto, Comment>(commentDto);

            _unitOfWork.Comments.AddComment(comment);
            _unitOfWork.Complete();

            commentDto.Id = comment.Id;
            commentDto.Commenter = new UserDto { FullName = User.GetFullName() };

            return Ok(commentDto);
        }

        // DELETE: api/Comments/id
        [Audit]
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
