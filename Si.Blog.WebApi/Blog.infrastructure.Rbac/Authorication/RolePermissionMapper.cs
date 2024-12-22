using Blog.Infrastructure.Rbac.enums;

namespace Blog.Infrastructure.Rbac.Authorication
{
    public class RolePermissionMapper
    {
        private static readonly Dictionary<Role, List<Permission>> _rolePermissionMap = new Dictionary<Role, List<Permission>>();
        public static List<Permission> GetPermissionForRole(Role role)
        {
            return _rolePermissionMap.ContainsKey(role) ? _rolePermissionMap[role] : new List<Permission>();
        }
        public static void AddPermissionForRole(Role role, List<Permission> permissions)
        {
            _rolePermissionMap[role] = permissions;
        }
    }
}
