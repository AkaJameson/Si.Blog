using Si.Framework.ToolKit.Extension;

namespace Con
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var password = "admin123";
            var passwordEncrep = password.AESEncrypt();
            Console.WriteLine(passwordEncrep);
            Console.ReadLine();
        }
    }
}
