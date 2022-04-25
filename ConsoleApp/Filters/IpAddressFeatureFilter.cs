using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using System.Threading.Tasks;
using ConsoleApp.Models;

namespace ConsoleApp.Filters;

[FilterAlias("IpAddress")]
public class IpAddressFeatureFilter : IFeatureFilter
{
    private readonly ILogger _logger;

    public IpAddressFeatureFilter(ILogger logger)
    {
        _logger = logger;
    }

    public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
    {
        var currentIpAddress = IpAddressFeatureFilterSettings.GetCurrentIpAddress();
        var settings = context.Parameters.Get<IpAddressFeatureFilterSettings>();
        var isEnabled = settings.Values.Contains(currentIpAddress);
        if (!isEnabled)
        {
            _logger.LogWarning($"Feature '{context.FeatureName}' is not enabled for current ip address '{currentIpAddress}'.");
        }
        return Task.FromResult(isEnabled);
    }
}