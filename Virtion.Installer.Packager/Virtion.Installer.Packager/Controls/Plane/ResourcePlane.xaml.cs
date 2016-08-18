using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using Virtion.Installer.Packager.Controls.TreeNodes;
using Virtion.Installer.Packager.Struct;
using Virtion.TreeView;
using Virtion.Util;
using Path = System.IO.Path;

namespace Virtion.Installer.Packager.Controls.Plane
{
    public partial class ResourcePlane : UserControl
    {
        private ProjectNode projectNode;
        private FolderNode packageFilesNode;
        private FolderNode installFilesNode;

        private string rootPath;
        private string installFilesPath;
        private string packageFilesPath;

        public MenuItem MI_Rename;
        public MenuItem MI_AddFile;
        public MenuItem MI_Delete;
        public MenuItem MI_OpenLocal;
        public MenuItem MI_Close;

        public ResourcePlane()
        {
            InitializeComponent();
            CreateMenuItem();
        }

        private void CreateMenuItem()
        {
            MI_Rename = new MenuItem()
            {
                Header = "Rename"
            };
            MI_Rename.Click += MI_Rename_Click;
            this.MI_AddFile = new MenuItem()
            {
                Header = "Add Files"
            };
            MI_AddFile.Click += MI_Add_Click;
            MI_Delete = new MenuItem()
            {
                Header = "Delete"
            };
            MI_Delete.Click += MI_Delete_Click;

            MI_OpenLocal = new MenuItem()
            {
                Header = "Open Local"
            };
            MI_OpenLocal.Click += MI_Open_Click;

            MI_Close = new MenuItem()
            {
                Header = "Close Project"
            };
            MI_Close.Click += MI_Close_Click;

        }

        public void LoadConfig()
        {
            var config = App.MainWindow.CurrentProject;
            if (config == null)
            {
                return;
            }

            this.rootPath = App.MainWindow.ProjectPath;
            this.packageFilesPath = rootPath + "Package Files\\";
            this.installFilesPath = rootPath + "Install Files\\";

            this.TV_Project.Root = new SharpTreeNode();
            this.TV_Project.Root.IsExpanded = true;
            this.BuildProjectItem(config.ProjectName);
            this.BuildFolderNode(config);

        }

        private void BuildFolderNode(ProjectConfig config)
        {
            this.installFilesNode = new FolderNode("Install Files");
            this.installFilesNode.DataContent = installFilesPath;
            this.projectNode.Children.Add(this.installFilesNode);
            this.packageFilesNode = new FolderNode("Package Files");
            this.packageFilesNode.DataContent = packageFilesPath;

            this.projectNode.Children.Add(packageFilesNode);

            //if (config.PackageFiles != null)
            //{
            //    this.BuildPackageFilesTree(config.PackageFiles);
            //}
            this.B_Reflesh_OnClick(null,null);
        }

        private void ScanDirectory(FolderNode folderNode, string path)
        {
            string[] directorys = null;
            try
            {
                directorys = Directory.GetDirectories(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            foreach (var dir in directorys)
            {
                string name = Path.GetFileName(dir);
                var dirNode =new FolderNode(name);
                dirNode.DataContent = dir;
                folderNode.Children.Add(dirNode);
                ScanDirectory(dirNode, dir);
            }

            string[] files = null;
            try
            {
                files = Directory.GetFiles(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            foreach (var file in files)
            {
                var fileNode = CreateFileNode(file);
                folderNode.Children.Add(fileNode);
            }
        }

        private FileNode CreateFileNode(string path)
        {
            var name = Path.GetFileName(path);
            var fileNode = new FileNode(name);
            fileNode.DataContent = path;
            fileNode.EditFinshedCallback += FileNode_EditFinshed;
            return fileNode;
        }

        private void BuildProjectItem(string name)
        {
            projectNode = new ProjectNode()
            {
                name = name,
                DataContent = this.rootPath
            };
            projectNode.EditFinshedCallback += Project_RemaneCallback;
            this.TV_Project.Root.Children.Add(projectNode);
            projectNode.IsExpanded = true;
        }


        #region Menu Event
        private void TV_Project_OnContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            this.TV_Project.ContextMenu.Items.Clear();
            this.TV_Project.ContextMenu.Items.Add(MI_OpenLocal);

            if (this.TV_Project.SelectedItems.Count > 1)
            {
                bool flag = true;
                foreach (var item in this.TV_Project.SelectedItems)
                {
                    if (item is FileNode == false)
                    {
                        flag = false;
                    }
                }
                if (flag == true)
                {
                    this.TV_Project.ContextMenu.Items.Add(MI_Delete);
                }
                return;
            }

            if (this.TV_Project.SelectedItem is ProjectNode)
            {
                this.TV_Project.ContextMenu.Items.Add(MI_Rename);
                this.TV_Project.ContextMenu.Items.Add(MI_Close);

                return;
            }

            if (this.TV_Project.SelectedItem is FolderNode)
            {
                this.TV_Project.ContextMenu.Items.Add(MI_AddFile);

                return;
            }

            if (this.TV_Project.SelectedItem is FileNode)
            {
                this.TV_Project.ContextMenu.Items.Add(MI_Rename);
                this.TV_Project.ContextMenu.Items.Add(MI_Delete);
                return;
            }
        }

        private void MI_Close_Click(object sender, RoutedEventArgs e)
        {
            this.TV_Project.Root.Children.Clear();
            App.MainWindow.CurrentProject = null;
            App.MainWindow.P_Setting.Clear();
        }

        private void MI_Open_Click(object sender, RoutedEventArgs e)
        {
            var node = this.TV_Project.SelectedItem as TreeNode;
            string path = node.DataContent;
            if (Directory.Exists(path) == false)
            {
                path = Path.GetDirectoryName(path);
            }
            ProcessStartInfo info = new ProcessStartInfo("explorer.exe", path);
            info.UseShellExecute = true;
            info.WindowStyle = ProcessWindowStyle.Normal;
            System.Diagnostics.Process.Start(info);
        }

        private void MI_Delete_Click(object sender, RoutedEventArgs e)
        {
            List<FileNode> list = new List<FileNode>();
            foreach (var obj in this.TV_Project.SelectedItems)
            {
                list.Add(obj as FileNode);
            }
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                string path = item.DataContent;
                if (File.Exists(path) == true)
                {
                    File.Delete(path);
                }
                item.DeleteNoTip();
            }
        }

        private void MI_Add_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.ValidateNames = false;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = false;
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var files = dialog.FileNames;
                foreach (var file in files)
                {
                    var name = Path.GetFileName(file);
                    try
                    {
                        string destName = this.packageFilesPath + name;
                        File.Copy(file, destName, true);
                        var fileNode = new FileNode(name);
                        fileNode.DataContent = destName;
                        this.packageFilesNode.Children.Add(fileNode);
                        this.packageFilesNode.IsExpanded = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void MI_Rename_Click(object sender, RoutedEventArgs e)
        {
            if (this.TV_Project.SelectedItem is ProjectNode)
            {
                var node = this.TV_Project.SelectedItem as TreeNode;
                node.IsEditing = true;
                return;
            }
            if (this.TV_Project.SelectedItem is FileNode)
            {
                var node = this.TV_Project.SelectedItem as TreeNode;
                node.IsEditing = true;
            }
        }

        private void FileNode_EditFinshed(TreeNode e, string s)
        {
            var dir = Path.GetDirectoryName(e.DataContent);
            FileManager.Rename(e.DataContent, dir + "\\" + s);
        }

        private void Project_RemaneCallback(TreeNode e, string name)
        {
            App.MainWindow.CurrentProject.ProjectName = name;
        }

        #endregion

        private void B_Reflesh_OnClick(object sender, RoutedEventArgs e)
        {
            packageFilesNode.Children.Clear();
            installFilesNode.Children.Clear();
            this.ScanDirectory(this.packageFilesNode, this.packageFilesPath);
            this.ScanDirectory(this.installFilesNode, this.installFilesPath);
        }
    }
}
