using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TPS.Web.Core.Models;

namespace TPS.Web.Core.ViewModels
{
    public class TourImagesViewModel
    {
        public Tour Tour { get; set; }

        public List<TourImages> TourImages { get; set; }

        [Display(Name = "Filter by uploader")]
        public List<string> UploaderNames { get; set; }
    }
}