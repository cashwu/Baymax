using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Baymax.Extension;
using Baymax.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Timer = System.Timers.Timer;

[assembly: InternalsVisibleTo("Baymax.Tests")]

namespace Baymax.Services
{
    internal sealed class BaymaxBackgroundService<T> : IHostedService, IDisposable where T : IBackgroundProcessService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostingEnvironment _env;
        private readonly Timer _timer;
        private readonly int _interval;

        public BaymaxBackgroundService(IConfiguration configuration, IServiceProvider serviceProvider, IHostingEnvironment env)
        {
            _serviceProvider = serviceProvider;
            _env = env;

            _interval = configuration[$"BackgroundService:{typeof(T).Name}"].ToInt(0);

            if (_interval > 0)
            {
                _timer = new Timer();
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (_env.IsTest())
            {
                return Task.CompletedTask;
            }

            if (_timer != null)
            {
                _timer.Enabled = true;
                _timer.Interval = _interval;
                _timer.Elapsed += (sender, args) => DoWork();
                _timer.Start();
            }

            DoWork();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (_env.IsTest())
            {
                return Task.CompletedTask;
            }

            if (_timer != null)
            {
                _timer.Enabled = false;
                _timer.Stop();
            }

            StopWork();

            return Task.CompletedTask;
        }

        private void DoWork()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var processingService = scope.ServiceProvider.GetRequiredService<T>();
                processingService.DoWork();
            }
        }

        private void StopWork()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var processingService = scope.ServiceProvider.GetRequiredService<T>();
                processingService.StopWork();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer?.Dispose();
            }
        }
    }
}