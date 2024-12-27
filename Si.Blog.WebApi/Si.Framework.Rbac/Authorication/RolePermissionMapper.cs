using Microsoft.EntityFrameworkCore;
using Si.Framework.Rbac.Entity;

namespace Si.Framework.Rbac.Authorication
{
    public class RolePermissionMapper
    {
        private static Dictionary<string, List<Permission>> _rolePermissionMap = new Dictionary<string, List<Permission>>();
        public static List<Permission> GetPermissionForRole(string roleId)
        {
            return _rolePermissionMap.ContainsKey(roleId) ? _rolePermissionMap[roleId] : new List<Permission>();
        }
        private static void AddPermissionForRole(string roleId, List<Permission> permissions)
        {
            _rolePermissionMap[roleId] = permissions;
        }
         public static void RegisterRolePermission<TDbContext>(TDbContext dbContext) where TDbContext : DbContext
        {

            var roleWithPermissions = dbContext.Set<RolePermission>().ToList().GroupBy(rp => rp.RoleId).ToDictionary(g => g.Key, g => g.Select(rp => rp.PermissionId).ToList());
            var allPermissions = dbContext.Set<Permission>().ToList();
            foreach (var rolePermission in roleWithPermissions)
            {
                ///获取角色对应的权限
                var rolesPermissions = allPermissions.Where(p => rolePermission.Value.Contains(p.Id)).ToList();
                AddPermissionForRole(rolePermission.Key.ToString(), rolesPermissions);
            }
            return;

        }
    }
}
