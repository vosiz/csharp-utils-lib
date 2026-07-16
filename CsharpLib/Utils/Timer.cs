using System;

namespace Vosiz.Utils
{
    public class Timer
    {
        private readonly int EveryMs;
        private readonly Action Callback;

        private double Remaining;

        // Constructor
        public Timer() { }

        // Constructor with every_ms and callback
        public Timer(int every_ms, Action callback)
        {
            EveryMs = every_ms;
            Callback = callback;

            Remaining = every_ms;
        }

        // Advances the countdown by delta_s seconds and triggers once it has elapsed
        public void Update(float delta_s)
        {

            Remaining -= delta_s * 1000.0f;

            if (Remaining <= 0)
                Trigger();
        }

        // Resets the countdown back to the full interval
        public void ResetTimer()
        {

            Remaining = EveryMs;
        }

        // Invokes the callback and optionally resets the countdown
        public void Trigger(bool reset = true)
        {

            Callback.Invoke();

            if (reset)
                ResetTimer();
        }
    }
}
