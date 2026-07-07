using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Vosiz.Commons
{

    public class Quantity
    {

        private static readonly Dictionary<int, SiPrefix> SiPrefixes = new Dictionary<int, SiPrefix> {

            { -12, new SiPrefix(-12, "p", "pico") },
            { -9,  new SiPrefix(-9,  "n", "nano") },
            { -6,  new SiPrefix(-6,  "µ", "micro") },
            { -3,  new SiPrefix(-3,  "m", "milli") },
            { 0,   new SiPrefix(0,   "",  "") },
            { 3,   new SiPrefix(3,   "k", "kilo") },
            { 6,   new SiPrefix(6,   "M", "mega") },
            { 9,   new SiPrefix(9,   "G", "giga") },
            { 12,  new SiPrefix(12,  "T", "tera") }
        };

        public string Name { private set; get; }
        public Unit Unit { private set; get; }
        public double Value { private set; get; }


        // Picks the largest supported engineering exponent (steps of 1000) for the given value
        private static int GetPrefixExponent(double abs_value) {

            if (abs_value == 0.0)
                return 0;

            int exponent = (int)Math.Floor(Math.Log10(abs_value) / 3.0) * 3;

            int min_exponent = SiPrefixes.Keys.Min();
            int max_exponent = SiPrefixes.Keys.Max();

            if (exponent < min_exponent)
                exponent = min_exponent;

            if (exponent > max_exponent)
                exponent = max_exponent;

            return exponent;
        }

        // Looks up the SI prefix symbol for a given exponent
        private static string LookupPrefixSymbol(int exponent) {

            if (SiPrefixes.TryGetValue(exponent, out SiPrefix prefix))
                return prefix.Symbol;

            return string.Empty;
        }


        // Constructor
        public Quantity() {

            Name = string.Empty;
            Unit = new Unit();
            Value = 0.0;
        }

        // Constructor with name, unit and value
        public Quantity(string name, Unit unit, double value) {

            Name = name;
            Unit = unit;
            Value = value;
        }

        // Default formatting - no decimals, with SI prefix conversion
        public override string ToString() {

            return ToString(0, true);
        }

        // Formats value with given decimal places and optional SI prefix conversion
        public string ToString(int decimals, bool use_prefix_conversion = true) {

            double value = Value;
            string prefix = string.Empty;

            if (use_prefix_conversion && Unit.AllowsPrefixConversion) {

                int exponent = GetPrefixExponent(Math.Abs(value));
                double scale = Math.Pow(10, exponent);

                value = value / scale;
                prefix = LookupPrefixSymbol(exponent);
            }

            string number = value.ToString("F" + decimals, CultureInfo.CurrentCulture);
            string symbol = prefix + Unit.Symbol;

            switch (Unit.Placement) {

                case UnitSymbolPlacement.BeforeWithSpace:
                    return symbol + " " + number;

                case UnitSymbolPlacement.BeforeNoSpace:
                    return symbol + number;

                case UnitSymbolPlacement.AfterNoSpace:
                    return number + symbol;

                case UnitSymbolPlacement.AfterWithSpace:
                    return number + " " + symbol;

                default:
                    throw new NotSupportedException(
                        string.Format("Unsupported unit placement {0}.", Unit.Placement));
            }
        }

    }
}
