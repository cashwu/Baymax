using Microsoft.AspNetCore.Hosting;

namespace Baymax.Extension
{
    public static class IHostingEnvironmentExtensions
    {
        public static bool IsTest(this IHostingEnvironment env)
        {
           return env.IsEnvironment("test");
        }
    }
}