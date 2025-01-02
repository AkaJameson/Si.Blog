using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Si.Framework.Base.Abstraction;
using Si.Framework.Base.Entity;
using Si.Framework.Rbac.Authorication;

namespace Si.Framework.Rbac
{
    public class Module : ISiModule
    {
        public ModuleLevel Level { get; set; } = ModuleLevel.Core;

        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
        }
    }
}
