using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vosiz.Commons
{

    public class AssertException : Exceptionf
    {

        public AssertException() :
            base("Assertion failed.")
        { }

        public AssertException(string message) :
            base($"Assertion failed: {message}")
        { }

        public AssertException(string message, Exception inner_exc) :
            base($"Assertion failed: {message}", inner_exc)
        { }

        public AssertException(string format, params string[] pars) :
            base($"Assertion failed: {format}", pars)
        { }

    }

    public static class Assert
    {
        public static void OnNull(object obj)
        {

            if (obj == null)
                throw new AssertException("Object is null");
        }

        public static void OnType(object obj, Type type)
        {

            if (obj.GetType() != type)
                throw new AssertException("Object is not typeof {0}, found {1}", type.ToString(), obj.GetType().ToString());
        }
    }
}
