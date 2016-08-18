using System.Collections.Generic;
using System.Windows;
using Virtion.TreeView;
using Virtion.Util;

namespace Virtion.Installer.Packager.Controls.TreeNodes
{
    public class FileNode : TreeNode
    {
        public FileNode(string path)
        {
            this.name = path;
        }

        public FileNode(string path, List<string> list)
        {
            this.name = path;
        }

        public FileNode(FileNode node)
        {
            this.name = node.name;
        }

        public override object Text
        {
            get
            {
                return name;
            }
        }

        public override object Icon
        {
            get
            {
                return ResourceHelper.LoadIcon("File.png");
            }
        }

        public override bool CanDrop(DragEventArgs e, int index)
        {
            return false;
            //var paths = e.Data.GetData(typeof(SharpTreeNode[])) as SharpTreeNode[];
            //foreach (var item in paths)
            //{
            //    if (item == this)
            //    {
            //        e.Effects = DragDropEffects.None;
            //        return false;
            //    }
            //    else if (item is FolderNode)
            //    {
            //        e.Effects = DragDropEffects.None;
            //        return false;
            //    }
            //}
            //e.Effects = DragDropEffects.Move;
            //return true;
        }

        public override void Drop(DragEventArgs e, int index)
        {
            var paths = e.Data.GetData(typeof(SharpTreeNode[])) as SharpTreeNode[];
            if (paths != null)
            {
                foreach (var item in paths)
                {
                    if (item is FileNode)
                    {
                        var node = new FileNode(item as FileNode);
                        Children.Insert(index++, node);
                    }
                }
            }
        }


    }
}
