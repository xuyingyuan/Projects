using System;
using System.Collections.Generic;
using System.Text;

namespace FreshingStore.Logger.Logging
{
    public interface ILoggerManager
    {
        void Information(string message);
        void Warning(string message);
        void Debug(string message);
        void Error(string message);
     
        void Trace(string message);

    }
}
