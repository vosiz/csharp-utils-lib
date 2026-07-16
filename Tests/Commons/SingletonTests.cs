using System;
using Vosiz.Commons;

namespace Tests.Commons
{

    public class SingletonTestSubject : Singleton<SingletonTestSubject>
    {
        public int Value = 42;

        // Constructor
        private SingletonTestSubject() { }
    }

    public static class SingletonTests
    {

        // Instance returns a non-null object
        public static void InstanceReturnsNonNullObject() {

            Check.True(SingletonTestSubject.Instance != null, "Instance should not be null");
        }

        // Instance always returns the same object
        public static void InstanceAlwaysReturnsSameObject() {

            var first = SingletonTestSubject.Instance;
            var second = SingletonTestSubject.Instance;

            Check.True(ReferenceEquals(first, second), "Instance should always be the same object");
        }

        // Instance exposes the state of the underlying object
        public static void InstanceExposesUnderlyingState() {

            Check.Equal(42, SingletonTestSubject.Instance.Value);
        }

    }
}
