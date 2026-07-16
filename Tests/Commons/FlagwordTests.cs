using System;
using Vosiz.Commons;

namespace Tests.Commons
{

    public enum FlagwordTestFlags
    {
        Alpha,
        Beta,
        Gamma
    }

    public static class FlagwordTests
    {

        // Get returns false for a flag that was never set
        public static void GetReturnsFalseForUnsetFlag() {

            Flagword flags = new Flagword();

            Check.False(flags.Get(FlagwordTestFlags.Alpha), "Unset flag should report false");
        }

        // Set marks a flag as set
        public static void SetMarksFlagAsSet() {

            Flagword flags = new Flagword();
            flags.Set(FlagwordTestFlags.Alpha);

            Check.True(flags.IsSet(FlagwordTestFlags.Alpha), "Flag should be set");
        }

        // Unset clears a previously set flag
        public static void UnsetClearsFlag() {

            Flagword flags = new Flagword();
            flags.Set(FlagwordTestFlags.Alpha);
            flags.Unset(FlagwordTestFlags.Alpha);

            Check.False(flags.IsSet(FlagwordTestFlags.Alpha), "Flag should be cleared");
        }

        // Toggle flips an unset flag to set and returns the new state
        public static void ToggleFlipsUnsetFlagToSet() {

            Flagword flags = new Flagword();

            bool new_state = flags.Toggle(FlagwordTestFlags.Alpha);

            Check.True(new_state, "Toggle should report the new state as set");
            Check.True(flags.IsSet(FlagwordTestFlags.Alpha), "Flag should be set");
        }

        // AnySet reports false when no flag is set
        public static void AnySetReturnsFalseWhenNothingSet() {

            Flagword flags = new Flagword();

            Check.False(flags.AnySet(), "No flags should be set");
        }

        // AnySet reports true once a flag is set
        public static void AnySetReturnsTrueWhenAFlagIsSet() {

            Flagword flags = new Flagword();
            flags.Set(FlagwordTestFlags.Beta);

            Check.True(flags.AnySet(), "A flag should be set");
        }

        // Display returns an empty string when no flag is set
        public static void DisplayReturnsEmptyWhenNothingSet() {

            Flagword flags = new Flagword();

            Check.Equal(string.Empty, flags.Display());
        }

        // Display lists the descriptions of the set flags in brackets
        public static void DisplayListsSetFlagDescriptions() {

            Flagword flags = new Flagword();
            flags.Set(FlagwordTestFlags.Gamma);

            Check.Equal("[Gamma]", flags.Display());
        }

    }
}
