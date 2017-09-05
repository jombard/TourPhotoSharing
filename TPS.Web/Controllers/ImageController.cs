using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Mvc;
using TPS.Web.Core;
using TPS.Web.Core.Dtos;
using TPS.Web.Core.Models;
using TPS.Web.Core.ViewModels;
using Image = TPS.Web.Core.Models.Image;

namespace TPS.Web.Controllers
{
    [Authorize]
    public class ImageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Audit]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var images = _unitOfWork.Images.GetUserImages(userId);

            var viewModel = new ImageListViewModel(images);

            return View(viewModel);
        }

        [Audit]
        public ActionResult Upload()
        {
            return View();
        }

        [Audit]
        public ActionResult UploadImage()
        {
            var userId = User.Identity.GetUserId();

            var completed = new List<ImageDto>();
            foreach (string fileName in Request.Files)
            {
                var file = Request.Files[fileName];

                if (file == null || file.ContentLength < 1)
                    continue;

                var image = _unitOfWork.Images.UploadUserImage(file, userId);
                _unitOfWork.Images.AddImage(image);

                var imageDto = Mapper.Map<Image, ImageDto>(image);
                completed.Add(imageDto);
            }

            _unitOfWork.Complete();

            return Json(completed);
        }

        [Audit]
        public ActionResult Confirm()
        {
            var images = _unitOfWork.Images.GetPending();

            var viewModel = new ImageListViewModel(images);

            return View(viewModel);
        }

        [Audit]
        public ActionResult ConfirmUpload()
        {
            return View();
        }
    }
}