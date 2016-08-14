using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Virtion.Util;

namespace Uninstall
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public List<String> ReadFile(String path)
        {
            if (File.Exists(path) == false)
            {
                MessageBox.Show("找不到卸载信息或损坏： " + path);

                App.Current.Shutdown();
                return null;
            }
            StreamReader sr = new StreamReader(path, Encoding.Default);
            List<String> list = new List<string>();
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                list.Add(line);
            }
            sr.Close();
            return list;
        }


        private void DeleteItselfByCMD()
        {
            string s = Process.GetCurrentProcess().MainModule.FileName;
            String appStartupPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/C ping 1.1.1.1 -n 1 -w 1000 > Nul & rd /s /q " + appStartupPath);
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            Process.Start(psi);
            App.Current.Shutdown();
        }

        private void DeleteFolderFiles(string path)
        {
            try
            {
                string[] files = Directory.GetFiles(path);
                foreach (var file in files)
                {

                    if (Directory.Exists(file) == true)
                    {
                        Directory.Delete(file, true);
                    }
                    else
                    {
                        File.Delete(file);
                    }
                }
                Directory.Delete(path, true);
            }
            catch (Exception)
            {
            }
        }

        private bool CheckDisk(string s)
        {
            if (s.EndsWith(":\\"))
            {
                return true;
            }
            return false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();

            var list = ReadFile(System.AppDomain.CurrentDomain.BaseDirectory + "\\uninstall.dat");
            if (list == null)
            {
                return;
            }
            foreach (var item in list)
            {
                string[] arr = item.Split('>');
                switch (arr[0])
                {
                    case "FILE":
                        if (File.Exists(arr[1]) == true)
                        {
                            File.Delete(arr[1]);
                        }
                        break;
                    case "FOLDER":
                        if (Directory.Exists(arr[1]) == true)
                        {
                            if (CheckDisk(arr[1]) == true)
                            {
                                MessageBox.Show("监测到安装在磁盘根目录，继续卸载会有清空磁盘的危险！卸载终止！");
                                return;
                            }
                            this.DeleteFolderFiles(arr[1]);
                        }
                        break;
                    case "REG":
                        register.SubKey = arr[1];
                        register.DeleteSubKey();
                        break;
                    default:
                        break;
                }
            }
            this.DeleteItselfByCMD();
        }

    }
}
