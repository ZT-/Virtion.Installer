using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using Virtion.Installer.Packager.Struct;
using Virtion.Util;
using Path = System.IO.Path;

namespace Virtion.Installer.Packager.Window
{
    public partial class NewProjectWindow : MetroWindow
    {
        public NewProjectWindow()
        {
            InitializeComponent();

            this.TB_Path.Text = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }

        private void B_OK_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.TB_ProjectName.Text) == true)
            {
                MessageBox.Show("Project name must't be null");
                return;
            }

            if (string.IsNullOrEmpty(this.TB_Path.Text) == true)
            {
                MessageBox.Show("Project root path must't be null");
                return;
            }

            string path = Path.Combine(this.TB_Path.Text, this.TB_ProjectName.Text) + "\\";
            if (System.IO.Directory.Exists(path) == false)
            {
                try
                {
                    System.IO.Directory.CreateDirectory(path);
                    System.IO.Directory.CreateDirectory(path + "Install Files");
                    System.IO.Directory.CreateDirectory(path + "Package Files");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            var project = new ProjectConfig();
            project.ProjectName = this.TB_ProjectName.Text;
            project.UninstallModuel = MainWindow.DefaultUninstallModuel;

            project.UiModuel = MainWindow.DefaultUiModuel;
            project.IconPath = MainWindow.DefaultIcon;
            App.MainWindow.ProjectPath = path;
            App.MainWindow.CurrentProject = project;

            string[] files = null;
            try
            {
                files = Directory.GetFiles(App.MainWindow.DefaultResPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            foreach (var file in files)
            {
                var name = Path.GetFileName(file);
                File.Copy(file, path + "Install Files\\" + name, true);
            }

            string s = JsonConvert.SerializeObject(project);
            FileManager.WriteFile(path + this.TB_ProjectName.Text + ".vproj", s);

            App.MainWindow.P_Resource.LoadConfig();
            this.Close();
        }

        private void B_Browser_OnClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folder = new System.Windows.Forms.FolderBrowserDialog();
            if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TB_Path.Text = folder.SelectedPath;
            }
        }

        private void B_Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
