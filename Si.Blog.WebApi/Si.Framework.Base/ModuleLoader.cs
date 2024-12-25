using Microsoft.Extensions.DependencyInjection;
using Si.Framework.Base.Abstraction;
using Si.Framework.Base.Entity;
using Si.Framework.ToolKit;
using Si.Framework.ToolKit.Extension;
using System.Diagnostics;
using System.Reflection;
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
            LogProvider.Info($"加载模块...\n ================{string.Join("\n", SortModules)}");
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
            if (modules == null || modules.Count == 0)
            {
                return new List<ISiModule>();
            }
            var coreModules = modules?.Where(p => p.Level == ModuleLevel.Core).ToList();
            var otherModules = modules?.Where(p => p.Level != ModuleLevel.Core).ToList();
            var sortedModules = new List<ISiModule>();
            // 1.优先加载无依赖的 Core 模块
            LoadModulesWithoutDependencies(coreModules, sortedModules);
            // 2. 检查 Core 模块的循环依赖
            if (CheckForCircularDependencies(coreModules))
            {
                LogProvider.Error("Core模块存在循环依赖！");
                throw new InvalidOperationException("Core模块存在循环依赖！");
            }
            // 3. 加载有依赖的 Core 模块
            LoadModulesWithDependencies(coreModules, sortedModules);
            // 4. 优先加载无依赖的其他级别模块
            LoadModulesWithoutDependencies(otherModules, sortedModules);
            //5. 检查其他级别模块的循环依赖,有则跳过该模块
            if (CheckForCircularDependencies(otherModules))
            {
                LogProvider.Error("其他级别模块存在循环依赖！");
            }
            // 5. 加载有依赖的其他级别模块
            LoadModulesWithDependencies(otherModules, sortedModules);
            return sortedModules;
        }
        /// <summary>
        /// 检查模块中是否存在循环依赖
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="loadingModules"></param>
        /// <returns></returns>
        private static bool CheckForCircularDependencies(List<ISiModule> modules)
        {
            var depandencies = new Dictionary<ISiModule, List<ISiModule>>();
            foreach (var module in modules)
            {
                var moduleDependencies = GetDependencies(module);
                depandencies[module] = moduleDependencies;
            }
            foreach (var module in modules)
            {
                if (HasCircularDependency(module, depandencies, new HashSet<ISiModule>()))
                {
                    LogProvider.Error($"模块 {module.GetType().Name} 存在循环依赖,以跳过加载。");
                    modules.Remove(module);
                    return true;
                }
            }
            return false;
        }
        private static bool HasCircularDependency(ISiModule module, Dictionary<ISiModule, List<ISiModule>> dependencies, HashSet<ISiModule> visitedModules)
        {
            if (visitedModules.Contains(module))
            {
                return true;
            }
            visitedModules.Add(module);
            foreach (var dep in dependencies[module])
            {
                if (HasCircularDependency(dep, dependencies, visitedModules))
                {
                    return true;
                }
            }
            visitedModules.Remove(module);
            return false;
        }
        // 根据依赖关系加载模块（无依赖的先加载）
        private static void LoadModulesWithoutDependencies(List<ISiModule> modules, List<ISiModule> sortedModules)
        {
            var modulesWithoutDependencies = modules.Where(m => !GetDependencies(m).Any()).ToList();
            foreach (var module in modulesWithoutDependencies)
            {
                sortedModules.Add(module);
            }
        }
        // 根据依赖关系加载模块（有依赖的模块，处理依赖关系）
        private static void LoadModulesWithDependencies(List<ISiModule> modules, List<ISiModule> sortedModules)
        {
            var moduleQueue = new Queue<ISiModule>(modules);
            while (moduleQueue.Count > 0)
            {
                var module = moduleQueue.Dequeue();

                if (!AreDependenciesLoaded(module, sortedModules))
                {
                    moduleQueue.Enqueue(module);
                    continue;
                }
                if (!sortedModules.Contains(module))
                {
                    sortedModules.Add(module);
                }
            }
        }

        // 检查模块的依赖是否已加载
        private static bool AreDependenciesLoaded(ISiModule module, List<ISiModule> sortedModules)
        {
            var dependencies = GetDependencies(module);
            return dependencies.All(dep => sortedModules.Contains(dep));
        }

        // 获取模块的所有依赖
        private static List<ISiModule> GetDependencies(ISiModule module)
        {
            var dependencies = new List<ISiModule>();
            var dependencyAttr = module.GetType().GetCustomAttribute<DependencyAttribute>();

            if (dependencyAttr != null)
            {
                foreach (var depType in dependencyAttr.DependencyModule)
                {
                    var dependency = (ISiModule)Activator.CreateInstance(depType);
                    dependencies.Add(dependency);
                }
            }

            return dependencies;
        }

    }
}
