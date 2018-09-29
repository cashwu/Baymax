using System;
using System.Collections.Generic;
using Baymax.Services.Interface;
using Microsoft.AspNetCore.Hosting;

namespace Baymax.Services
{
    public class LogService : ILogService
    {
        private readonly IHostingEnvironment _env;
        private readonly IEnumerable<ILogBase> _logs;

        public LogService(IHostingEnvironment env,
                          IEnumerable<ILogBase> logs)
        {
            _env = env;
            _logs = logs;
        }

        public void Log(Exception ex)
        {
            foreach (var log in _logs)
            {
                log.LogAsync(ex, _env.EnvironmentName);
            }
        }

        public void Log(string msg)
        {
            foreach (var log in _logs)
            {
                log.LogAsync(msg, _env.EnvironmentName);
            }
        }
    }
}