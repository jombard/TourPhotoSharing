using System.Collections.Generic;
using TPS.Web.Core.Models;

namespace TPS.Web.Core.ViewModels
{
    public class TourImagesViewModel
    {
        public Tour Tour { get; set; }
        public List<TourImages> TourImages { get; set; }
    }
}