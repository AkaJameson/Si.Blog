using Blog.Application.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Serilog;
using Si.Framework.Base;
using Si.Framework.Base.Extension;
using Si.Framework.Base.Serilog;
using Si.Framework.Base.Utility;
using Si.Framework.Rbac.Authorication;
using Si.Framework.Rbac.JWT;
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
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Description = "请输入 Bearer 跟随你的 JWT token，例如: `Bearer your_token_here`"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
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
            var tokenValidationParameters = JWTHelper.GetTokenValidation();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });
            builder.Services.AddAuthorization(options =>
            {
                //读取数据库配置
                using var blogDbContext = new BlogDbContext(new DbContextOptionsBuilder<BlogDbContext>().Options, ServiceLocator.GetConfiguration()["ConnectionStrings:DefaultConnection"]);
                RolePermissionMapper.RegisterRolePermission<BlogDbContext>(blogDbContext);
                //管理员
                options.AddPolicy("Admin", policy => RolePermissionMapper.GetPermissionForRole("0").ForEach(p => policy.Requirements.Add(new PermissionRequirement(p))));
                //访客
                options.AddPolicy("Guest", policy => RolePermissionMapper.GetPermissionForRole("1").ForEach(p => policy.Requirements.Add(new PermissionRequirement(p))));
                //普通用户
                options.AddPolicy("User", policy => RolePermissionMapper.GetPermissionForRole("2").ForEach(p => policy.Requirements.Add(new PermissionRequirement(p))));
            });
            builder.Services.AddControllers(options =>
            {
                WebPipeExtension.AddFilter(options);
            }).AddJsonOptions(options =>
            {
                //忽略null值
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            var app = builder.Build();
            ServiceLocator.SetServiceProvider(app.Services);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CorPolicy");
            app.UseHttpsRedirection();
            app.UseMiddleware<AuthorizeMiddleware>();
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
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.Run();
        }


    }
}
