using System;
using Vosiz.Commons;

namespace Tests.Commons
{

    public static class ExceptionsTests
    {

        // Exceptionf formats the message with the given args
        public static void ExceptionfFormatsMessage() {

            Exceptionf exc = new Exceptionf("value is {0}", 42);

            Check.Equal("value is 42", exc.Message);
        }

        // Exceptionf copies the message of the wrapped exception
        public static void ExceptionfWrapsInnerExceptionMessage() {

            InvalidOperationException inner = new InvalidOperationException("bad state");
            Exceptionf exc = new Exceptionf(inner);

            Check.Equal("bad state", exc.Message);
        }

        // FormattedException formats the message with the given args
        public static void FormattedExceptionFormatsMessage() {

            FormattedException exc = new FormattedException("value is {0}", 42);

            Check.Equal("value is 42", exc.Message);
        }

        // NotException sets the message
        public static void NotExceptionSetsMessage() {

            NotException exc = new NotException("not allowed");

            Check.Equal("not allowed", exc.Message);
        }

        // UnimplementedStateException formats the state into the message
        public static void UnimplementedStateExceptionFormatsMessage() {

            UnimplementedStateException exc = new UnimplementedStateException("Idle");

            Check.Equal("Unimplemented state Idle", exc.Message);
        }

        // MessageException combines the message with the caught exception's message
        public static void MessageExceptionCombinesMessages() {

            InvalidOperationException inner = new InvalidOperationException("bad state");
            MessageException exc = new MessageException("Operation failed", inner);

            Check.Equal("Operation failed: bad state", exc.Message);
        }

    }
}
