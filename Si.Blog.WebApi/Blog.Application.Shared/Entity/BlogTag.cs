using Si.Framework.EntityFramework.Abstraction;

namespace Blog.Application.Shared.Entity
{
    public class BlogTag: BaseEntity
    {
        public int TagId { get; set; }
        public int BlogId { get; set; }
    }
}
