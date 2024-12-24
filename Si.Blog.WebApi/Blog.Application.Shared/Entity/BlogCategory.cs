using Si.Framework.EntityFramework.Abstraction;

namespace Blog.Application.Shared.Entity
{
    public class BlogCategory: BaseEntity
    {
        public int CategoryId { get; set; }
        public int BlogId { get; set; }
    }
}
