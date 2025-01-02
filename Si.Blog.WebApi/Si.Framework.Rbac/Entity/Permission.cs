using Si.Framework.EntityFramework.Abstraction;

namespace Si.Framework.Rbac.Entity
{
    public class Permission:BaseEntity
    {
        public int Id { get; set; }
        public string PermissionName { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }

    }
}
