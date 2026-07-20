using System;
using VPresets = Vosiz.Presets;
using VCommons = Vosiz.Commons;

namespace Tests.Presets
{

    public static class QuantityTests
    {

        // All dictionary maps every basic quantity to its unit
        public static void AllMapsQuantitiesToUnits() {

            Check.True(ReferenceEquals(VPresets.Quantity.All["Temperature"], VPresets.Unit.Kelvin), "Temperature should use Kelvin");
            Check.True(ReferenceEquals(VPresets.Quantity.All["Current"], VPresets.Unit.Ampere), "Current should use Ampere");
            Check.True(ReferenceEquals(VPresets.Quantity.All["Voltage"], VPresets.Unit.Volt), "Voltage should use Volt");
            Check.True(ReferenceEquals(VPresets.Quantity.All["Length"], VPresets.Unit.Meter), "Length should use Meter");
            Check.True(ReferenceEquals(VPresets.Quantity.All["Time"], VPresets.Unit.Second), "Time should use Second");
        }

        // Create builds a Quantity with the preset name, unit and given value
        public static void CreateBuildsQuantityFromPresetName() {

            VCommons.Quantity quantity = VPresets.Quantity.Create("Voltage", 12.0);

            Check.Equal("Voltage", quantity.Name);
            Check.True(ReferenceEquals(VPresets.Unit.Volt, quantity.Unit), "Should use the Volt preset unit");
            Check.Equal(12.0, quantity.Value);
        }

        // Create throws for an unknown preset name
        public static void CreateThrowsForUnknownName() {

            Check.Throws<NotSupportedException>(() => VPresets.Quantity.Create("Pressure", 1.0));
        }

        // Temperature shortcut uses Kelvin
        public static void TemperatureUsesKelvin() {

            VCommons.Quantity quantity = VPresets.Quantity.Temperature(293.15);

            Check.Equal("Temperature", quantity.Name);
            Check.True(ReferenceEquals(VPresets.Unit.Kelvin, quantity.Unit), "Should use Kelvin");
            Check.Equal(293.15, quantity.Value);
        }

        // Current shortcut uses Ampere
        public static void CurrentUsesAmpere() {

            VCommons.Quantity quantity = VPresets.Quantity.Current(2.5);

            Check.Equal("Current", quantity.Name);
            Check.True(ReferenceEquals(VPresets.Unit.Ampere, quantity.Unit), "Should use Ampere");
            Check.Equal(2.5, quantity.Value);
        }

        // Voltage shortcut uses Volt
        public static void VoltageUsesVolt() {

            VCommons.Quantity quantity = VPresets.Quantity.Voltage(1120.302);

            Check.Equal("Voltage", quantity.Name);
            Check.True(ReferenceEquals(VPresets.Unit.Volt, quantity.Unit), "Should use Volt");
            Check.Equal(1120.302, quantity.Value);
        }

        // Length shortcut uses Meter
        public static void LengthUsesMeter() {

            VCommons.Quantity quantity = VPresets.Quantity.Length(3.0);

            Check.Equal("Length", quantity.Name);
            Check.True(ReferenceEquals(VPresets.Unit.Meter, quantity.Unit), "Should use Meter");
            Check.Equal(3.0, quantity.Value);
        }

        // Time shortcut uses Second
        public static void TimeUsesSecond() {

            VCommons.Quantity quantity = VPresets.Quantity.Time(1.322);

            Check.Equal("Time", quantity.Name);
            Check.True(ReferenceEquals(VPresets.Unit.Second, quantity.Unit), "Should use Second");
            Check.Equal(1.322, quantity.Value);
        }

        // Time formats sub-second values with an SI prefix and no space, e.g. milliseconds
        public static void TimeFormatsSubSecondWithPrefix() {

            VCommons.Quantity quantity = VPresets.Quantity.Time(0.02);

            Check.Equal("20ms", quantity.ToString(0));
        }

    }
}
