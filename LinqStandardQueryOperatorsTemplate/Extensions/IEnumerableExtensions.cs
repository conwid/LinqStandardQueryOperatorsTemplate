using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace $safeprojectname$.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void Dump<T>(this IEnumerable<T> source)
        {
            Console.Clear();
            ShowWindow(ThisConsole, MAXIMIZE);
            Console.WriteLine(source.ToStringTable());
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }

        // https://stackoverflow.com/questions/22053112/maximizing-console-window-c-sharp
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;
    }
}
