using Si.Framework.EntityFramework.Abstraction;

namespace Si.Framework.Rbac.Entity
{
    public class UserRole: BaseEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
