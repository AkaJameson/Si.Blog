using Blog.Infrastructure.Rbac.enums;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Blog.Infrastructure.Rbac.Authorication
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var user = context.User;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var roles = user.Claims.Where(C => C.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            var userPermissions = new List<Permission>();
            foreach (var role in roles)
            {
                if (Enum.TryParse(role, out Role permission))
                {
                    if (Enum.TryParse(role, out Role parsedRole))
                    {
                        userPermissions.AddRange(RolePermissionMapper.GetPermissionForRole(parsedRole));
                    }
                }
            }
            if (userPermissions.Contains(requirement.RequiredPermission))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;

        }

    }
}
