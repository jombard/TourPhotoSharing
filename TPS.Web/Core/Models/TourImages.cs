using System.ComponentModel.DataAnnotations;

namespace TPS.Web.Core.Models
{
    public class TourImages
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Tour Tour { get; set; }

        [Required]
        public Image Image { get; set; }
    }
}