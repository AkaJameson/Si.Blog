using Microsoft.Extensions.DependencyInjection;
using Si.Framework.Base.Abstraction;
using Si.Framework.Base.Entity;
using Si.Framework.Base.Utility;
using Si.Framework.ToolKit.Extension;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
namespace Si.Framework.Base
{
    public static class ModuleLoader
    {
        private static Type[] Module { get; set; }
        private static List<ISiModule> SortModules { get; set; } = new List<ISiModule>();

        static ModuleLoader()
        {
            Module = AssemblyManager.FindTypes(p => p.IsBaseOn(typeof(ISiModule)) && p.IsClass && !p.IsAbstract).ToArray();
            var moduleList = Module?.Select(x => Activator.CreateInstance(x) as ISiModule)?.Distinct()?
               .OrderBy(p => p!.Level)?.ToList();
            SortModules = Sort(moduleList);
        }
        public static void LoadModules(this IServiceCollection services)
        {
            LogProvider.Info($"加载模块...\n{string.Join("\n", SortModules)}");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (var module in SortModules)
            {
                module.RegisterServices(services);
            }
            stopwatch.Stop();
            LogProvider.Info($"加载模块完成，耗时：{stopwatch.ElapsedMilliseconds} 毫秒。");
        }
        private static List<ISiModule> Sort(List<ISiModule> modules)
        {
            if (IsCircularDependency(modules))
            {
                throw new Exception("存在循环依赖！");
            }
            var dependencyDict = new Dictionary<ISiModule, List<ISiModule>>();
            var reverseDependenciesDict = new Dictionary<ISiModule, List<ISiModule>>();

            foreach (var module in modules)
            {
                var dependencies = module.GetType().GetCustomAttribute<DependencyAttribute>()?.DependencyModule ?? new Type[0];
                var dependentModules = dependencies.Select(dep => modules.FirstOrDefault(m => m.GetType() == dep)).ToList();

                dependencyDict[module] = dependentModules;

                foreach (var depModule in dependentModules)
                {
                    if (!reverseDependenciesDict.ContainsKey(depModule))
                    {
                        reverseDependenciesDict[depModule] = new List<ISiModule>();
                    }
                    reverseDependenciesDict[depModule].Add(module);
                }
            }
            var independentModules = modules.Where(m => !dependencyDict[m].Any()).ToList();
            var sortedModules = new List<ISiModule>();
            var queue = new Queue<ISiModule>(independentModules);
            while (queue.Any())
            {
                var module = queue.Dequeue();
                sortedModules.Add(module);
                if (reverseDependenciesDict.ContainsKey(module))
                {
                    foreach (var dependentModule in reverseDependenciesDict[module])
                    {
                        dependencyDict[dependentModule].Remove(module);
                        if (!dependencyDict[dependentModule].Any())
                        {
                            queue.Enqueue(dependentModule);
                        }
                    }
                }
            }
            if (sortedModules.Count != modules.Count)
            {
                throw new Exception("无法完成排序，可能存在循环依赖或缺少依赖模块！");
            }
            return sortedModules;
        }
        private static bool IsCircularDependency(List<ISiModule> modules)
        {
            var dependenciesDict = new Dictionary<Type, Type[]>();
            foreach (var module in modules)
            {
                var dependencies = module.GetType().GetCustomAttribute<DependencyAttribute>();
                dependenciesDict.TryAdd(module.GetType(), dependencies?.DependencyModule?? new Type[0]);
            }
            foreach (var module in modules)
            {
                //如果 module 依赖的模块中包含 module 自身，则存在循环依赖
                if (dependenciesDict[module.GetType()]
                    .Any(p => dependenciesDict[p].Any(q => q == module.GetType())))
                {
                    return true;
                }
            }
            return false;

        }

        


    }
}
