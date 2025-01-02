using Si.Framework.EntityFramework.Abstraction;

namespace Si.Framework.Rbac.Entity
{
    public class BaseUser : BaseEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime CreateTime { get; set; }
        public string Description { get; set; }
    }
}
