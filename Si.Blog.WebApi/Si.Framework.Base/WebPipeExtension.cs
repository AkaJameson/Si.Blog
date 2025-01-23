using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Si.Framework.Base.Abstraction;
using Si.Framework.ToolKit;
using Si.Framework.ToolKit.Extension;

namespace Si.Framework.Base
{
    public static class WebPipeExtension
    {
        public static MvcOptions AddFilter(MvcOptions options)
        {
            var filters = AssemblyManager.FindTypesByBase<ISiFilter>()
                            .Where(t => t.IsClass && !t.IsAbstract && t.IsBaseOn(typeof(IFilterMetadata)))
                            .Select(t => (ISiFilter)Activator.CreateInstance(t)!)?
                            .OrderBy(t => t.Order);
            foreach (var filter in filters)
            {
                options.Filters.Add((IFilterMetadata)filter);
            }
            return options;
        }

        public static void AddSettings(this ConfigurationManager configuration)
        {
            configuration.AddJsonFile($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json")}"
                , optional: true, reloadOnChange: true);
        }
    }
}
