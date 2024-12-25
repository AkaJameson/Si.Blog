using Microsoft.Extensions.DependencyInjection;
using Si.Framework.Base.Abstraction;
using Si.Framework.Base.Entity;
using Si.Framework.MemoryCache.Services;

namespace Si.Framework.MemoryCache
{
    public class Module : ISiModule
    {
        public ModuleLevel Level { get; set; } = ModuleLevel.Core;

        public void RegisterServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<ICacheService, CacheService>();
        }
    }
}
