using Blog.Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Serilog;
using Si.Framework.Base;
using Si.Framework.Base.Extension;
using Si.Framework.Base.Utility;
using Si.Framework.Serilog;
namespace Api.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //添加Serilog日志
            LoggerLib.InitLogger();
            var builder = WebApplication.CreateBuilder(args);
            //添加服务配置文件
            builder.Configuration.AddSettings();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(Log.Logger);
            builder.WebHost.ConfigureKestrel(options =>
            {
                var section = builder.Configuration.GetSection("Kestrel");
                options.Configure(section);
            });
            // 注册 Swagger 生成器服务
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1",
                    Description = "BlogAPI"
                });
            });
            //添加hsts
            builder.Services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });
            builder.Services.AddDbContext<BlogDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
            //注册上下文访问器
            builder.Services.AddHttpContextAccessor();
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
            var staticFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "wwwroot");
            Directory.CreateDirectory(staticFilePath);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "wwwroot")),
                RequestPath = "/resources"
            });
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    // 配置 Swagger UI 的路径（可选）
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API v1");
                    options.RoutePrefix = string.Empty; // 使 Swagger UI 在根路径显示
                });
            }
            app.MapControllers();
            app.Run();
        }

       
    }
}
