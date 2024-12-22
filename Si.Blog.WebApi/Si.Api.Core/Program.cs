using Blog.Infrastructure.Rbac.JWT;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using Si.Framework.Base;
using Si.Framework.Serilog;
using Si.Framework.ToolKit;
namespace Api.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //添加Serilog日志
            LoggerLib.InitLogger();
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(Log.Logger);
            builder.WebHost.ConfigureKestrel(options =>
            {
                var section = builder.Configuration.GetSection("Kestrel");
                options.Configure(section);
            });
            //添加hsts
            builder.Services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });
            //注册上下文访问器
            builder.Services.AddHttpContextAccessor();
            //注册JWT验证
            builder.Services.AddJWTBearer();
            //配置跨域
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowCredentials();
                });
            });
            //注册模块
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
