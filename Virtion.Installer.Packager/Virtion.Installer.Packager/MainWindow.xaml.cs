using System;
using System.IO;
using MahApps.Metro.Controls;
using System.Windows;
using Newtonsoft.Json;
using SevenZip;
using Virtion.Installer.Packager.Struct;
using Virtion.Installer.Packager.Window;
using Virtion.Util;

namespace Virtion.Installer.Packager
{
    public partial class MainWindow : MetroWindow
    {
        private ProjectConfig currentProject;
        public ProjectConfig CurrentProject
        {
            set
            {
                if (value == null)
                {
                    this.P_Setting.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.P_Setting.Visibility = Visibility.Visible;
                }
                this.currentProject = value;
            }
            get { return this.currentProject; }
        }

        public string ProjectPath;
        public const string DefaultIcon = "default.ico";
        public const string DefaultUninstallModule = "Uninstall.exe";
        public const string DefaultUiModule = "Virtion.Installer.UI.exe";
        public string InstallFilesPath
        {
            get
            {
                return App.MainWindow.ProjectPath + "Install Files\\";
            }
        }
        public string PackageFilesPath
        {
            get
            {
                return App.MainWindow.ProjectPath + "Package Files\\";
            }
        }
        public string OutputPath
        {
            get
            {
                return App.MainWindow.ProjectPath + "Output\\";
            }
        }
        public string BuilderPath
        {
            get
            {
                return App.CurrentPath + "Builder\\";
            }
        }
        public string DefaultResPath
        {
            get
            {
                return App.CurrentPath + "Default\\x86\\";
            }
        }


        public MainWindow()
        {
            InitializeComponent();
        }


        private void B_Open_OnClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter = "Project|*.vproj";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    string s = File.ReadAllText(dialog.FileName);
                    this.CurrentProject = JsonConvert.DeserializeObject<ProjectConfig>(s);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show("Project file is error");
                    return;
                }
                this.ProjectPath = Path.GetDirectoryName(dialog.FileName) + "\\";
                this.P_Resource.LoadConfig();
                this.P_Setting.LoadConfig();
                if (this.CurrentProject.Platform == Platform.X86)
                {
                    this.CB_Platform.SelectedIndex = 0;
                }
                else
                {
                    this.CB_Platform.SelectedIndex = 1;
                }

            }
        }

        private void B_Build_OnClick(object sender, RoutedEventArgs e)
        {
            Complier complier = new Complier();
            if (File.Exists(this.OutputPath) == false)
            {
                Directory.CreateDirectory(this.OutputPath);
            }

            complier.Start();
        }

        private void B_New_OnClick(object sender, RoutedEventArgs e)
        {
            (new NewProjectWindow()).ShowDialog();
        }

        private void B_Save_OnClick(object sender, RoutedEventArgs e)
        {
            this.P_Setting.SaveConfig();

            if (this.CB_Platform.Text == "X86")
            {
                this.CurrentProject.Platform = Platform.X86;
            }
            else
            {
                this.CurrentProject.Platform = Platform.X64;
            }
            string s = JsonConvert.SerializeObject(this.CurrentProject);
            FileManager.WriteFile(this.ProjectPath + this.CurrentProject.ProjectName + ".vproj", s);
        }

        private void CB_Platform_OnDropDownClosed(object sender, EventArgs e)
        {
            if (this.CurrentProject == null)
            {
                return;
            }
            if (this.CB_Platform.Text == "X86")
            {
                this.CurrentProject.Platform = Platform.X86;
            }
            else
            {
                this.CurrentProject.Platform = Platform.X64;
            }
        }
    }
}
