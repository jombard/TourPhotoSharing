using AutoMapper;
using TPS.Web.Core.Dtos;
using TPS.Web.Core.Models;

namespace TPS.Web.App_Start
{
    public class AutoMapperConfiguration : Profile
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Image, ImageDto>().ReverseMap();
                cfg.CreateMap<Comment, CommentDto>().ReverseMap();
            });
        }
    }
}