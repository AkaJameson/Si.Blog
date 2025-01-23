using Blog.Application.Database;
using Blog.Infrastructure.Rbac.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
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
            //���Serilog��־
            LoggerLib.InitLogger();
            var builder = WebApplication.CreateBuilder(args);
            //��ӷ��������ļ�
            builder.Configuration.AddSettings();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(Log.Logger);
            builder.WebHost.ConfigureKestrel(options =>
            {
                var section = builder.Configuration.GetSection("Kestrel");
                options.Configure(section);
            });
            //���hsts
            builder.Services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });
            var connectionStr = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<BlogDbContext>(options=>options.UseSqlite(connectionStr));
            //ע�������ķ�����
            builder.Services.AddHttpContextAccessor();
            //ע��JWT��֤
            builder.Services.AddJWTBearer(builder.Configuration);
            //���ÿ���
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowCredentials();
                });
            });
            //ע��ģ��
            ModuleLoader.LoadModules(builder.Services);
            builder.Services.AddControllers(options =>
            {
                WebPipeExtension.AddFilter(options);
            });
            var app = builder.Build();
            ServiceLocator.SetServiceProvider(app.Services);
            app.UseRouting();
            app.UseCors("CorPolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<AuthorizationMiddleware>();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "wwwroot")),
                RequestPath = "/resources"
            });
            app.MapControllers();
            app.Run();
        }
    }
}
