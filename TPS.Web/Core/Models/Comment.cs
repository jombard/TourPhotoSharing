using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPS.Web.Core.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public ApplicationUser Commenter { get; set; }

        [ForeignKey("Commenter")]
        public string CommenterId { get; set; }

        public string CommentValue { get; set; }

        public DateTime CommentDate { get; set; }

        public Tour Tour { get; set; }

        [ForeignKey("Tour")]
        public Guid TourId { get; set; }

        public string ParentCommentId { get; set; }
    }
}