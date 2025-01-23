using Blog.Infrastructure.Rbac.Entity;

namespace Blog.Infrastructure.Rbac.Authorication
{
    public class RolePermissionMapper
    {
        private static Dictionary<string, List<Permission>> _rolePermissionMap = new Dictionary<string, List<Permission>>();
        public static List<Permission> GetPermissionForRole(string roleId)
        {
            return _rolePermissionMap.ContainsKey(roleId) ? _rolePermissionMap[roleId] : new List<Permission>();
        }
        public static void AddPermissionForRole(string roleId, List<Permission> permissions)
        {
            _rolePermissionMap[roleId] = permissions;
        }

        public static void LoadRbacRelationship(Func<Dictionary<string, List<Permission>>> rolePermissionMap)
        {
            _rolePermissionMap.Clear();
            _rolePermissionMap = rolePermissionMap?.Invoke();
        }
    }
}
