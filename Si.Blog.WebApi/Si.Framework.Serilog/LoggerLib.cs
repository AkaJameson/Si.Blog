using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System.Reflection;

namespace Si.Framework.Serilog
{
    public class LoggerLib
    {
        /// <summary>
        /// 初始化 Serilog 配置
        /// </summary>
        public static void InitLogger()
        {
            // 从嵌入式资源或类库目录中加载配置文件
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "logs", DateTime.Now.ToString("yyyy-MM-dd"), "Information-.log"),
                restrictedToMinimumLevel: LogEventLevel.Information,
                rollingInterval:RollingInterval.Day,
                retainedFileCountLimit:10,
                fileSizeLimitBytes:10485760,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}"
                )
                .WriteTo.File(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "logs", DateTime.Now.ToString("yyyy-MM-dd"), "Debug-.log"),
                restrictedToMinimumLevel: LogEventLevel.Debug,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 10,
                fileSizeLimitBytes: 10485760,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}"
                )
                .WriteTo.File(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "logs", DateTime.Now.ToString("yyyy-MM-dd"), "Fatal-.log"),
                restrictedToMinimumLevel: LogEventLevel.Fatal,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 10,
                fileSizeLimitBytes: 10485760,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}"
                )
                .WriteTo.File(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "logs", DateTime.Now.ToString("yyyy-MM-dd"), "Error-.log"),
                restrictedToMinimumLevel: LogEventLevel.Error,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 10,
                fileSizeLimitBytes: 10485760,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}"
                )
                .WriteTo.File(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "logs", DateTime.Now.ToString("yyyy-MM-dd"), "Warning-.log"),
                restrictedToMinimumLevel: LogEventLevel.Warning,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 10,
                fileSizeLimitBytes: 10485760,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}"
                )
                .CreateLogger();
        }
        /// <summary>
        /// 停止 Serilog，并释放资源
        /// </summary>
        public static void ShutdownLogger()
        {
            Log.CloseAndFlush();
        }
       
    }
}
