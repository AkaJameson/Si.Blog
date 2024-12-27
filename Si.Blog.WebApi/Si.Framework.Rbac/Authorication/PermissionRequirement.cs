using Microsoft.AspNetCore.Authorization;
using Si.Framework.Rbac.Entity;

namespace Si.Framework.Rbac.Authorication
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public Permission RequiredPermission { get; }
        public int RequiredPermissionId { get; }

        public PermissionRequirement(Permission requiredPermission)
        {
            RequiredPermission = requiredPermission;
            RequiredPermissionId = requiredPermission.Id;
        }

    }
}
