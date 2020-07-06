﻿
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreshingStore.Logger.Logging
{
    public class LoggerManager : ILoggerManager
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public LoggerManager()
        {
        }
        public void Information(string message)
        {
            logger.Info(message);
        }

        public void Warning(string message)
        {
            logger.Warn(message);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

       

        public void Trace(string message)
        {
            logger.Trace(message);
        }
    }
}
