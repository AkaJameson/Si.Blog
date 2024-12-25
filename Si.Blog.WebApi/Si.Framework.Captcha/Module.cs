using Microsoft.Extensions.DependencyInjection;
using Si.Framework.Base.Abstraction;
using Si.Framework.Base.Entity;

namespace Si.Framework.Captcha
{
    public class Module : ISiModule
    {
        public ModuleLevel Level { get; set; } = ModuleLevel.Core;

        public void RegisterServices(IServiceCollection services)
        {

        }
    }
}
