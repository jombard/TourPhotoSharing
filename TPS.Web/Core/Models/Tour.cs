using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPS.Web.Core.Models
{
    public class Tour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ApplicationUser Owner { get; set; }

        [ForeignKey("Owner")]
        public string OwnerId { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }
    }
}