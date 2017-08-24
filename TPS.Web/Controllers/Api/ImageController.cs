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
            var userId = User.Identity.GetUserId();

            var imageInDb = _unitOfWork.Images.GetImage(id, userId);

            if (imageInDb == null)
                return NotFound();

            return Ok(Mapper.Map<Image, ImageDto>(imageInDb));
        }

        // PUT: api/Image/5
        [HttpPut]
        public IHttpActionResult UpdateImage(string id, ImageDto imageDto)
        {
            var userId = User.Identity.GetUserId();

            var imageInDb = _unitOfWork.Images.GetImage(id, userId);

            if (imageInDb == null)
                return NotFound();

            if (imageDto.Caption != null)
            {
                imageInDb.Caption = imageDto.Caption;
                imageInDb.Confirmed = true;
            }

            if (imageDto.Query != null)
            {
                imageInDb.Query = imageDto.Query;
            }

            _unitOfWork.Complete();

            return Ok();
        }

        // DELETE: api/Image/5
        [HttpDelete]
        public IHttpActionResult DeleteImage(string id)
        {
            var userId = User.Identity.GetUserId();

            var imageInDb = _unitOfWork.Images.GetImage(id, userId);

            if (imageInDb == null)
                return NotFound();

            _unitOfWork.Images.Remove(imageInDb);
            _unitOfWork.Complete();

            return Ok(id);
        }

        [HttpPost]
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
