using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vosiz.Commons
{

    public enum RetvalType { 
    
        // No-problemo !
        NoProblem = 0x00,

        // Success
        Success = 0x10,

        // Fail
        Fail = 0x20,

        // Types - informative
        Tip = 0x70,
        Info = 0x71,
        Notice = 0x72,

        // Types - warnings
        Warning = 0xA0,
        Fakup = 0xA1,

        // Types - errors
        Error = 0xE0,

        // Types - fatal
        Fatal = 0xF0,
        Exception = 0xF1

    }

    public class Retval
    {

        public static Retval Np => Create(RetvalType.NoProblem, string.Empty);

        RetvalType Type;
        string Message;


        protected Retval(RetvalType type, string msg) {

            Type = type;
            Message = msg;
        }

        public static Retval Create(RetvalType type, string msg) {

            return new Retval(type, msg);
        }

    }
}
