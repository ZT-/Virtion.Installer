using System;
using System.Linq;
using System.Windows;
using Virtion.TreeView;

namespace Virtion.Installer.Packager.Controls.TreeNodes
{
    public abstract class TreeNode : SharpTreeNode
    {
        public string name;
        public Action<TreeNode, string> EditFinshedCallback;
        public override bool IsCheckable
        {
            get
            {
                return false;
            }
        }
        public override object Text
        {
            get
            {
                return name;
            }
        }
        public override string ToString()
        {
            return name;
        }
        public override string LoadEditText()
        {
            return name;
        }
        public override bool SaveEditText(string value)
        {
            if (value.Trim() == "")
            {
                MessageBox.Show("不能为空");
                return false;
            }
            else
            {  
                if (EditFinshedCallback != null)
                {
                    EditFinshedCallback(this,value);
                }
                name = value;
                return true;
            }
        }

        public bool CanCopy(SharpTreeNode[] nodes)
        {
            return nodes.All(n => n is TreeNode);
        }

        protected IDataObject GetDataObject(SharpTreeNode[] nodes)
        {
            var data = new DataObject();
            data.SetData(typeof(SharpTreeNode[]), nodes);
            return data;
        }

        public override object ToolTip
        {
            get
            {
                return DataContent;
            }
        }

        public string DataContent;

        public override bool CanDelete()
        {
            return true;
        }

        public void DeleteNoTip()
        {
            this.DeleteCore();
        }

        public override void Delete()
        {
            if (MessageBox.Show("确认删除" + this.name, "删除", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                this.DeleteCore();
            }
        }

        public override void DeleteCore()
        {
            this.Parent.Children.Remove(this);
        }

        public override bool CanDrag(SharpTreeNode[] nodes)
        {
            return true;
        }

        public override IDataObject Copy(SharpTreeNode[] nodes)
        {
            return GetDataObject(nodes);
        }


    }
}
