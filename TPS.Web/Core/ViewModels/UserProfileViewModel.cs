using System.ComponentModel.DataAnnotations;

namespace TPS.Web.Core.ViewModels
{
    public class UserProfileViewModel
    {
        [Required]
        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name="Last Name")]
        public string LastName { get; set; }
    }
}