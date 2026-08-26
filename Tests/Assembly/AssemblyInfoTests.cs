using Vosiz.Assembly;
using Vosiz.Commons;

namespace Tests.Assembly
{

    public static class AssemblyInfoTests
    {

        // Constructor stores every given field as-is
        public static void ConstructorStoresAllFields() {

            Version version = Version.Create("1.2.3", "{0}.{1}.{2}");
            AssemblyInfo info = new AssemblyInfo(version, "Windows", true, "beta", "b1234");

            Check.Equal(version, info.Version);
            Check.Equal("Windows", info.OS);
            Check.True(info.IsProduction, "IsProduction should be true");
            Check.Equal("beta", info.Label);
            Check.Equal("b1234", info.BuildId);
        }

        // Constructor throws for a null version
        public static void ConstructorThrowsForNullVersion() {

            Check.Throws<AssertException>(() => new AssemblyInfo(null, "Windows", true, "beta", "b1234"));
        }

        // ToString renders just the version
        public static void ToStringRendersVersionOnly() {

            Version version = Version.Create("1.2.3", "{0}.{1}.{2}");
            AssemblyInfo info = new AssemblyInfo(version, "Windows", true, "beta", "b1234");

            Check.Equal("1.2.3", info.ToString());
        }

        // ToFullString appends label, build id, OS and compile mode
        public static void ToFullStringIncludesAllFields() {

            Version version = Version.Create("1.2.3", "{0}.{1}.{2}");
            AssemblyInfo info = new AssemblyInfo(version, "Windows", true, "beta", "b1234");

            Check.Equal("1.2.3-beta+b1234 (Windows, Release)", info.ToFullString());
        }

        // ToFullString omits fields that aren't set
        public static void ToFullStringOmitsMissingFields() {

            Version version = Version.Create("1.2.3", "{0}.{1}.{2}");
            AssemblyInfo info = new AssemblyInfo(version, null, false, null, null);

            Check.Equal("1.2.3 (Debug)", info.ToFullString());
        }

    }
}
