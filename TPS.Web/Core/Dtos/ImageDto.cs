using System;

namespace TPS.Web.Core.Dtos
{
    public class ImageDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string AltText { get; set; }
        public string Caption { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedDate { get; set; }
        public string Thumbnail { get; set; }
        public string ImageMimeType { get; set; }
        public bool Confirmed { get; set; }
        public string OwnerId { get; set; }
        public string Query { get; set; }
    }
}