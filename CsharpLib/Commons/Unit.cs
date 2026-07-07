using System;
using System.Collections.Generic;
using System.Text;

namespace Vosiz.Commons
{

    public enum UnitSymbolPlacement
    {
        // 0000 00BS
        // ======================
        // B: before - 1 = is before unit
        // S: space - 1 = has space
        // ----------------------
        BeforeWithSpace     = 0x03, // 0011
        BeforeNoSpace       = 0x02, // 0010
        AfterWithSpace      = 0x01, // 0001
        AfterNoSpace        = 0x00, // 0000
    }

    public class Unit {

        public string Symbol { private set; get; }
        public UnitSymbolPlacement Placement { private set; get; }
        public bool AllowsPrefixConversion { private set; get; }


        // Constructor
        public Unit() {

            Symbol = string.Empty;
            Placement = UnitSymbolPlacement.AfterWithSpace;
            AllowsPrefixConversion = true;
        }

        // Constructor with symbol, placement and prefix support
        public Unit(string symbol, UnitSymbolPlacement placement = UnitSymbolPlacement.AfterWithSpace, bool allows_prefix_conversion = true) {

            Symbol = symbol;
            Placement = placement;
            AllowsPrefixConversion = allows_prefix_conversion;
        }

    }
}
