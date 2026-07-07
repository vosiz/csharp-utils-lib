using System;
using System.Globalization;
using Vosiz.Commons;

namespace Tests.Commons
{

    public static class QuantityTests
    {

        // Default constructor sets sane defaults
        public static void DefaultConstructorSetsDefaults() {

            Quantity quantity = new Quantity();

            Check.Equal(string.Empty, quantity.Name);
            Check.Equal(0.0, quantity.Value);
        }

        // Default ToString applies the kilo prefix
        public static void ToStringDefaultAppliesKiloPrefix() {

            Quantity voltage = new Quantity("voltage", new Unit("V"), 1120.302);

            Check.Equal("1 kV", voltage.ToString());
        }

        // ToString with decimals and no prefix conversion keeps the base unit
        public static void ToStringWithDecimalsAndNoPrefix() {

            Quantity voltage = new Quantity("voltage", new Unit("V"), 1120.302);
            string expected = string.Format("{0} V", (1120.302).ToString("F2", CultureInfo.CurrentCulture));

            Check.Equal(expected, voltage.ToString(2, false));
        }

        // ToString respects Before placement
        public static void ToStringRespectsBeforePlacement() {

            Quantity price = new Quantity("price", new Unit("$", UnitSymbolPlacement.BeforeNoSpace, false), 42.0);

            Check.Equal("$42", price.ToString());
        }

        // ToString respects AfterNoSpace placement
        public static void ToStringRespectsAfterNoSpacePlacement() {

            Quantity angle = new Quantity("angle", new Unit("°", UnitSymbolPlacement.AfterNoSpace, false), 90.0);

            Check.Equal("90°", angle.ToString());
        }

        // ToString ignores prefix conversion when the unit disallows it
        public static void ToStringIgnoresPrefixWhenUnitDisallows() {

            Quantity angle = new Quantity("angle", new Unit("°", UnitSymbolPlacement.AfterNoSpace, false), 9000.0);

            Check.Equal("9000°", angle.ToString(0, true));
        }

        // ToString handles zero without throwing
        public static void ToStringHandlesZeroValue() {

            Quantity voltage = new Quantity("voltage", new Unit("V"), 0.0);

            Check.Equal("0 V", voltage.ToString());
        }

        // ToString keeps the sign for negative values with prefix conversion
        public static void ToStringHandlesNegativeValueWithPrefix() {

            Quantity voltage = new Quantity("voltage", new Unit("V"), -1120.302);

            Check.Equal("-1 kV", voltage.ToString());
        }

    }
}
