using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Si.Framework.Base.Abstraction;
using Si.Framework.Base.Utility;
using Si.Framework.ToolKit.Extension;
using System.Net;

namespace Si.Framework.Base.Extension
{
    public static class WebPipeExtension
    {
        public static MvcOptions AddFilter(MvcOptions options)
        {
            var filters = AssemblyManager.FindTypesByBase<ISiFilter>()
                            .Where(t => t.IsClass && !t.IsAbstract && t.IsBaseOn(typeof(IFilterMetadata)))
                            .Select(t => (ISiFilter)ActivatorUtilities.CreateInstance(ServiceLocator.GetServiceProvider(),t)!)?
                            .OrderBy(t => t.Order);
            foreach (var filter in filters)
            {
                options.Filters.Add((IFilterMetadata)filter);
            }
            return options;
        }
        /// <summary>
        /// Kestrel配置
        /// </summary>
        /// <param name="builder"></param>
        public static void UseKestrel(this WebApplicationBuilder builder)
        {
            builder.WebHost.ConfigureKestrel(options =>
            {
                var kestrelConfig = builder.Configuration.GetSection("Kestrel");
                var ipAddr = IPAddress.Parse(kestrelConfig.GetValue<string>("Url") ?? "0.0.0.0");
                //https配置
                var httpPort = kestrelConfig.GetValue<int>("Configuration:Http:Port");
                options.Listen(ipAddr, httpPort);
                var httpsPort = kestrelConfig.GetValue<int>("Configuration:Https:Port");
                var certificatePath = kestrelConfig.GetValue<string>("Configuration:Https:Certificate:Path");
                var certificatePassword = kestrelConfig.GetValue<string>("Configuration:Https:Certificate:Password");
                if (!string.IsNullOrEmpty(certificatePath) && File.Exists(certificatePath))
                {
                    try
                    {
                        options.Listen(ipAddr, httpsPort, listenOptions =>
                        {
                            listenOptions.UseHttps(certificatePath, certificatePassword, httpsOptions =>
                            {
                                httpsOptions.AllowAnyClientCertificate();
                            });
                        });
                        LogProvider.Info($"HTTPS enabled with certificate: {certificatePath}");
                    }
                    catch (Exception ex)
                    {
                        LogProvider.Error($"HTTPS certificate error:{ex.ToString()}");
                    }
                }
            });
        }
    }
}
