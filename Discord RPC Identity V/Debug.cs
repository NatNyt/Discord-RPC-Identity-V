using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_RPC_Identity_V
{
    internal class Debug
    {
        public static void Info(string instance, string message) {
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            Console.WriteLine($"[{currentTime}] {instance}::Info {message}");
        }
        public static void Error(string instance, string message)
        {
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            Console.WriteLine($"[{currentTime}] {instance}::Error {message}");
        }
    }
}
