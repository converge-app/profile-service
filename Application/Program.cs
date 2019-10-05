using System;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var assemblyName = typeof(Startup).GetTypeInfo().Assembly.FullName;
            var protocol = Environment.GetEnvironmentVariable("PROTOCOL") ?? "http";
            var port = Environment.GetEnvironmentVariable("HTTP_PORT") ?? "80";

            return WebHost.CreateDefaultBuilder(args)
                .UseUrls($"{protocol}://0.0.0.0:{port}")
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.AddEventSourceLogger();
                })
                .UseStartup(assemblyName);
        }
    }
}
