using System.Web.Mvc;
using TPS.Web.Core.Models;

namespace TPS.Web.Controllers
{
    [RequireHttps]
    [Authorize]
    public class HomeController : Controller
    {
        [Audit]
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Tour Photo Sharing";

            return View();
        }

        [Audit]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Audit]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}