using System;
using System.Collections.Generic;
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
            Unknown = 0x00,

            // 0x1# Neutral
            Debug = 0x10,

            // 0x2# Mild
            Notice = 0x20,
            Info = 0x21,

            // 0x5# Warning
            Warning = 0x50,

            // 0x7# Error
            Error = 0x70,
            Fatal = 0x71,

            // 0xF# 
            Fakup = 0xf0,
        }
    }
}
