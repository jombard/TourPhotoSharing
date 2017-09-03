using System.ComponentModel.DataAnnotations;

namespace TPS.Web.Core.Models
{
    public class StarredImages
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public Image Image { get; set; }
    }
}