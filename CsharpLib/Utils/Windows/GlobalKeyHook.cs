using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Vosiz.Utils.Windows
{
    public class GlobalKeyHook : IDisposable
    {
        private static IntPtr HookId = IntPtr.Zero;
        private static LowLevelKeyboardProc HookProc;

        private const int WHKEYBOARDLL = 13;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        public bool Enabled { get; private set; } = false;

        private readonly Action Callback;
        private readonly Keys[] KeysToWatch;
        private readonly Keys[] ModifiersToWatch;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        public GlobalKeyHook(Action callback, Keys[] keys, Keys[] modifiers = null)
        {
            HookProc = HookCallback;  // Prevents GC from collecting the delegate
            Callback = callback;
            KeysToWatch = keys;
            ModifiersToWatch = modifiers ?? Array.Empty<Keys>();

            HookId = SetHook(HookProc);
            Enabled = HookId != IntPtr.Zero;
        }


        public void Dispose()
        {
            if (HookId != IntPtr.Zero)
            {
                UnhookWindowsHookEx(HookId);
                HookId = IntPtr.Zero;
                Enabled = false;
            }
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            IntPtr moduleHandle = GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName);

            if (moduleHandle == IntPtr.Zero)
            {
                MessageBox.Show("Error: Could not get module handle!", "Hook Error");
                return IntPtr.Zero;
            }

            IntPtr hookId = SetWindowsHookEx(WHKEYBOARDLL, proc, moduleHandle, 0);
            if (hookId == IntPtr.Zero)
            {
                MessageBox.Show("Error: Hook could not be set!", "Hook Error");
            }

            return hookId;
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && Enabled)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;
                bool allModifiersPressed = ModifiersToWatch.All(mod => (Control.ModifierKeys & mod) != 0);

                if (KeysToWatch.Contains(key) && allModifiersPressed)
                {
                    Callback?.Invoke();
                }
            }

            return CallNextHookEx(HookId, nCode, wParam, lParam);
        }


    }
}
