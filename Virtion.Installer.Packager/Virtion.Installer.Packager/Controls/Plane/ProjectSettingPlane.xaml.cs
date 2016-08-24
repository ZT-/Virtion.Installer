using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Virtion.Installer.Packager.Struct;

namespace Virtion.Installer.Packager.Controls.Plane
{
    public partial class ProjectSettingPlane : UserControl
    {
        public bool IsChange;
        private ProjectConfig config;

        public ProjectSettingPlane()
        {
            InitializeComponent();
        }

        public void LoadConfig()
        {
            config = App.MainWindow.CurrentProject;
            if (config == null)
            {
                return;
            }
            if (config.AppName != null)
            {
                this.TB_AppName.Text = config.AppName;
            }
            if (config.Version != null)
            {
                this.TB_Version.Text = config.Version;
            }

            if (config.IconPath != null)
            {
                this.TB_IconPath.Text = config.IconPath;
            }
            if (config.MainModule != null)
            {
                this.TB_MainModule.Text = config.MainModule;
            }
            if (config.UiModule != null)
            {
                this.TB_UiModule.Text = config.UiModule;
            }
            if (config.UninstallModule != null)
            {
                this.TB_UninstallModule.Text = config.UninstallModule;
            }
        }

        private string installFilesPath
        {
            get
            {
                return App.MainWindow.ProjectPath + "Install Files\\";
            }
        }

        private string packageFilesPath
        {
            get
            {
                return App.MainWindow.ProjectPath + "Package Files\\";
            }
        }


        public void SaveConfig(string key, string value)
        {
            if (string.IsNullOrEmpty(value) == true)
            {
                return;
            }
            if (config == null)
            {
                return;
            }

            this.IsChange = true;
            switch (key)
            {
                case "AppName":
                    if (config.AppName != value)
                    {
                        config.AppName = value;
                        return;
                    }
                    break;
                case "Version":
                    if (config.Version != value)
                    {
                        config.Version = value;
                        return;
                    }
                    break;
                case "IconPath":
                    if (config.IconPath != value)
                    {
                        config.IconPath = value;
                        return;
                    }
                    break;
                case "MainModule":
                    if (config.MainModule != value)
                    {
                        config.MainModule = value;
                        return;
                    }
                    break;
                case "UiModule":
                    if (config.UiModule != value)
                    {
                        config.UiModule = value;
                        return;
                    }
                    break;
                case "UninstallModule":
                    if (config.UninstallModule != value)
                    {
                        config.UninstallModule = value;
                        return;
                    }
                    break;
                default:
                    break;
            }

            this.IsChange = false;
        }

        public void Clear()
        {
            this.TB_AppName.Text = null;
            this.TB_Version.Text = null;
            this.TB_IconPath.Text = null;
            this.TB_MainModule.Text = null;
            this.TB_UiModule.Text = null;
            this.TB_UninstallModule.Text = null;
        }

        private void B_Icon_OnClick(object sender, RoutedEventArgs e)
        {
            if (config == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(App.MainWindow.ProjectPath) == true)
            {
                return;
            }
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.InitialDirectory = this.installFilesPath;
            dialog.Filter = "Icon|*.ico";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string srcPath = Path.GetDirectoryName(dialog.FileName) + "\\";
                if (srcPath != this.installFilesPath)
                {
                    try
                    {
                        File.Copy(dialog.FileName, this.installFilesPath + dialog.SafeFileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }
                this.TB_IconPath.Text = dialog.SafeFileName;
                this.SaveConfig("IconPath", dialog.SafeFileName);
            }
        }

        private void B_MainModule_OnClick(object sender, RoutedEventArgs e)
        {
            if (config == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(App.MainWindow.ProjectPath) == true)
            {
                return;
            }
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.InitialDirectory = this.packageFilesPath;
            dialog.Filter = "Main Exe|*.exe";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string srcPath = Path.GetDirectoryName(dialog.FileName) + "\\";
                if (srcPath != this.packageFilesPath)
                {
                    try
                    {
                        File.Copy(dialog.FileName, this.packageFilesPath + dialog.SafeFileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }
                this.TB_MainModule.Text = dialog.SafeFileName;
                this.SaveConfig("MainModule", dialog.SafeFileName);
            }
        }

        private void B_UiModule_OnClick(object sender, RoutedEventArgs e)
        {
            if (config == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(App.MainWindow.ProjectPath) == true)
            {
                return;
            }
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.InitialDirectory = this.installFilesPath;
            dialog.Filter = "UI|*.exe";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string srcPath = Path.GetDirectoryName(dialog.FileName) + "\\";
                if (srcPath != this.installFilesPath)
                {
                    try
                    {
                        File.Copy(dialog.FileName, this.installFilesPath + dialog.SafeFileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }
                this.TB_UiModule.Text = dialog.SafeFileName;
                this.SaveConfig("UiModule", dialog.SafeFileName);
            }
        }

        private void B_UninstallModule_OnClick(object sender, RoutedEventArgs e)
        {
            if (config == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(App.MainWindow.ProjectPath) == true)
            {
                return;
            }
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.InitialDirectory = this.installFilesPath;
            dialog.Filter = "Uninstall|*.exe";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string srcPath = Path.GetDirectoryName(dialog.FileName) + "\\";
                if (srcPath != this.installFilesPath)
                {
                    try
                    {
                        File.Copy(dialog.FileName, this.installFilesPath + dialog.SafeFileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }
                this.TB_UninstallModule.Text = dialog.SafeFileName;
                this.SaveConfig("UninstallModule", dialog.SafeFileName);
            }
        }

        private void TB_AppName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            this.SaveConfig("AppName", this.TB_AppName.Text);
        }

        private void TB_Version_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            this.SaveConfig("Version", this.TB_Version.Text);
        }

        private void TB_IconPath_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            this.SaveConfig("IconPath", this.TB_IconPath.Text);
        }

        private void TB_MainModule_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            this.SaveConfig("MainModule", this.TB_MainModule.Text);
        }

        private void TB_UiModule_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            this.SaveConfig("UiModule", this.TB_UiModule.Text);
        }

        private void TB_UninstallModule_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            this.SaveConfig("UninstallModule", this.TB_UninstallModule.Text);
        }


    }
}
