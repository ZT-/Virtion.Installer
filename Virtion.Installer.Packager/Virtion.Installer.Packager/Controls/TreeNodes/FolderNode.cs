using System.Windows;
using Virtion.TreeView;
using Virtion.Util;

namespace Virtion.Installer.Packager.Controls.TreeNodes
{
    public class FolderNode : TreeNode
    {
        public FolderNode(string path)
        {
            this.name = path;
        }

        public FolderNode(FolderNode node)
        {
            this.name = node.name;
            foreach (var item in node.Children)
            {
                if (item is FileNode)
                {
                    this.Children.Add(new FileNode(item as FileNode));
                }
                else
                {
                    this.Children.Add(new FolderNode(item as FolderNode));
                }
            }
        }

        public override object Icon
        {
            get
            {
                return ResourceHelper.LoadIcon("Folder.png");
            }
        }

        public override object ExpandedIcon
        {
            get
            {
                return ResourceHelper.LoadIcon("FolderOpened.png");
            }
        }

        public bool CanPaste(IDataObject data)
        {
            return data.GetDataPresent(DataFormats.FileDrop);
        }

        public override bool CanDrop(DragEventArgs e, int index)
        {
            var paths = e.Data.GetData(typeof(SharpTreeNode[])) as SharpTreeNode[];
            foreach (var item in paths)
            {
                if (item == this)
                {
                    e.Effects = DragDropEffects.None;
                    return false;
                }
            }
            e.Effects = DragDropEffects.Move;
            return true;
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
                    else
                    {
                        var node = new FolderNode(item as FolderNode);
                        Children.Insert(index++, node);
                    }
                }
            }
        }

    }
}
