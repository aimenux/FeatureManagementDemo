using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using System;
using System.Threading.Tasks;
using ConsoleApp.Extensions;
using ConsoleApp.Filters;
using ConsoleApp.Services;
using Microsoft.FeatureManagement.FeatureFilters;

namespace ConsoleApp;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var serviceScope = host.Services.CreateScope())
        {
            var service = serviceScope.ServiceProvider.GetRequiredService<IFeatureService>();
            var features = await service.GetFeaturesAsync();
            foreach (var feature in features)
            {
                Console.WriteLine(ObjectDumper.Dump(feature));
            }
            var contextualFeatures = await service.GetContextualFeaturesAsync();
            foreach (var contextualFeature in contextualFeatures)
            {
                Console.WriteLine(ObjectDumper.Dump(contextualFeature));
            }
        }

        Console.WriteLine("Press any key to exit !");
        Console.ReadKey();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile();
                config.AddEnvironmentVariables();
                config.AddCommandLine(args);
            })
            .ConfigureLogging((hostingContext, loggingBuilder) =>
            {
                loggingBuilder.AddConsoleLogger();
                loggingBuilder.AddNonGenericLogger();
                loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
            })
            .ConfigureServices((_, services) =>
            {
                services
                    .AddFeatureManagement()
                    .AddFeatureFilter<PercentageFilter>()
                    .AddFeatureFilter<TimeWindowFilter>()
                    .AddFeatureFilter<RandomFeatureFilter>()
                    .AddFeatureFilter<RuntimeFeatureFilter>()
                    .AddFeatureFilter<IpAddressFeatureFilter>()
                    .AddFeatureFilter<ContextualTargetingFilter>()
                    .AddFeatureFilter<IdentityUserFeatureFilter>()
                    .AddFeatureFilter<OperatingSystemFeatureFilter>();

                services.AddTransient<ContextualTargetingFilter>();
                services.AddTransient<IFeatureService, FeatureService>();
            });
}