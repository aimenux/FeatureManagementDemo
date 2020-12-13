﻿using ConsoleApp.Models;
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
            var settings = context.Parameters.Get<OperatingSystemFeatureFilterSettings>();
            var isEnabled = settings.IsWindows();
            if (!isEnabled)
            {
                _logger.LogWarning($"Feature '{Alias}' is not enabled for '{settings.Value}'.");
            }
            return Task.FromResult(isEnabled);
        }
    }
}