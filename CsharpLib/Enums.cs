using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vosiz
{
    public class Enums
    {
        public enum Severity
        {

            // 0x00 Init
            [Description("UNK")]
            Unknown = 0x00,

            // 0x1# Neutral
            [Description("DBG")]
            Debug = 0x10,
            [Description("VRB")]
            Verbose = 0x1A,
            [Description("TRC")]
            Trace = 0x1F,

            // 0x2# Mild
            [Description("NTC")]
            Notice = 0x20,
            [Description("INF")]
            Info = 0x21,

            // 0x5# Warning
            [Description("WRN")]
            Warning = 0x50,

            // 0x7# Error
            [Description("ERR")]
            Error = 0x70,
            [Description("FTL")]
            Fatal = 0x71,

            // 0xA#
            [Description("ANY")]
            Any = 0xA0,
            [Description("ALL")]
            All = 0xA1,

            // 0xF#
            [Description("FKP")]
            Fakup = 0xf0,
        }

        public enum PlatformOS
        {
            // PPPV VVVS
            // ======================
            // P: family  - platform family (Desktop/Mobile/Web/Console/...)
            // V: variant - specific OS within that family
            // S: specific - 1 = a finer detail (distro/edition) exists elsewhere
            // ----------------------
            [Description("unknown")]
            Unknown     = 0x00, // 0000 0000

            // Desktop
            [Description("windows")]
            Windows     = 0x20, // 0010 0000
            [Description("linux")]
            Linux       = 0x22, // 0010 0010
            [Description("macintosh")]
            MacOS       = 0x24, // 0010 0100

            // Mobile
            [Description("android")]
            Android     = 0x40, // 0100 0000
            [Description("ios")]
            iOS         = 0x42, // 0100 0010

            // Web
            [Description("web")]
            WebGL       = 0x60, // 0110 0000

            // Console
            [Description("xbox")]
            Xbox        = 0x80, // 1000 0000
            [Description("ps")]
            PlayStation = 0x82, // 1000 0010
        }
    }
}
