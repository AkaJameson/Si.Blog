
using Blog.Application.AppServices;
using Blog.Application.IAppServices;
using Blog.Application.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Si.Framework.Base.Abstraction;
using Si.Framework.Base.Entity;
using Si.Framework.Base.Utility;
using Si.Framework.Rbac.Authorication;
using Si.Framework.Rbac.JWT;

namespace Blog.Application
{
    //运行依赖于Domain层的Module
    [Dependency(typeof(Domain.Module))]
    public class Module : ISiModule
    {
        public ModuleLevel Level => ModuleLevel.Application;

        public void RegisterServices(IServiceCollection services)
        {
            
            services.AddScoped<IAppUserService,AppUserServiceImpl>();
            services.AddScoped<IAppAdminService, AppAdminServiceImpl>();
        }
    }
}
