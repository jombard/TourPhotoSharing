using System.ComponentModel.DataAnnotations;
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
            Caption = image.Caption;
            ImagePath = image.ImageUrl;
            Query = image.Query;
            Confirmed = image.Confirmed;
            Owner = image.Owner.FullName;
        }

        public string Owner { get; set; }

        public string Query { get; set; }

        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        [DataType(DataType.Html)]
        public string Caption { get; set; }

        public string ImagePath { get; set; }

        public byte[] Thumbnail { get; set; }

        public bool Confirmed { get; set; }
    }
}