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
            // ע�� Swagger ����������
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
                    Description = "������ Bearer ������� JWT token������: `Bearer your_token_here`"
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
            //���hsts
            builder.Services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });
            builder.Services.AddDbContext<BlogDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
            //ע�������ķ�����
            builder.Services.AddHttpContextAccessor();
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
            var tokenValidationParameters = JWTHelper.GetTokenValidation();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });
            builder.Services.AddAuthorization(options =>
            {
                //��ȡ���ݿ�����
                using var blogDbContext = new BlogDbContext(new DbContextOptionsBuilder<BlogDbContext>().Options, ServiceLocator.GetConfiguration()["ConnectionStrings:DefaultConnection"]);
                RolePermissionMapper.RegisterRolePermission<BlogDbContext>(blogDbContext);
                //����Ա
                options.AddPolicy("Admin", policy => RolePermissionMapper.GetPermissionForRole("0").ForEach(p => policy.Requirements.Add(new PermissionRequirement(p))));
                //�ÿ�
                options.AddPolicy("Guest", policy => RolePermissionMapper.GetPermissionForRole("1").ForEach(p => policy.Requirements.Add(new PermissionRequirement(p))));
                //��ͨ�û�
                options.AddPolicy("User", policy => RolePermissionMapper.GetPermissionForRole("2").ForEach(p => policy.Requirements.Add(new PermissionRequirement(p))));
            });
            builder.Services.AddControllers(options =>
            {
                WebPipeExtension.AddFilter(options);
            }).AddJsonOptions(options =>
            {
                //����nullֵ
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
                    // ���� Swagger UI ��·������ѡ��
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API v1");
                    options.RoutePrefix = string.Empty; // ʹ Swagger UI �ڸ�·����ʾ
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
