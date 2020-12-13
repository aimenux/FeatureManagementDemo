using System;

namespace ConsoleApp.Models
{
    public class OperatingSystemFeatureFilterSettings
    {
        public string Value { get; set; }

        public bool IsWindows()
        {
            return string.Equals(Value, "Windows", StringComparison.OrdinalIgnoreCase);
        }
    }
}
