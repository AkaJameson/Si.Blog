namespace Si.Framework.ToolKit
{
    public class ServiceLocator
    {
        private static IServiceProvider _serviceProvider;
        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public static T GetService<T>()
        {
            return (T)_serviceProvider.GetService(typeof(T));
        }
        public static IServiceProvider GetServiceProvider() { return _serviceProvider; }
    }
}
