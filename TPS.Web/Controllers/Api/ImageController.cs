using AutoMapper;
using ImageResizer;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web;
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

        // POST: api/Image
        public IHttpActionResult PostImage()
        {
            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count < 1)
                return BadRequest("No images provided");

            try
            {
                foreach (string fileName in httpRequest.Files)
                {
                    var file = httpRequest.Files[fileName];
                    //Save file content goes here
                    if (file != null && file.ContentLength > 0)
                    {
                        var filepath = "~\\uploads\\images\\";
                        var guid = "<guid>";

                        var resizeSettings = new ResizeSettings
                        {
                            MaxWidth = 1900,
                            MaxHeight = 1900,
                            Format = "jpg"
                        };

                        var imageJob = new ImageJob(file, filepath + guid, new Instructions(resizeSettings), true, true);
                        var compressedImg = ImageBuilder.Current.Build(imageJob);
                        compressedImg.Build();

                        var url = filepath + compressedImg.FinalPath.Split('\\').Last();

                        var imageDto = new ImageDto
                        {
                            ImageUrl = url,
                            Title = file.FileName,
                            ImageMimeType = file.ContentType,
                            OwnerId = User.Identity.GetUserId()
                        };

                        var id = _unitOfWork.Images.AddImage(imageDto);

                        // TODO attach to tour
                        //var lejogTour = _context.Tours.SingleOrDefault(t => t.Name == "LEJOG");

                        //var tourImage = new TourImages
                        //{
                        //    Image = image,
                        //    Tour = lejogTour
                        //};

                        //_unitOfWork.TourImages.Add(tourImage);
                    }
                }

                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok();
        }

        // PUT: api/Image/5
        [HttpPut]
        public IHttpActionResult UpdateImage(string id, ImageDto imageDto)
        {
            var userId = User.Identity.GetUserId();

            var imageInDb = _unitOfWork.Images.GetImage(id, userId);

            if (imageInDb == null)
                return NotFound();

            imageInDb.Caption = imageDto.Caption;
            imageInDb.Confirmed = true;

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
