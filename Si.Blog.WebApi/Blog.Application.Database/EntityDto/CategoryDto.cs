using AutoMapper;
using Blog.Application.Database.Entity;
using Blog.Application.Database.EntityDto;

namespace Blog.Application.Database.EntityDto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreateTime { get; set; }
    }
    public class CategoryMap : Profile
    {
        public CategoryMap()
        {
            CreateMap<Category, CategoryDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")));

        }
    }
}
