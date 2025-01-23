using AutoMapper;
using Blog.Application.Database.Entity;

namespace Blog.Application.Database.EntityDto
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Views { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public string CreateTime { get; set; }
        public string UpdateTime { get; set; }
        public string PublishTime { get; set; }
        public int UserId { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
    }
    public class BlogMap : Profile
    {
        public BlogMap()
        {
            CreateMap<Entity.Blog,BlogDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString())) 
                .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")))  // 转换为自定义格式的字符串
                .ForMember(dest => dest.UpdateTime, opt => opt.MapFrom(src => src.UpdateTime.HasValue ? src.UpdateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty))  
                .ForMember(dest => dest.PublishTime, opt => opt.MapFrom(src => src.UpdateTime.HasValue ? src.UpdateTime.Value.ToString("yyyy-MM-dd HH:mm:ss"):string.Empty));
        }
    }
   
    
}
