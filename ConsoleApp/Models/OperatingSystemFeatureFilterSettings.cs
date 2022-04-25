using System.Runtime.InteropServices;

namespace ConsoleApp.Models;

public class OperatingSystemFeatureFilterSettings
{
    public string Value { get; set; }

    public OSPlatform GetFeatureOsPlatform() => OSPlatform.Create(Value);

    public static OSPlatform? GetCurrentOsPlatform()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return OSPlatform.Windows;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return OSPlatform.Linux;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return OSPlatform.OSX;
        }

        return null;
    }
}