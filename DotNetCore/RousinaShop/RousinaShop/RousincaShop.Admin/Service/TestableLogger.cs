using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace RousincaShop.Admin.Service
{
    public class TestableLogger:IloggerService
    {
        private readonly ILogger _logger;

        public TestableLogger(ILogger<TestableLogger> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message, params object[] parameters)
        {
            _logger.LogInformation(message, parameters);
        }
    }
}
