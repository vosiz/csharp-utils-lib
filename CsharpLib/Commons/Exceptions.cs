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

        public Exceptionf(Exception exc) :
            base(exc.Message) { }
    }

    public class FormattedException : Exceptionf 
    {
        public FormattedException(string fmt, params object[] pars) :
            base(string.Format(fmt, pars)) { }
    }

    public class NotException : Exception
    {
        public NotException(string message) : base(message) { }
    }

    public class UnimplementedStateException : FormattedException
    {
        public UnimplementedStateException(object state)
            : base("Unimplemented state {0}", state.ToString()) { }
    }
}
