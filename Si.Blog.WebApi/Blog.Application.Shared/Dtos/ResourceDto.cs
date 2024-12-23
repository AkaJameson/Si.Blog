using AutoMapper;
using Blog.Application.Shared.Entity;

namespace Blog.Application.Shared.Dtos
{
    public class ResourceDto
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
    }
    public class ResouceMap : Profile
    {
        public ResouceMap()
        {
            CreateMap<Resource, ResourceDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.filePath))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
