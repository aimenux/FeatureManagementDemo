using ConsoleApp.Constants;
using ConsoleApp.Models;
using Microsoft.FeatureManagement;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureManager _featureManager;

        public FeatureService(IFeatureManager featureManager)
        {
            _featureManager = featureManager;
        }

        public async Task ListFeaturesAsync()
        {
            const string contextualFeature = nameof(FeatureFlags.FeatureI);

            var features = Enum.GetNames<FeatureFlags>()
                .Where(f => f != contextualFeature)
                .ToList();

            foreach (var feature in features)
            {
                var isEnabled = await _featureManager.IsEnabledAsync(feature);
                Console.WriteLine($"Feature {feature}: {isEnabled}");
            }

            await ListContextualFeatureAsync(contextualFeature);
        }

        private async Task ListContextualFeatureAsync(string feature)
        {
            var context = new RuntimeInformationContext("Windows", "X64");
            var isEnabled = await _featureManager.IsEnabledAsync(feature, context);
            Console.WriteLine($"Feature {feature}: {isEnabled}");
        }
    }
}
