using Blog.Infrastructure.Rbac.Entity;
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

            var rolesId = user.Claims.Where(C => C.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            var userPermissions = new List<Permission>();
            foreach (var role in rolesId)
            {
               
                var rolePermissions = RolePermissionMapper.GetPermissionForRole(role);
            }
            if (userPermissions.Any(p=>p.Id == requirement.RequiredPermissionId))
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
