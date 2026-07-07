using System;
using Vosiz.Commons;

namespace Tests.Commons
{

    public static class LimitedTests
    {

        // Constructor sets Id, Min and Max
        public static void ConstructorSetsProperties() {

            Limited<int> limited = new Limited<int>("range", 1, 10);

            Check.Equal("range", limited.Id);
            Check.Equal(1, limited.Min);
            Check.Equal(10, limited.Max);
        }

    }
}
