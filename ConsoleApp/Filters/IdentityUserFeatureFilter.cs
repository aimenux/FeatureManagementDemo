using ConsoleApp.Filters.Contexts.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using System.Threading.Tasks;

namespace ConsoleApp.Filters
{
    [FilterAlias(Alias)]
    public class IdentityUserFeatureFilter : IFeatureFilter
    {
        private const string Alias = "Identity";

        private readonly ContextualTargetingFilter _contextualTargetingFilter;
        private readonly IIdentityUserProvider _identityUserProvider;
        private readonly ILogger _logger;

        public IdentityUserFeatureFilter(
            ContextualTargetingFilter contextualTargetingFilter,
            IIdentityUserProvider identityUserProvider,
            ILogger logger)
        {
            _contextualTargetingFilter = contextualTargetingFilter;
            _identityUserProvider = identityUserProvider;
            _logger = logger;
        }

        public async Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var identityUser = await _identityUserProvider.GetRandomIdentityUserAsync();

            var targetingContext = new TargetingContext
            {
                UserId = identityUser.Id,
                Groups = identityUser.Groups
            };

            var isEnabled = await _contextualTargetingFilter.EvaluateAsync(context, targetingContext);
            if (!isEnabled)
            {
                _logger.LogWarning($"Feature '{Alias}' is not enabled for current identity user '{identityUser}'.");
            }

            return isEnabled;
        }
    }
}
