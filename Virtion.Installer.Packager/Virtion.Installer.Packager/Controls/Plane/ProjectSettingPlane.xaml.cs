using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Virtion.Installer.Packager.Controls.Plane
{
    public partial class ProjectSettingPlane : UserControl
    {
        public ProjectSettingPlane()
        {
            InitializeComponent();
        }

        public void LoadConfig()
        {
            var config = App.MainWindow.CurrentProject;
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


        public void SaveConfig()
        {
            var config = App.MainWindow.CurrentProject;
            if (config == null)
            {
                return;
            }
            config.AppName = this.TB_AppName.Text;
            config.Version = this.TB_Version.Text;
            config.IconPath = this.TB_IconPath.Text;
            config.MainModule = this.TB_MainModule.Text;
            config.UiModule = this.TB_UiModule.Text;
            config.UninstallModule = this.TB_UninstallModule.Text;
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
            }
        }

        private void B_MainModule_OnClick(object sender, RoutedEventArgs e)
        {
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
            }
        }

        private void B_UiModule_OnClick(object sender, RoutedEventArgs e)
        {
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
            }
        }

        private void B_UninstallModule_OnClick(object sender, RoutedEventArgs e)
        {
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
            }
        }
    }
}
