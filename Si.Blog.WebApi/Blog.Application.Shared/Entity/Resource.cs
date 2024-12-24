using Si.Framework.EntityFramework.Abstraction;

namespace Blog.Application.Shared.Entity
{
    public class Resource: BaseEntity
    {
        public int Id { get; set; }
        public string filePath { get; set; }
        public string Description { get; set; }
    }
}
