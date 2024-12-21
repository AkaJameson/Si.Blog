using Blog.Application.Rbac.JWT;
using Microsoft.AspNetCore.Authorization;
using Si.Framework.Base;
using Si.Framework.ToolKit;

namespace Api.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.ConfigureKestrel(options =>
            {
                var section = builder.Configuration.GetSection("Kestrel");
                options.Configure(section);
            });
            //Ìí¼Óhsts
            builder.Services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });
            //×¢²áÉÏÏÂÎÄ·ÃÎÊÆ÷
            builder.Services.AddHttpContextAccessor();
            //×¢²áJWTÑéÖ¤
            builder.Services.AddJWTBearer();
            //ÅäÖÃ¿çÓò
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowCredentials();
                });
            });
            //×¢²áÄ£¿é
            ModuleLoader.LoadModules(builder.Services);
            builder.Services.AddControllers(options =>
            {
                ComponentManager.AddFilter(options);
            });
            var app = builder.Build();
            ServiceLocator.SetServiceProvider(app.Services);
            app.UseRouting();
            app.UseCors("CorPolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<AuthorizationMiddleware>();
            app.MapControllers();
            app.Run();
        }
    }
}
