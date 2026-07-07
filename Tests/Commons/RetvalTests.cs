using System;
using Vosiz.Commons;

namespace Tests.Commons
{

    public static class RetvalTests
    {

        // Create sets Type and Message
        public static void CreateSetsTypeAndMessage() {

            Retval retval = Retval.Create(RetvalType.Error, "boom");

            Check.Equal(RetvalType.Error, retval.Type);
            Check.Equal("boom", retval.Message);
        }

        // Np returns NoProblem with an empty message
        public static void NpReturnsNoProblemWithEmptyMessage() {

            Retval retval = Retval.Np;

            Check.Equal(RetvalType.NoProblem, retval.Type);
            Check.Equal(string.Empty, retval.Message);
        }

        // ToString formats type and message
        public static void ToStringFormatsTypeAndMessage() {

            Retval retval = Retval.Create(RetvalType.Warning, "careful");

            Check.Equal("[Warning]: careful", retval.ToString());
        }

    }
}
