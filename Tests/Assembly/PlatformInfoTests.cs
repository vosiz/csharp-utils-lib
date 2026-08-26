using Vosiz.Assembly;
using PlatformOS = Vosiz.Enums.PlatformOS;

namespace Tests.Assembly
{

    // Stand-in for a runtime-specific subclass (e.g. Unity/MAUI) plugging in its own detection
    public class FakePlatformInfo : PlatformInfo
    {

        public override PlatformOS Detect() {

            return PlatformOS.WebGL;
        }
    }

    public static class PlatformInfoTests
    {

        // Default detection returns Windows here
        public static void DetectReturnsWindows() {

            PlatformInfo info = new PlatformInfo();

            Check.Equal(PlatformOS.Windows, info.Detect());
        }

        // Detect is virtual and overridable by subclasses
        public static void DetectIsOverridable() {

            PlatformInfo info = new FakePlatformInfo();

            Check.Equal(PlatformOS.WebGL, info.Detect());
        }

    }
}
