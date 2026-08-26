using System.Runtime.InteropServices;
using PlatformOS = Vosiz.Enums.PlatformOS;

namespace Vosiz.Assembly
{

    public class PlatformInfo
    {

        public PlatformInfo() { }


        // Detects the current OS platform
        // - falls back to Unknown for runtimes it can't see into (MAUI/Unity/Xamarin/WebGL)
        // - override in a subclass to plug in that runtime's own detection API
        public virtual PlatformOS Detect()
        {

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return PlatformOS.Windows;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return PlatformOS.Linux;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return PlatformOS.MacOS;

            return PlatformOS.Unknown;
        }

    }
}
