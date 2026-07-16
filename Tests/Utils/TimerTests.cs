using System;
using Vosiz.Utils;

namespace Tests.Utils
{

    public static class TimerTests
    {

        // Update does not trigger the callback before the interval has elapsed
        public static void UpdateDoesNotTriggerBeforeInterval() {

            int trigger_count = 0;
            Timer timer = new Timer(1000, () => trigger_count++);

            timer.Update(0.5f);

            Check.Equal(0, trigger_count);
        }

        // Update triggers the callback once the interval has elapsed
        public static void UpdateTriggersOnceIntervalElapses() {

            int trigger_count = 0;
            Timer timer = new Timer(1000, () => trigger_count++);

            timer.Update(0.5f);
            timer.Update(0.6f);

            Check.Equal(1, trigger_count);
        }

        // Update retriggers after the countdown resets
        public static void UpdateRetriggersAfterReset() {

            int trigger_count = 0;
            Timer timer = new Timer(1000, () => trigger_count++);

            timer.Update(1.0f);
            timer.Update(1.0f);

            Check.Equal(2, trigger_count);
        }

        // Trigger with reset:false invokes the callback without resetting the countdown
        public static void TriggerWithoutResetKeepsCountdownProgress() {

            int trigger_count = 0;
            Timer timer = new Timer(1000, () => trigger_count++);

            timer.Update(0.5f);
            timer.Trigger(reset: false);

            Check.Equal(1, trigger_count);

            timer.Update(0.6f);

            Check.Equal(2, trigger_count);
        }

    }
}
