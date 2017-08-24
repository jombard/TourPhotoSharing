﻿using System.ComponentModel.DataAnnotations;
using System.Web;
using TPS.Web.Core.Models;

namespace TPS.Web.Core.ViewModels
{
    public class ImageViewModel
    {
        public ImageViewModel()
        {
        }

        public ImageViewModel(Image image)
        {
            Id = image.Id.ToString();
            Title = image.Title;
            AltText = image.AltText;
            Caption = image.Caption;
            ImagePath = image.ImageUrl;
            Thumbnail = image.Thumbnail;
            Query = image.Query;
        }

        public string Query { get; set; }

        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string AltText { get; set; }

        [DataType(DataType.Html)]
        public string Caption { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }

        public string ImagePath { get; set; }

        public byte[] Thumbnail { get; set; }
    }
}