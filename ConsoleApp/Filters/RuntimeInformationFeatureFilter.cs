using ConsoleApp.Filters.Contexts.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using System.Threading.Tasks;

namespace ConsoleApp.Filters
{
    [FilterAlias(Alias)]
    public class RuntimeInformationFeatureFilter : IContextualFeatureFilter<RuntimeInformationContext>
    {
        private const string Alias = "RuntimeInformation";

        private readonly ILogger _logger;

        public RuntimeInformationFeatureFilter(ILogger logger)
        {
            _logger = logger;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext featureFilterContext)
        {
            var appContext = new RuntimeInformationContext("Windows", "X64");
            return EvaluateAsync(featureFilterContext, appContext);
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext featureFilterContext, RuntimeInformationContext appContext)
        {
            var runtimeContext = featureFilterContext.Parameters.Get<RuntimeInformationContext>();
            var isEnabled = runtimeContext.Equals(appContext);
            if (!isEnabled)
            {
                _logger.LogWarning($"Feature '{Alias}' is not enabled for current context '{appContext}'.");
            }
            return Task.FromResult(isEnabled);
        }
    }
}
