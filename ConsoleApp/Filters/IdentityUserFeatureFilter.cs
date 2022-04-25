using System;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using System.Threading.Tasks;

namespace ConsoleApp.Filters;

[FilterAlias("Identity")]
public class IdentityUserFeatureFilter : IFeatureFilter
{
    private readonly ContextualTargetingFilter _contextualTargetingFilter;
    private readonly ILogger _logger;

    public IdentityUserFeatureFilter(ContextualTargetingFilter contextualTargetingFilter, ILogger logger)
    {
        _contextualTargetingFilter = contextualTargetingFilter ?? throw new ArgumentNullException(nameof(contextualTargetingFilter));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
    {
        const string userId = "Alice";

        var targetingContext = new TargetingContext
        {
            UserId = userId
        };

        var isEnabled = await _contextualTargetingFilter.EvaluateAsync(context, targetingContext);
        if (!isEnabled)
        {
            _logger.LogWarning($"Feature '{context.FeatureName}' is not enabled for current user '{userId}'.");
        }

        return isEnabled;
    }
}