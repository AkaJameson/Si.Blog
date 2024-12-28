using Microsoft.Extensions.DependencyInjection;
using Si.Framework.Base.Abstraction;
using Si.Framework.Base.Entity;

namespace Blog.Application.Contracts
{
    [Dependency(typeof(Blog.Application.Domain.Module))]
    public class Module : ISiModule
    {
        public ModuleLevel Level { get; set; } = ModuleLevel.Application;
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<BlogRegexHelper>();
        }
    }
}
