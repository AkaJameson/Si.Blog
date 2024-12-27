using Microsoft.Extensions.Configuration;

namespace Si.Framework.Base.Utility
{
    public static class ServiceLocator
    {
        private static IServiceProvider _serviceProvider;
        private static IConfiguration _configuration;
        
        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public static T GetService<T>()
        {
            return (T)_serviceProvider.GetService(typeof(T));
        }
        public static void AddSettings(this ConfigurationManager configuration)
        {
            configuration.AddJsonFile($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json")}"
                , optional: true, reloadOnChange: true);
            _configuration = configuration;
        }
        public static IServiceProvider GetServiceProvider() { return _serviceProvider; }

        public static IConfiguration GetConfiguration() { return _configuration; }
    }
}
