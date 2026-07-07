using System;
using Vosiz.Commons;

namespace Tests.Commons
{

    public static class UnitTests
    {

        // Default constructor sets sane defaults
        public static void DefaultConstructorSetsDefaults() {

            Unit unit = new Unit();

            Check.Equal(string.Empty, unit.Symbol);
            Check.Equal(UnitSymbolPlacement.AfterWithSpace, unit.Placement);
            Check.True(unit.AllowsPrefixConversion, "AllowsPrefixConversion should default to true");
        }

        // Constructor with just a symbol uses default placement and prefix support
        public static void ConstructorWithSymbolUsesDefaults() {

            Unit unit = new Unit("V");

            Check.Equal("V", unit.Symbol);
            Check.Equal(UnitSymbolPlacement.AfterWithSpace, unit.Placement);
            Check.True(unit.AllowsPrefixConversion, "AllowsPrefixConversion should default to true");
        }

        // Constructor overrides placement
        public static void ConstructorWithPlacementOverridesDefault() {

            Unit unit = new Unit("$", UnitSymbolPlacement.BeforeNoSpace);

            Check.Equal(UnitSymbolPlacement.BeforeNoSpace, unit.Placement);
        }

        // Constructor can disable prefix conversion
        public static void ConstructorCanDisablePrefixConversion() {

            Unit unit = new Unit("°", UnitSymbolPlacement.AfterNoSpace, false);

            Check.False(unit.AllowsPrefixConversion, "AllowsPrefixConversion should be false");
        }

    }
}
