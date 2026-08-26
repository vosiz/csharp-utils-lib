using Vosiz.Assembly;
using Vosiz.Commons;

namespace Tests.Assembly
{

    public static class VersionTests
    {

        // True when the version is ordinally lower
        public static void RequiresUpdateReturnsTrueWhenLower() {

            Version current = Version.Create("1.2.3", "{0}.{1}.{2}");
            Version required = Version.Create("1.2.4", "{0}.{1}.{2}");

            Check.True(current.RequiresUpdate(required));
        }

        // False when both versions are equal
        public static void RequiresUpdateReturnsFalseWhenEqual() {

            Version current = Version.Create("1.2.3", "{0}.{1}.{2}");
            Version required = Version.Create("1.2.3", "{0}.{1}.{2}");

            Check.False(current.RequiresUpdate(required));
        }

        // False when this version is ordinally higher
        public static void RequiresUpdateReturnsFalseWhenHigher() {

            Version current = Version.Create("1.2.4", "{0}.{1}.{2}");
            Version required = Version.Create("1.2.3", "{0}.{1}.{2}");

            Check.False(current.RequiresUpdate(required));
        }

        // Compares Major first, ignoring lower components
        public static void RequiresUpdateComparesMajorFirst() {

            Version current = Version.Create("2.0.0", "{0}.{1}.{2}");
            Version required = Version.Create("1.9.9", "{0}.{1}.{2}");

            Check.False(current.RequiresUpdate(required));
        }

        // RequiresUpdate throws for a null required version
        public static void RequiresUpdateThrowsForNullRequired() {

            Version current = Version.Create("1.2.3", "{0}.{1}.{2}");

            Check.Throws<AssertException>(() => current.RequiresUpdate(null));
        }

    }
}
