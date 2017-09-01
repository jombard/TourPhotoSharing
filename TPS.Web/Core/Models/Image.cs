using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TPS.Web.Core.Dtos;

namespace TPS.Web.Core.Models
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [DataType(DataType.Html)]
        public string Caption { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        private DateTime? _createdDate;

        [DataType(DataType.DateTime)]
        public DateTime? CreatedDate
        {
            get => _createdDate ?? DateTime.UtcNow;
            set => _createdDate = value;
        }

        public byte[] Thumbnail { get; set; }

        public string ImageMimeType { get; set; }

        public bool Confirmed { get; set; }

        public ApplicationUser Owner { get; set; }

        [ForeignKey("Owner")]
        public string OwnerId { get; set; }

        public float? Latitude { get; set; }

        public float? Longitude { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime UploadDate { get; set; }

        public string Query { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }

        public static readonly string[] ValidImageTypes = {
            "image/gif",
            "image/jpeg",
            "image/pjpeg",
            "image/png"
        };

        public Image()
        {
        }

        public Image(ImageDto imageDto)
        {
            Id = imageDto.Id;
            Title = imageDto.Title;
            Caption = imageDto.Caption;
            ImageUrl = imageDto.ImageUrl;
            CreatedDate = DateTime.Now;
            Confirmed = false;
            OwnerId = imageDto.OwnerId;
        }
    }
}