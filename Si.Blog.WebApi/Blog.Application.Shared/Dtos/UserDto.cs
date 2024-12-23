using AutoMapper;
using Blog.Application.Shared.Entity;

namespace Blog.Application.Shared.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string AvatarPath { get; set; }
        public string Account { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string CreateTime { get; set; }
    }

    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")));
        }
    }
}
