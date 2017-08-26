using System.ComponentModel.DataAnnotations;
using TPS.Web.Core.Models;

namespace TPS.Web.Core.ViewModels
{
    public class TourFormViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string OwnerId { get; set; }

        public TourFormViewModel(Tour tour)
        {
            Id = tour.Id.ToString();
            Name = tour.Name;
            Description = tour.Description;
            OwnerId = tour.OwnerId;
        }

        public TourFormViewModel()
        {
        }
    }
}