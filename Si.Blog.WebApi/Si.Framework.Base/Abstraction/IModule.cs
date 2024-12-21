using Microsoft.Extensions.DependencyInjection;
using Si.Framework.Base.Entity;

namespace Si.Framework.Base.Abstraction
{
    public interface IModule
    {
        public ModuleLevel Level { get; }
        /// <summary>
        /// 提供管道注册服务功能
        /// </summary>
        /// <param name="services"></param>
        public void RegisterServices(IServiceCollection services);

    }
}
