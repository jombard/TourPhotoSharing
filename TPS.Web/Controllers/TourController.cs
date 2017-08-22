using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TPS.Web.Core.Models;
using TPS.Web.Core.ViewModels;
using TPS.Web.Persistence;

namespace TPS.Web.Controllers
{
    [Authorize]
    public class TourController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TourController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Tour
        public ActionResult Index()
        {
            var tours = _context.Tours.ToList();
            return View(tours);
        }

        public ActionResult New()
        {
            return View("TourForm", new TourFormViewModel());
        }

        public ActionResult Edit(string id)
        {
            var tour = _context.Tours.SingleOrDefault(t => t.Id.ToString() == id);

            if(tour == null)
                return HttpNotFound();

            var viewModel = new TourFormViewModel(tour);

            return View("TourForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                _context.Tours.Add(newTour);
            }
            else
            {
                var tourInDb = _context.Tours.Single(t => t.Id.ToString() == tour.Id);
                tourInDb.Name = tour.Name;
                tourInDb.Description = tour.Description;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult ViewTour(string id)
        {
            var tour = _context.Tours.SingleOrDefault(t => t.Id.ToString() == id);

            if(tour == null)
                return HttpNotFound();

            var tourImages = _context.TourImages
                .Where(t => t.Tour.Id.ToString() == id)
                .Include(i => i.Image)
                .Include(u => u.Image.Owner)
                .OrderByDescending(t => t.Image.CreatedDate)
                .ToList();

            var uploaders = tourImages
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