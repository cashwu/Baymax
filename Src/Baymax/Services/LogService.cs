using System.Collections.Generic;
using Baymax.Exception;
using Baymax.Services.Interface;
using Microsoft.AspNetCore.Hosting;

namespace Baymax.Services
{
    internal class LogService : ILogService
    {
        private readonly IEnumerable<ILogBase> _logs;

        public LogService(IEnumerable<ILogBase> logs)
        {
            _logs = logs;
        }

        public void Log(System.Exception ex)
        {
            foreach (var log in _logs)
            {
                if (ex is EntityValidationException validationExceptions)
                {
                    foreach (var validationException in validationExceptions.Exceptions)
                    {
                        log.LogAsync(validationException);
                    }
                }
                else
                {
                    log.LogAsync(ex);
                }
            }
        }

        public void Log(string msg)
        {
            foreach (var log in _logs)
            {
                log.LogAsync(msg);
            }
        }
    }
}