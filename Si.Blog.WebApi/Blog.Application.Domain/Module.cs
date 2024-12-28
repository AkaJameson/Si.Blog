using Blog.Application.Domain.Aggregates;
using Blog.Application.Domain.DomainServices;
using Blog.Application.Domain.IDomainServices;
using Blog.Application.Shared;
using Microsoft.Extensions.DependencyInjection;
using Si.Framework.Base.Abstraction;
using Si.Framework.Base.Entity;
using Si.Framework.EntityFramework.UnitofWork;

namespace Blog.Application.Domain
{
    public class Module : ISiModule
    {
        
        public ModuleLevel Level { get; set; } = ModuleLevel.Application;

        public void RegisterServices(IServiceCollection services)
        {

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork<BlogDbContext>));
            services.AddScoped<IUserService, UserServiceImpl>();
            services.AddScoped<UserAggregate>();
            services.AddScoped<IAdminService, AdminServiceImpl>();

        }
    }
}
