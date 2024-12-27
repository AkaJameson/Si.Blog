using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Si.Framework.AutoMapper.MapServices;
using Si.Framework.Base.Abstraction;
using Si.Framework.Base.Entity;
using Si.Framework.Base.Utility;

namespace Si.Framework.AutoMapper
{
    public class Module : ISiModule
    {
        public ModuleLevel Level { get; set; } = ModuleLevel.Core;
        public void RegisterServices(IServiceCollection services)
        {
            var profiles = AssemblyManager.FindTypesByBase<Profile>();
            services.AddAutoMapper(profiles);
            services.AddScoped<IMapperService, MapperServices>();
        }
    }
}
