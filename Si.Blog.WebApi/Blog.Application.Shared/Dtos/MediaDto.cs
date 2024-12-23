using AutoMapper;
using Blog.Application.Shared.Entity;

namespace Blog.Application.Shared.Dtos
{
    public class MediaDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string CreateTime { get; set; }
    }
    public class MediaMap : Profile
    {
        public MediaMap()
        {
            CreateMap<Media, MediaDto>()
                .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")));
        }

    }
}
