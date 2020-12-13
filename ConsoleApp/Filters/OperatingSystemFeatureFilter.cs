using ConsoleApp.Filters.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using System.Threading.Tasks;

namespace ConsoleApp.Filters
{
    [FilterAlias(Alias)]
    public class OperatingSystemFeatureFilter : IFeatureFilter
    {
        private const string Alias = "OperatingSystem";

        private readonly ILogger _logger;

        public OperatingSystemFeatureFilter(ILogger logger)
        {
            _logger = logger;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var currentOSPlatform = OperatingSystemFeatureFilterSettings.GetCurrentOSPlatform();
            var settings = context.Parameters.Get<OperatingSystemFeatureFilterSettings>();
            var isEnabled = settings.GetFeatureOSPlatform() == currentOSPlatform;
            if (!isEnabled)
            {
                _logger.LogWarning($"Feature '{Alias}' is not enabled for current operating system '{currentOSPlatform}'.");
            }
            return Task.FromResult(isEnabled);
        }
    }
}
