using System;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using System.Threading.Tasks;

namespace ConsoleApp.Filters;

[FilterAlias("Random")]
public class RandomFeatureFilter : IFeatureFilter
{
    private readonly ILogger _logger;

    public RandomFeatureFilter(ILogger logger)
    {
        _logger = logger;
    }

    public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
    {
        var value = GetRandomNumber();
        var isEnabled = value % 3 == 0;
        if (!isEnabled)
        {
            _logger.LogWarning($"Feature '{context.FeatureName}' is not enabled for current value '{value}'.");
        }
        return Task.FromResult(isEnabled);
    }

    private static int GetRandomNumber()
    {
        var random = new Random(Guid.NewGuid().GetHashCode());
        return random.Next(0, int.MaxValue);
    }
}