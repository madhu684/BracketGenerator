using BracketGeneratorTestApp.Shared.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGeneratorTestApp.Shared
{
    public class ConsoleLogger : ILogger
    {
        public void LogInformation(string message)
        {
            Console.WriteLine($"[INFO] {message}");
        }

        public void LogError(string message, Exception ex)
        {
            Console.WriteLine($"[ERROR] {message}\n{ex}");
        }
    }
}
