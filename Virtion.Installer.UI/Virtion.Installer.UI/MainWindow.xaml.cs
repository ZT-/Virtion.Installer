using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Virtion.Installer.UI
{
    public partial class MainWindow : Window
    {
        public const string DefaultPath = "D:\\ETest";

        [DllImport("Installer.exe", SetLastError = true)]
        public static extern void Exit();

        [DllImport("Installer.exe", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void ExtractInstall(string destFolder, bool isShortCut, bool isStartMenu);

        public MainWindow()
        {
            InitializeComponent();
            this.TB_Path.Text = DefaultPath;
        }

        private void HideOption()
        {
            this.B_Browser.Visibility = Visibility.Hidden;
            this.B_FilePath.Visibility = Visibility.Hidden;
            this.B_Install.Visibility = Visibility.Hidden;
            this.CB_Agree.Visibility = Visibility.Hidden;
            this.CB_Icon.Visibility = Visibility.Hidden;
            this.CB_Menu.Visibility = Visibility.Hidden;
            this.CB_License.Visibility = Visibility.Hidden;
        }

        private void ShowProgress()
        {
            this.PB_ProgressBar.Visibility = Visibility.Visible;
            this.TB_ProgressText.Visibility = Visibility.Visible;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.B_Install_MouseDown(null, null);
            }
        }

        private void B_Install_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.CB_Agree.IsChecked == false)
            {
                MessageBox.Show("必须同意用户协议才可以安装！");
                return;
            }

            this.HideOption();
            this.ShowProgress();

            var tm = new System.Timers.Timer();
            tm.Interval = 50;
            tm.AutoReset = true;
            tm.Enabled = true;
            tm.Elapsed += Timer_Elapsed;

            string path = this.TB_Path.Text;
            bool isIcon = this.CB_Icon.IsChecked.Value;
            bool isMenu = this.CB_Menu.IsChecked.Value;

            var thread = new Thread(() =>
            {
                ExtractInstall(path, isIcon, isMenu);
            });

            thread.IsBackground = true;
            thread.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                if (App.IsFinsh == true)
                {
                    this.PB_ProgressBar.Value = 100;
                    this.TB_ProgressText.Visibility = Visibility.Hidden;
                    this.B_Finsh.Visibility = Visibility.Visible;
                }
                else
                {
                    if (this.PB_ProgressBar.Value < App.Value)
                    {
                        this.PB_ProgressBar.Value++;
                    }
                    else
                    {
                        this.PB_ProgressBar.Value = App.Value;
                    }
                    this.TB_ProgressText.Text = this.PB_ProgressBar.Value + "%";
                }
            }));
        }

        private void B_Browser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folder = new System.Windows.Forms.FolderBrowserDialog();
            if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string s = "";
                if (folder.SelectedPath.EndsWith(":\\") == true)
                {
                    s += folder.SelectedPath + "VirtionSoftware";
                }
                else
                {
                    s += folder.SelectedPath;
                }
                this.TB_Path.Text = s;
            }
        }

        private void B_Close_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            App.Current.Shutdown(0);
            Exit();
        }

        private void B_FilePath_OnMouseEnter(object sender, MouseEventArgs e)
        {
            this.B_FilePath.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AA333333"));
        }

        private void B_FilePath_OnMouseLeave(object sender, MouseEventArgs e)
        {
            this.B_FilePath.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#23333333"));
        }

        private void B_Browser_OnMouseLeave(object sender, MouseEventArgs e)
        {
            this.B_Browser.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#23333333"));
        }

        private void B_Browser_OnMouseEnter(object sender, MouseEventArgs e)
        {
            this.B_Browser.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AA333333"));
        }

        private void B_Install_OnMouseEnter(object sender, MouseEventArgs e)
        {
            this.B_Install.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007acc"));
        }

        private void B_Install_OnMouseLeave(object sender, MouseEventArgs e)
        {
            this.B_Install.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3196ef"));
        }

        private void B_Close_OnMouseEnter(object sender, MouseEventArgs e)
        {
            this.B_Close.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EEEEEE"));
        }

        private void B_Close_OnMouseLeave(object sender, MouseEventArgs e)
        {
            this.B_Close.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void B_Finsh_OnMouseLeave(object sender, MouseEventArgs e)
        {
            this.B_Finsh.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3196ef"));
        }

        private void B_Finsh_OnMouseEnter(object sender, MouseEventArgs e)
        {
            this.B_Finsh.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007acc"));
        }

        private void B_Finsh_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo(App.InstallPath);
            info.UseShellExecute = true;
            info.WindowStyle = ProcessWindowStyle.Normal;
            System.Diagnostics.Process.Start(info);
            App.Current.Shutdown(0);
            Exit();
        }
    }
}
