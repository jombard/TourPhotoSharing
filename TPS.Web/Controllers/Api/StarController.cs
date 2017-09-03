using Microsoft.AspNet.Identity;
using System.Web.Http;
using TPS.Web.Core;
using TPS.Web.Core.Models;

namespace TPS.Web.Controllers.Api
{
    public class StarController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public StarController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Audit]
        public IHttpActionResult Star(string imageId)
        {
            var userId = User.Identity.GetUserId();

            var image = _unitOfWork.Images.GetImage(imageId);
            if (image == null)
                return NotFound();

            var result = _unitOfWork.StarredImages.AddStar(image, userId);

            return Ok(result);
        }

        [HttpDelete]
        [Audit]
        public IHttpActionResult UnStar(string imageId)
        {
            var userId = User.Identity.GetUserId();

            var image = _unitOfWork.Images.GetImage(imageId);
            if (image == null)
                return NotFound();

            var result = _unitOfWork.StarredImages.RemoveStar(image, userId);

            return Ok(result);
        }
    }
}
