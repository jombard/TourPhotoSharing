using System;
using System.ComponentModel.DataAnnotations;

namespace TPS.Web.Core.Dtos
{
    public class CommentDto
    {
        public Guid Id { get; set; }

        public string CommenterId { get; set; }

        [Required]
        [StringLength(2048)]
        public string CommentValue { get; set; }

        public string CommentDate { get; set; }

        public Guid TourId { get; set; }

        public string ParentCommentId { get; set; }

        public string UserFullName { get; set; }

        public UserDto Commenter { get; set; }
    }
}