using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using System.Threading.Tasks;
using ConsoleApp.Models;

namespace ConsoleApp.Filters;

[FilterAlias("OperatingSystem")]
public class OperatingSystemFeatureFilter : IFeatureFilter
{
    private readonly ILogger _logger;

    public OperatingSystemFeatureFilter(ILogger logger)
    {
        _logger = logger;
    }

    public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
    {
        var currentOsPlatform = OperatingSystemFeatureFilterSettings.GetCurrentOsPlatform();
        var settings = context.Parameters.Get<OperatingSystemFeatureFilterSettings>();
        var isEnabled = settings.GetFeatureOsPlatform() == currentOsPlatform;
        if (!isEnabled)
        {
            _logger.LogWarning($"Feature '{context.FeatureName}' is not enabled for current operating system '{currentOsPlatform}'.");
        }
        return Task.FromResult(isEnabled);
    }
}