using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vosiz.Commons
{
    public class Exceptionf : Exception
    {
        public Exceptionf(string fmt, params object[] pars) :
            base(string.Format(fmt, pars)) { }
    }

    internal class WrapperException : Exception {

        public WrapperException(string msg) : base("Library-Wrapper.exc: " + msg) { }

        public WrapperException(Exception exc) : this(exc.Message) { }


    }
}
