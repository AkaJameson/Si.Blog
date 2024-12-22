using Microsoft.Extensions.Configuration;
using Serilog;
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
            var configuration = LoadEmbeddedConfiguration();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            // 开启日志自动清理任务（每隔一天清理一次）
            StartCleanupTask();
        }
        /// <summary>
        /// 停止 Serilog，并释放资源
        /// </summary>
        public static void ShutdownLogger()
        {
            Log.CloseAndFlush();
            _cleanupTimer?.Dispose();
        }
        /// <summary>
        /// 加载嵌入式资源中的日志配置文件
        /// </summary>
        /// <returns></returns>
        private static IConfigurationRoot LoadEmbeddedConfiguration()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"{assembly.GetName().Name}.LogSettings.json";
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                throw new FileNotFoundException($"Embedded resource '{resourceName}' not found.");
            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();
            var configuration = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)))
                .Build();
            return configuration;
        }
        private static Timer _cleanupTimer;
        /// <summary>
        /// 开启日志自动清理任务
        /// </summary>
        private static void StartCleanupTask()
        {
            _cleanupTimer = new Timer(CleanupOldLogFolders, null, TimeSpan.Zero, TimeSpan.FromDays(1));
        }

        /// <summary>
        /// 清理 30 天前的日志文件夹
        /// </summary>
        /// <param name="state"></param>
        private static void CleanupOldLogFolders(object state)
        {
            try
            {
                var logDirectory = Path.Combine(Directory.GetCurrentDirectory(),"..","logs");
                if (!Directory.Exists(logDirectory)) return;
                var directories = Directory.GetDirectories(logDirectory);
                foreach (var dir in directories)
                {
                    if (DateTime.TryParse(Path.GetFileName(dir), out var folderDate))
                    {
                        if (folderDate < DateTime.Now.AddDays(-30))
                        {
                            Directory.Delete(dir, true); // 递归删除文件夹及其内容
                            Log.Information($"Deleted old log folder: {dir}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to clean up old log folders.");
            }
        }
    }
}
