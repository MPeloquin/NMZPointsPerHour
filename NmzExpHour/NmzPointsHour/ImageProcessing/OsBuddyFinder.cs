using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NmzPointsHour.ImageProcessing
{
    public interface IOsBuddyFinder
    {
        OsBuddyFinder.Rect FindOsBuddy();
        bool OsBuddyOpened();
    }

    public class OsBuddyFinder : IOsBuddyFinder
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        public bool OsBuddyOpened()
        {
            var processes = Process.GetProcessesByName("OSBuddy");

            return processes.Length != 0;
        }

        public Rect FindOsBuddy()
        {
            var processes = Process.GetProcessesByName("OSBuddy");

            if(processes.Length == 0)
                return new Rect{Bottom = 0, Left = 0, Right = 0, Top =  0};

            IntPtr osBuddy = processes[0].MainWindowHandle;
            Rect osRect = new Rect();
            GetWindowRect(osBuddy, ref osRect);

            return osRect;
        }
    }
}