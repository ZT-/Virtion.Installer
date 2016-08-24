using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Interop;


namespace Virtion.Installer.UI
{
    public partial class App : Application
    {
        public static string InstallPath;
        public static bool IsFinsh;
        public static int Value;

        public static int Start(string startAppPath)
        {
            Thread thread = new Thread(() =>
            {
                App app = new App();
                app.InitializeComponent();
                app.Run();

            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return 0;
        }

        public static int SetProgressValue(string value)
        {
            Int32.TryParse(value, out Value);
            return 0;
        }

        public static int InstallFinish(string path)
        {
            InstallPath = path;
            IsFinsh = true;
            return 0;
        }

    }
}
