using ConsoleApp.Filters;
using ConsoleApp.Filters.Contexts.Identity;
using ConsoleApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var service = serviceScope.ServiceProvider.GetService<IFeatureService>();
                await service.ListFeaturesAsync();
            }

            Console.WriteLine("Press any key to exit !");
            Console.ReadKey();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddCommandLine(args);
                config.AddEnvironmentVariables();
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureLogging((hostingContext, loggingBuilder) =>
            {
                loggingBuilder.AddConsoleLogger();
                loggingBuilder.AddNonGenericLogger();
                loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
            })
            .ConfigureServices((hostingContext, services) =>
            {
                services.AddFeatureManagement()
                        .AddFeatureFilter<PercentageFilter>()
                        .AddFeatureFilter<TimeWindowFilter>()
                        .AddFeatureFilter<RandomFeatureFilter>()
                        .AddFeatureFilter<IpAddressFeatureFilter>()
                        .AddFeatureFilter<ContextualTargetingFilter>()
                        .AddFeatureFilter<IdentityUserFeatureFilter>()
                        .AddFeatureFilter<OperatingSystemFeatureFilter>()
                        .AddFeatureFilter<RuntimeInformationFeatureFilter>();

                services.AddTransient<ContextualTargetingFilter>();
                services.AddTransient<IFeatureService, FeatureService>();
                services.AddTransient<IIdentityUserProvider, InMemoryIdentityUserProvider>();
            });

        private static void AddConsoleLogger(this ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddSimpleConsole(options =>
            {
                options.IncludeScopes = true;
                options.TimestampFormat = "[HH:mm:ss:fff] ";
                options.ColorBehavior = LoggerColorBehavior.Enabled;
            });
        }

        private static void AddNonGenericLogger(this ILoggingBuilder loggingBuilder)
        {
            var services = loggingBuilder.Services;
            services.AddSingleton(serviceProvider =>
            {
                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
                return loggerFactory.CreateLogger("FeatureManagementDemo");
            });
        }
    }
}
