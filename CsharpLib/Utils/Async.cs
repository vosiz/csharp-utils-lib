using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

#if USE_WPF
using System.Windows;
#endif

namespace Vosiz.Utils
{
    public static class Async
    {
        private class AsyncScopedLock
        {
            private static SemaphoreSlim[] DefaultLock = { new SemaphoreSlim(1, 1) };
            private static readonly ConcurrentDictionary<object, SemaphoreSlim> LockMap = new ConcurrentDictionary<object, SemaphoreSlim>();

            private readonly List<SemaphoreSlim> Locks;

            // Constructor with locks
            public AsyncScopedLock(params object[] locks)
            {

                SemaphoreSlim[] semaphores = locks.Length > 0 ?
                    locks.Select(l => LockMap.GetOrAdd(l, _ => new SemaphoreSlim(1, 1))).ToArray() :
                    DefaultLock;

                Array.Sort(semaphores, (a, b) => RuntimeHelpers.GetHashCode(a).CompareTo(RuntimeHelpers.GetHashCode(b)));
                Locks = new List<SemaphoreSlim>(semaphores);
            }

            // Acquires all locks in a fixed order to avoid deadlocks
            async public Task LockAsync()
            {

                foreach (var semaphore in Locks)
                    await semaphore.WaitAsync();
            }

            // Releases all locks in reverse order
            public void Unlock()
            {

                for (int i = Locks.Count - 1; i >= 0; i--)
                    Locks[i].Release();
            }
        }

        // Runs an action on the thread pool
        async public static Task Run(Action action)
        {
            await Task.Run(action);
        }

        // Awaits an already-async function
        async public static Task Run(Func<Task> func)
        {
            await func();
        }

        // Runs a function on the thread pool and returns its result
        async public static Task<T> Run<T>(Func<T> func)
        {
            return await Task.Run(func);
        }

        // Awaits an already-async function and returns its result
        async public static Task<T> Run<T>(Func<Task<T>> func)
        {
            return await func();
        }

        // Runs an action on the thread pool while holding the given locks
        async public static Task RunLocked(Action action, params object[] locks)
        {

            var scoped_lock = new AsyncScopedLock(locks);
            await scoped_lock.LockAsync();

            try
            {
                await Task.Run(action);
            }
            finally
            {
                scoped_lock.Unlock();
            }
        }

        // Runs a function on the thread pool while holding the given locks
        async public static Task<T> RunLocked<T>(Func<T> func, params object[] locks)
        {

            var scoped_lock = new AsyncScopedLock(locks);
            await scoped_lock.LockAsync();

            try
            {
                return await Task.Run(func);
            }
            finally
            {
                scoped_lock.Unlock();
            }
        }

        // Awaits an already-async action while holding the given locks
        async public static Task RunLocked(Func<Task> func, params object[] locks)
        {

            var scoped_lock = new AsyncScopedLock(locks);
            await scoped_lock.LockAsync();

            try
            {
                await func();
            }
            finally
            {
                scoped_lock.Unlock();
            }
        }

        // Awaits an already-async function while holding the given locks
        async public static Task<T> RunLocked<T>(Func<Task<T>> func, params object[] locks)
        {

            var scoped_lock = new AsyncScopedLock(locks);
            await scoped_lock.LockAsync();

            try
            {
                return await func();
            }
            finally
            {
                scoped_lock.Unlock();
            }
        }

#if USE_WPF
        // Runs an action on the WPF UI thread
        async public static Task RunOnUI(Action action)
        {

            if (Application.Current == null)
                throw new InvalidOperationException("No WPF Application context available.");

            await Application.Current.Dispatcher.InvokeAsync(action);
        }

        // Runs a function on the WPF UI thread and returns its result
        async public static Task<T> RunOnUI<T>(Func<T> func)
        {

            if (Application.Current == null)
                throw new InvalidOperationException("No WPF Application context available.");

            return await Application.Current.Dispatcher.InvokeAsync(func);
        }
#endif

        // Repeatedly runs an action on a background loop until cancelled
        public static CancellationTokenSource Repeat(Action action, int interval = 1000)
        {

            var cts = new CancellationTokenSource();
            var token = cts.Token;

            _ = Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    action();

                    if (interval > 0 && !token.IsCancellationRequested)
                        Thread.Sleep(interval);
                }
            });

            return cts;
        }

        // Repeatedly awaits an async action on a background loop until cancelled
        public static CancellationTokenSource Repeat(Func<Task> action, int interval = 1000)
        {

            var cts = new CancellationTokenSource();
            var token = cts.Token;

            _ = Run(() =>
            {
                _ = Run(async () =>
                {
                    while (!token.IsCancellationRequested)
                    {
                        await action();

                        if (interval > 0 && !token.IsCancellationRequested)
                            await Task.Delay(interval, token);
                    }
                });
            });

            return cts;
        }
    }
}
