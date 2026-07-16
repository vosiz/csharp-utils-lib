using System;
using System.Threading;
using System.Threading.Tasks;
using Vosiz.Utils;

namespace Tests.Utils
{

    public static class AsyncTests
    {

        // Run executes an action and completes
        public static void RunExecutesAction() {

            bool executed = false;

            Async.Run(() => executed = true).Wait();

            Check.True(executed, "Action should have executed");
        }

        // Run with a function returns its result
        public static void RunWithFunctionReturnsResult() {

            int result = Async.Run(() => 21 * 2).Result;

            Check.Equal(42, result);
        }

        // RunLocked serializes access to a shared resource under the same lock key
        public static void RunLockedSerializesAccessToSharedResource() {

            object lock_key = new object();
            int counter = 0;
            int max_concurrent = 0;

            Task[] tasks = new Task[10];

            for (int i = 0; i < tasks.Length; i++) {

                tasks[i] = Async.RunLocked(() => {

                    int current = Interlocked.Increment(ref counter);
                    max_concurrent = Math.Max(max_concurrent, current);

                    Thread.Sleep(10);

                    Interlocked.Decrement(ref counter);

                }, lock_key);
            }

            Task.WaitAll(tasks);

            Check.Equal(1, max_concurrent);
        }

        // Repeat invokes the action multiple times until cancelled
        public static void RepeatInvokesActionUntilCancelled() {

            int invocation_count = 0;

            var cts = Async.Repeat(() => Interlocked.Increment(ref invocation_count), interval: 10);

            Thread.Sleep(50);
            cts.Cancel();

            int count_after_cancel = invocation_count;

            Thread.Sleep(100);

            Check.True(count_after_cancel >= 2, "Action should have run multiple times before cancellation");
            Check.Equal(count_after_cancel, invocation_count);
        }

    }
}
