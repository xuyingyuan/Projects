using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CSharp8.defaultInterface
{
    public enum LogLevel
    {
        Information,
        Warning,
        Error
    }

  public  interface ILogger
    {
      public   void WriteCore(LogLevel Level, string message);

        void WriteInformaion(string message)
        {
            WriteCore(LogLevel.Information, message);
        }

        void WriteWarning(string message)
        {
            WriteCore(LogLevel.Warning, message);
        }

        void WriteError(string message)
        {
            WriteCore(LogLevel.Error, message);
        }
    }
   public  class ConsoleLogger: ILogger
    {
        public void WriteCore(LogLevel Level, string message)
        {
            Console.WriteLine($"{Level}: {message}");
        }        
    }


   public class TraceLogger : ILogger
    {
        public void WriteCore(LogLevel Level, string message)
        {
           switch(Level)
            {
                case LogLevel.Information:
                    Trace.TraceInformation(message);
                    break;
                case LogLevel.Warning:
                    Trace.TraceWarning(message);
                    break;
                case LogLevel.Error:
                    Trace.TraceError(message);
                    break;

            }
        }
    }

}
