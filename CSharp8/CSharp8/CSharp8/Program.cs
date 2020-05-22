using System;
using CSharp8.defaultInterface;
namespace CSharp8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            checkILooger();
        }

        public static void checkILooger()
        {
            ILogger consoleLogger = new ConsoleLogger();
            consoleLogger.WriteWarning("cool no code duplication!");

            ILogger tracelogger = new TraceLogger();
            tracelogger.WriteInformaion("cool no code duplication!");

        }
    }
}
