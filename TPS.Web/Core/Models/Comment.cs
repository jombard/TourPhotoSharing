using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TPS.Web.Core.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public ApplicationUser Commenter { get; set; }

        [ForeignKey("Commenter")]
        public string CommenterId { get; set; }

        public string CommentValue { get; set; }

        public DateTime CommentDate { get; set; }
    }
}