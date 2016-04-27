using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Innouvous.Utils.SingleInstance
{
    public class Checker
    {
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr handle, int nCmdShow);
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool IsIconic(IntPtr handle);

        private const int SW_RESTORE = 9;

        public static bool AlreadyRunning
        {
            get
            {
                var process = PriorProcess();
                if (process != null)
                {
                    IntPtr handle = process.MainWindowHandle;

                    if (IsIconic(handle))
                    {
                        ShowWindow(handle, SW_RESTORE);
                    }

                    SetForegroundWindow(handle);

                    return true;
                }
                else
                    return false;
            }
        }

        /*
        //Method 2
        public static bool IsSingleInstance()
        {
            bool createdNew;
            var singleInstanceWatcher = new Semaphore(0, 1,
                Assembly.GetExecutingAssembly().GetName().Name, out createdNew);

            return createdNew;
        }
        */

        public static Process PriorProcess()
        // Returns a System.Diagnostics.Process pointing to
        // a pre-existing process with the same name as the
        // current one, if any; or null if the current process
        // is unique.
        {
            Process curr = Process.GetCurrentProcess();
            Process[] procs = Process.GetProcessesByName(curr.ProcessName);
            foreach (Process p in procs)
            {
                if ((p.Id != curr.Id) &&
                    (p.MainModule.FileName == curr.MainModule.FileName))
                    return p;
            }

            return null;
        }
    }
}
