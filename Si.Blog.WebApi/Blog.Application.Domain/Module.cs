using Microsoft.Extensions.DependencyInjection;
using Si.Framework.Base.Abstraction;
using Si.Framework.Base.Entity;
using Si.Framework.EntityFramework.UnitofWork;

namespace Blog.Application.Domain
{
    public class Module : IModule
    {
        public ModuleLevel Level { get; set; } = ModuleLevel.Application;

        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
