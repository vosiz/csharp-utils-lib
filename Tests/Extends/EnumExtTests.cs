using System;
using System.ComponentModel;
using System.Linq;
using Vosiz.Commons;
using Vosiz.Extends;

namespace Tests.Extends
{

    public enum SampleDescribedEnum
    {
        [Description("First Value")]
        First,

        Second
    }

    public static class EnumExtTests
    {

        // GetDescription returns the Description attribute when present
        public static void GetDescriptionReturnsAttributeValue() {

            Check.Equal("First Value", SampleDescribedEnum.First.GetDescription());
        }

        // GetDescription falls back to ToString when no attribute is present
        public static void GetDescriptionFallsBackToToString() {

            Check.Equal("Second", SampleDescribedEnum.Second.GetDescription());
        }

        // GetDescription throws for a null enum value
        public static void GetDescriptionThrowsForNull() {

            Enum value = null;

            Check.Throws<AssertException>(() => value.GetDescription());
        }

        // GetAll returns every declared enum value
        public static void GetAllReturnsEveryValue() {

            var values = SampleDescribedEnum.First.GetAll().ToList();

            Check.Equal(2, values.Count);
            Check.True(values.Contains(SampleDescribedEnum.First), "Missing First");
            Check.True(values.Contains(SampleDescribedEnum.Second), "Missing Second");
        }

    }
}
