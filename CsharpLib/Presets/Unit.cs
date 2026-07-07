using System;
using System.Collections.Generic;
using System.Text;
using Commons = Vosiz.Commons;

namespace Vosiz.Presets
{

    public static class Unit
    {

        public static readonly Dictionary<string, Commons.Unit> All = new Dictionary<string, Commons.Unit> {

            { "Kelvin", new Commons.Unit("K", Commons.UnitSymbolPlacement.AfterWithSpace, true) },
            { "Ampere", new Commons.Unit("A", Commons.UnitSymbolPlacement.AfterWithSpace, true) },
            { "Volt",   new Commons.Unit("V", Commons.UnitSymbolPlacement.AfterWithSpace, true) },
            { "Meter",  new Commons.Unit("m", Commons.UnitSymbolPlacement.AfterWithSpace, true) }
        };

        public static Commons.Unit Kelvin => All["Kelvin"];
        public static Commons.Unit Ampere => All["Ampere"];
        public static Commons.Unit Volt => All["Volt"];
        public static Commons.Unit Meter => All["Meter"];

    }
}
