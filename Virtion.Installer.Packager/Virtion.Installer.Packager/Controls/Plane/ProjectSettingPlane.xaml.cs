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
            if (config.MainModuel != null)
            {
                this.TB_MainModuel.Text = config.MainModuel;
            }
            if (config.UiModuel != null)
            {
                this.TB_UiModuel.Text = config.UiModuel;
            }
            if (config.UninstallModuel != null)
            {
                this.TB_UninstallModuel.Text = config.UninstallModuel;
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
            config.MainModuel = this.TB_MainModuel.Text;
            config.UiModuel = this.TB_UiModuel.Text;
            config.UninstallModuel = this.TB_UninstallModuel.Text;
        }

        public void Clear()
        {
            this.TB_AppName.Text = null;
            this.TB_Version.Text = null;
            this.TB_IconPath.Text = null;
            this.TB_MainModuel.Text = null;
            this.TB_UiModuel.Text = null;
            this.TB_UninstallModuel.Text = null;
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

        private void B_MainModuel_OnClick(object sender, RoutedEventArgs e)
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
                this.TB_MainModuel.Text = dialog.SafeFileName;
            }
        }

        private void B_UiModuel_OnClick(object sender, RoutedEventArgs e)
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
                this.TB_UiModuel.Text = dialog.SafeFileName;
            }
        }

        private void B_UninstallModuel_OnClick(object sender, RoutedEventArgs e)
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
                this.TB_UninstallModuel.Text = dialog.SafeFileName;
            }
        }
    }
}
