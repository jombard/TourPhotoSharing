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

        // POST: api/image
        [HttpPost]
        [Audit]
        public IHttpActionResult AddStar(string id)
        {
            var userId = User.Identity.GetUserId();

            var image = _unitOfWork.Images.GetImage(id);

            if (image == null)
                return NotFound();

            var result = _unitOfWork.Images.AddStar(image, userId);

            _unitOfWork.Complete();

            return Ok(result);
        }

        // DELETE: api/Image/5
        [HttpDelete]
        [Audit]
        public IHttpActionResult DeleteStar(string id)
        {
            var userId = User.Identity.GetUserId();

            var imageInDb = _unitOfWork.Images.GetImage(id);

            if (imageInDb == null)
                return NotFound();

            var result = _unitOfWork.Images.RemoveStar(imageInDb, userId);

            _unitOfWork.Complete();

            return Ok(result);
        }
    }
}
