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
    }
}
