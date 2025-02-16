using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Vosiz.Utils.Windows
{
    public class GlobalKeyHook : IDisposable
    {
        private const int WH_KEYBOARD_LL = 13;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr HookUd = IntPtr.Zero;
        private LowLevelKeyboardProc Process;
        private Action Callback;
        private Keys[] KeysToWatch;
        private Keys[] ModifiersToWatch;
        public bool Enabled = false;

        public GlobalKeyHook(Action callback, Keys[] keys, Keys[] modifiers = null)
        {
            Process = HookCallback;
            Callback = callback;
            KeysToWatch = keys;
            ModifiersToWatch = modifiers ?? new Keys[0];
            HookUd = SetHook(Process);
            Enabled = true;
        }

        public void Dispose()
        {
            UnhookWindowsHookEx(HookUd);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && Enabled)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;

                bool ctrlPressed = (Control.ModifierKeys & Keys.Control) != 0;
                bool modsPressed = ModifiersToWatch.All(mod => (Control.ModifierKeys & mod) != 0);
                bool keyPressed = KeysToWatch.Contains(key);

                if (modsPressed && keyPressed)
                {
                    Callback?.Invoke();
                    return (IntPtr)1;  // Optionally block the keypress
                }
            }
            return CallNextHookEx(HookUd, nCode, wParam, lParam);
        }
    }
}
