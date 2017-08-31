using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using TPS.Web.Core;
using TPS.Web.Core.Dtos;
using TPS.Web.Core.Models;

namespace TPS.Web.Controllers.Api
{
    [Authorize]
    public class ImageController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Image/5
        public IHttpActionResult GetImage(string id)
        {
            var imageInDb = _unitOfWork.Images.GetImage(id);

            if (imageInDb == null)
                return NotFound();

            return Ok(Mapper.Map<Image, ImageDto>(imageInDb));
        }

        // PUT: api/Image/5
        [HttpPut]
        [Audit]
        public IHttpActionResult UpdateImage(string id, ImageDto imageDto)
        {
            var imageInDb = _unitOfWork.Images.GetImage(id);

            if (imageInDb == null)
                return NotFound();

            if (imageDto.Caption != null)
            {
                imageInDb.Caption = imageDto.Caption;
            }

            if (imageDto.Query != null)
            {
                imageInDb.Query = imageDto.Query;
            }

            imageInDb.Confirmed = imageDto.Confirmed;

            _unitOfWork.Complete();

            return Ok();
        }

        // DELETE: api/Image/5
        [HttpDelete]
        [Audit]
        public IHttpActionResult DeleteImage(string id)
        {
            var imageInDb = _unitOfWork.Images.GetImage(id);

            if (imageInDb == null)
                return NotFound();

            _unitOfWork.Images.Remove(imageInDb);
            _unitOfWork.Complete();

            return Ok(id);
        }

        // POST: api/image
        [HttpPost]
        [Audit]
        public IHttpActionResult AddImage(ImageDto imageDto)
        {
            var userId = User.Identity.GetUserId();

            imageDto.OwnerId = userId;

            var id = _unitOfWork.Images.AddImage(imageDto);
            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}
