using Microsoft.Extensions.DependencyInjection;
using Si.Framework.Base.Abstraction;
using Si.Framework.Base.Entity;
using Si.Framework.ToolKit;

namespace Si.Framework.DDDComm
{
    public class Module : IModule
    {
        public ModuleLevel Level { get; set; } = ModuleLevel.Core;
        public void RegisterServices(IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AssemblyManager.AllAssemblies));
        }
    }
}
