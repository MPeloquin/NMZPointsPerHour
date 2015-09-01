using System;
using System.Windows.Forms;
using NmzPointsHour.NightmareZone;
using NmzPointsHour.UI;

namespace NmzPointsHour
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Ui(new NMZSession()));
        }
    }
}
