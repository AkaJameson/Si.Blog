using Blog.Infrastructure.Rbac.enums;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Infrastructure.Rbac.Authorication
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public Permission RequiredPermission { get; }

        public PermissionRequirement(Permission requiredPermission)
        {
            RequiredPermission = requiredPermission;
        }

    }
}
