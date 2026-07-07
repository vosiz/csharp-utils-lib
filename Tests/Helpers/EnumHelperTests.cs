using System;
using System.Collections.Generic;
using Vosiz.Helpers;
using Tests.Extends;

namespace Tests.Helpers
{

    public static class EnumHelperTests
    {

        // GetAll maps every enum value to its numeric value and name
        public static void GetAllMapsValuesAndNames() {

            Dictionary<int, string> all = EnumHelper.GetAll<SampleDescribedEnum>();

            Check.Equal(2, all.Count);
            Check.Equal("First", all[(int)SampleDescribedEnum.First]);
            Check.Equal("Second", all[(int)SampleDescribedEnum.Second]);
        }

        // Random never returns an ignored value
        public static void RandomNeverReturnsIgnoredValue() {

            for (int i = 0; i < 20; i++) {

                SampleDescribedEnum value = EnumHelper.Random(SampleDescribedEnum.First);

                Check.Equal(SampleDescribedEnum.Second, value);
            }
        }

        // GetDescriptions returns the Description attribute when present
        public static void GetDescriptionsReturnsAttributeValue() {

            Check.Equal("First Value", EnumHelper.GetDescriptions(SampleDescribedEnum.First));
        }

        // GetDescriptions falls back to ToString when no attribute is present
        public static void GetDescriptionsFallsBackToToString() {

            Check.Equal("Second", EnumHelper.GetDescriptions(SampleDescribedEnum.Second));
        }

    }
}
