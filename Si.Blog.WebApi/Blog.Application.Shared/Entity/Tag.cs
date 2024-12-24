using Si.Framework.EntityFramework.Abstraction;

namespace Blog.Application.Shared.Entity
{
    public class Tag: BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
