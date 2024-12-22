using Serilog;

namespace Si.Framework.Serilog
{
    public static class LogHub
    {
        public static void Info(string message)
        {
            Log.Information(message);
        }
        public static void Error(string message)
        {
            Log.Error(message);
        }
        public static void Debug(string message)
        {
            Log.Debug(message);
        }
        public static void Fatal(string message)
        {
            Log.Fatal(message);
        }
        public static void Warning(string message)
        {
            Log.Warning(message);
        }

    }
}
