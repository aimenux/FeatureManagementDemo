using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using System.Threading.Tasks;
using ConsoleApp.Models;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement.FeatureFilters;

namespace ConsoleApp.Filters;

[FilterAlias("Runtime")]
public class RuntimeFeatureFilter : IContextualFeatureFilter<RuntimeContext>
{
    private readonly IOptions<TargetingEvaluationOptions> _options;
    private readonly ILogger _logger;

    public RuntimeFeatureFilter(IOptions<TargetingEvaluationOptions> options, ILogger logger)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext featureFilterContext, RuntimeContext appContext)
    {
        var settings = featureFilterContext.Parameters.Get<RuntimeFeatureFilterSettings>();
        var runtimeContext = new RuntimeContext(settings.OperatingSystem, settings.ProcessArchitecture);
        var isEnabled = runtimeContext.Equals(appContext);
        if (!isEnabled)
        {
            _logger.LogWarning($"Feature '{featureFilterContext.FeatureName}' is not enabled for current context '{appContext}'.");
        }
        return Task.FromResult(isEnabled);
    }
}