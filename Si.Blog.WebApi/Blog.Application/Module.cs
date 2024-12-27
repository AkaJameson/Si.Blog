
using Blog.Application.AppServices;
using Blog.Application.IAppServices;
using Blog.Application.Shared;
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
            //注册JWT验证
            services.AddJWTBearer(ServiceLocator.GetConfiguration());
            services.AddAuthorization(options =>
            {
                //读取数据库配置
                using var blogDbContext = new BlogDbContext(new DbContextOptionsBuilder<BlogDbContext>().Options, ServiceLocator.GetConfiguration()["ConnectionStrings:DefaultConnection"]);
                RolePermissionMapper.RegisterRolePermission<BlogDbContext>(blogDbContext);
                //管理员
                options.AddPolicy("Admin", policy => RolePermissionMapper.GetPermissionForRole("0").ForEach(p => policy.Requirements.Add(new PermissionRequirement(p))));
                //访客
                options.AddPolicy("Guest", policy => RolePermissionMapper.GetPermissionForRole("1").ForEach(p => policy.Requirements.Add(new PermissionRequirement(p))));
                //普通用户
                options.AddPolicy("User", policy => RolePermissionMapper.GetPermissionForRole("2").ForEach(p => policy.Requirements.Add(new PermissionRequirement(p))));
            });

            services.AddScoped<IAppUserService,AppUserServiceImpl>();
        }
    }
}
