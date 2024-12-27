using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Si.Framework.Base.Abstraction;
using Si.Framework.Base.Utility;
using Si.Framework.ToolKit.Extension;

namespace Si.Framework.Base.Extension
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
    }
}
