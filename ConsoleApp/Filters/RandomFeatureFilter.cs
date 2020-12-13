using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using System;
using System.Threading.Tasks;

namespace ConsoleApp.Filters
{
    [FilterAlias(Alias)]
    public class RandomFeatureFilter : IFeatureFilter
    {
        private const string Alias = "Random";

        private readonly ILogger _logger;

        public RandomFeatureFilter(ILogger logger)
        {
            _logger = logger;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var value = Randomize.GetNumber();
            var isEnabled = value % 3 == 0;
            if (!isEnabled)
            {
                _logger.LogWarning($"Feature '{Alias}' is not enabled for current value '{value}'.");
            }
            return Task.FromResult(isEnabled);
        }

        private static class Randomize
        {
            private static readonly Random Random = new Random(Guid.NewGuid().GetHashCode());

            public static int GetNumber() => Random.Next();
        }
    }
}
