
using Blog.Application.AppServices;
using Microsoft.Extensions.DependencyInjection;
using Si.Framework.Base.Abstraction;
using Si.Framework.Base.Entity;

namespace Blog.Application
{
    public class Module : ISiModule
    {
        public ModuleLevel Level => ModuleLevel.Application;

        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<AppUserServices>();
        }
    }
}
