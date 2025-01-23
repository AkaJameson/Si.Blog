using Blog.Infrastructure.Rbac.Entity;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Infrastructure.Rbac.Authorication
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
