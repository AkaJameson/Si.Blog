using Microsoft.AspNetCore.Authorization;
using Si.Application.Rbac.enums;

namespace Blog.Application.Rbac.Authorication
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
