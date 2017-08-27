using System;

namespace TPS.Web.Core.Dtos
{
    public class CommentDto
    {
        public Guid Id { get; set; }

        public string CommenterId { get; set; }

        public string CommentValue { get; set; }

        public string CommentDate { get; set; }

        public Guid TourId { get; set; }

        public string ParentCommentId { get; set; }
    }
}