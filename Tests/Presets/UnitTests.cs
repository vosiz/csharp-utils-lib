using System;
using VPresets = Vosiz.Presets;

namespace Tests.Presets
{

    public static class UnitTests
    {

        // All dictionary contains every preset unit
        public static void AllContainsEveryPresetUnit() {

            Check.True(VPresets.Unit.All.ContainsKey("Kelvin"), "Missing Kelvin");
            Check.True(VPresets.Unit.All.ContainsKey("Ampere"), "Missing Ampere");
            Check.True(VPresets.Unit.All.ContainsKey("Volt"), "Missing Volt");
            Check.True(VPresets.Unit.All.ContainsKey("Meter"), "Missing Meter");
        }

        // Shortcut properties match the symbols they represent
        public static void ShortcutsHaveExpectedSymbols() {

            Check.Equal("K", VPresets.Unit.Kelvin.Symbol);
            Check.Equal("A", VPresets.Unit.Ampere.Symbol);
            Check.Equal("V", VPresets.Unit.Volt.Symbol);
            Check.Equal("m", VPresets.Unit.Meter.Symbol);
        }

        // Shortcuts and dictionary entries point to the same instance
        public static void ShortcutsMatchDictionaryEntries() {

            Check.True(ReferenceEquals(VPresets.Unit.Kelvin, VPresets.Unit.All["Kelvin"]), "Kelvin instance mismatch");
            Check.True(ReferenceEquals(VPresets.Unit.Ampere, VPresets.Unit.All["Ampere"]), "Ampere instance mismatch");
            Check.True(ReferenceEquals(VPresets.Unit.Volt, VPresets.Unit.All["Volt"]), "Volt instance mismatch");
            Check.True(ReferenceEquals(VPresets.Unit.Meter, VPresets.Unit.All["Meter"]), "Meter instance mismatch");
        }

        // All preset units allow prefix conversion
        public static void AllPresetUnitsAllowPrefixConversion() {

            Check.True(VPresets.Unit.Kelvin.AllowsPrefixConversion, "Kelvin should allow prefix conversion");
            Check.True(VPresets.Unit.Ampere.AllowsPrefixConversion, "Ampere should allow prefix conversion");
            Check.True(VPresets.Unit.Volt.AllowsPrefixConversion, "Volt should allow prefix conversion");
            Check.True(VPresets.Unit.Meter.AllowsPrefixConversion, "Meter should allow prefix conversion");
        }

    }
}
