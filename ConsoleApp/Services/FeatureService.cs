using Microsoft.FeatureManagement;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApp.Extensions;
using ConsoleApp.Models;
using Microsoft.FeatureManagement.FeatureFilters;

namespace ConsoleApp.Services;

public interface IFeatureService
{
    Task<FeaturesResponse> GetFeaturesAsync(CancellationToken cancellationToken = default);

    Task<FeaturesResponse> GetContextualFeaturesAsync(CancellationToken cancellationToken = default);
}

public class FeatureService : IFeatureService
{
    private readonly IFeatureManager _featureManager;

    public FeatureService(IFeatureManager featureManager)
    {
        _featureManager = featureManager ?? throw new ArgumentNullException(nameof(featureManager));
    }

    public async Task<FeaturesResponse> GetFeaturesAsync(CancellationToken cancellationToken = default)
    {
        ConsoleColor.Green.WriteLine("Listing features");

        var responses = new FeaturesResponse();
        var features = Enum.GetNames<FeatureFlags>();

        foreach (var feature in features)
        {
            var isEnabled = await _featureManager.IsEnabledAsync(feature);
            responses.Add(new FeatureResponse
            {
                FeatureName = feature,
                IsEnabled = isEnabled
            });
        }

        return responses;
    }

    public async Task<FeaturesResponse> GetContextualFeaturesAsync(CancellationToken cancellationToken = default)
    {
        ConsoleColor.Green.WriteLine("Listing contextual features");

        var responses = new FeaturesResponse();

        var targetingContexts = new List<TargetingContext>
        {
            new TargetingContext
            {
                UserId = "Julia",
            },
            new TargetingContext
            {
                UserId = "Adam",
            },
            new TargetingContext
            {
                UserId = "Abdul",
                Groups = new []{"HR"}
            },
            new TargetingContext
            {
                UserId = "Irene",
                Groups = new []{"IT"}
            }
        };

        foreach (var targetingContext in targetingContexts)
        {
            const string featureName = nameof(FeatureFlags.FeatureK);
            var isEnabled = await _featureManager.IsEnabledAsync(featureName, targetingContext);
            responses.Add(new FeatureResponse
            {
                FeatureName = featureName,
                FeatureContext = targetingContext,
                IsEnabled = isEnabled
            });
        }

        var runtimeContexts = new List<RuntimeContext>
        {
            new RuntimeContext("Windows", "X86"),
            new RuntimeContext("Windows", "X64")
        };

        foreach (var runtimeContext in runtimeContexts)
        {
            const string featureName = nameof(FeatureFlags.FeatureL);
            var isEnabled = await _featureManager.IsEnabledAsync(featureName, runtimeContext);
            responses.Add(new FeatureResponse
            {
                FeatureName = featureName,
                FeatureContext = runtimeContext,
                IsEnabled = isEnabled
            });
        }

        return responses;
    }
}