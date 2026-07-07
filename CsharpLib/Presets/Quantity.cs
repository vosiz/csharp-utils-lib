using System;
using System.Collections.Generic;
using System.Text;
using Commons = Vosiz.Commons;

namespace Vosiz.Presets
{

    public static class Quantity
    {

        public static readonly Dictionary<string, Commons.Unit> All = new Dictionary<string, Commons.Unit> {

            { "Temperature", Unit.Kelvin },
            { "Current",     Unit.Ampere },
            { "Voltage",     Unit.Volt },
            { "Length",      Unit.Meter }
        };


        // Creates a preset Quantity by name (Temperature, Current, Voltage, Length) with the given value
        public static Commons.Quantity Create(string name, double value) {

            if (!All.TryGetValue(name, out Commons.Unit unit))
                throw new NotSupportedException(string.Format("No preset quantity named {0}.", name));

            return new Commons.Quantity(name, unit, value);
        }

        // Creates a Temperature quantity in Kelvin
        public static Commons.Quantity Temperature(double value) {

            return Create("Temperature", value);
        }

        // Creates a Current quantity in Ampere
        public static Commons.Quantity Current(double value) {

            return Create("Current", value);
        }

        // Creates a Voltage quantity in Volt
        public static Commons.Quantity Voltage(double value) {

            return Create("Voltage", value);
        }

        // Creates a Length quantity in Meter
        public static Commons.Quantity Length(double value) {

            return Create("Length", value);
        }

    }
}
