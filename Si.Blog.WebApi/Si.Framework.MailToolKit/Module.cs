using Microsoft.Extensions.DependencyInjection;
using Si.Framework.Base.Abstraction;
using Si.Framework.Base.Entity;
using Si.Framework.MailToolKit.MailService;

namespace Si.Framework.MailToolKit
{
    public class Module : ISiModule
    {
        public ModuleLevel Level { get; set; } = ModuleLevel.Application;

        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
