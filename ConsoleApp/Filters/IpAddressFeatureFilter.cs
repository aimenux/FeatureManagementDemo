using ConsoleApp.Filters.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using System.Threading.Tasks;

namespace ConsoleApp.Filters
{
    [FilterAlias(Alias)]
    public class IpAddressFeatureFilter : IFeatureFilter
    {
        private const string Alias = "IpAddress";

        private readonly ILogger _logger;

        public IpAddressFeatureFilter(ILogger logger)
        {
            _logger = logger;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var currentIpAddress = IpAddressFilterSettings.GetCurrentIpAddress();
            var settings = context.Parameters.Get<IpAddressFilterSettings>();
            var isEnabled = settings.Values.Contains(currentIpAddress);
            if (!isEnabled)
            {
                _logger.LogWarning($"Feature '{Alias}' is not enabled for current ip address '{currentIpAddress}'.");
            }
            return Task.FromResult(isEnabled);
        }
    }
}
