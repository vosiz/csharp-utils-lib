using System;
using Vosiz.Extends;

namespace Tests.Extends
{

    public static class DoubleExtTests
    {

        // Clamp keeps a value already inside the range unchanged
        public static void ClampKeepsValueInsideRange() {

            Check.Equal(5.0, 5.0.Clamp(0.0, 10.0));
        }

        // Clamp pulls a value below the minimum up to the minimum
        public static void ClampPullsValueUpToMin() {

            Check.Equal(0.0, (-5.0).Clamp(0.0, 10.0));
        }

        // Clamp pulls a value above the maximum down to the maximum
        public static void ClampPullsValueDownToMax() {

            Check.Equal(10.0, 15.0.Clamp(0.0, 10.0));
        }

        // Lerp moves the value towards the target by at most rate
        public static void LerpMovesTowardsTargetByRate() {

            Check.Equal(3.0, 0.0.Lerp(10.0, 3.0));
        }

        // Lerp does not overshoot the target when rate exceeds the remaining distance
        public static void LerpDoesNotOvershootTarget() {

            Check.Equal(10.0, 8.0.Lerp(10.0, 5.0));
        }

        // Lerp moves downwards towards a smaller target
        public static void LerpMovesDownwardsTowardsTarget() {

            Check.Equal(7.0, 10.0.Lerp(5.0, 3.0));
        }

    }
}
