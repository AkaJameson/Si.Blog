using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Si.Framework.Base.Abstraction;
using Si.Framework.Base.Entity;
using Si.Framework.Rbac.Authorication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
