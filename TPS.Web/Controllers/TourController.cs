using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using TPS.Web.Core;
using TPS.Web.Core.Models;
using TPS.Web.Core.ViewModels;

namespace TPS.Web.Controllers
{
    [Authorize]
    public class TourController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TourController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Tour
        [Audit]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var tours = _unitOfWork.Tours.GetTours(userId);
            return View("Index", tours);
        }

        public ActionResult New()
        {
            return View("TourForm", new TourFormViewModel());
        }

        [Audit]
        public ActionResult Edit(string id)
        {
            var tour = _unitOfWork.Tours.GetTour(id);

            if (tour == null)
                return HttpNotFound();

            var viewModel = new TourFormViewModel(tour);

            return View("TourForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult Save(TourFormViewModel tour)
        {
            if (!ModelState.IsValid)
                return View("TourForm", tour);

            if (string.IsNullOrEmpty(tour.Id))
            {
                var newTour = new Tour
                {
                    OwnerId = User.Identity.GetUserId(),
                    Name = tour.Name,
                    Description = tour.Description,
                    Thumbnail = @"~\Uploads\default.jpg"
                };
                _unitOfWork.Tours.AddTour(newTour);
            }
            else
            {
                var tourInDb = _unitOfWork.Tours.GetTour(tour.Id);
                tourInDb.Name = tour.Name;
                tourInDb.Description = tour.Description;
            }

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        [Audit]
        public ActionResult ViewTour(string id)
        {
            var tour = _unitOfWork.Tours.GetTour(id);

            if (tour == null)
                return HttpNotFound();

            var tourImages = _unitOfWork.Tours.GetImages(id);

            var uploaders = tourImages?
                .Select(u => u.Image.Owner.FullName)
                .Distinct()
                .OrderBy(o => o.Substring(0))
                .ToList();

            var viewModel = new TourImagesViewModel()
            {
                Tour = tour,
                TourImages = tourImages,
                UploaderNames = uploaders
            };
            return View(viewModel);
        }
    }
}