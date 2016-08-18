using Virtion.Util;

namespace Virtion.Installer.Packager.Controls.TreeNodes
{
    public class ProjectNode : TreeNode
    {
        public override object Icon
        {
            get
            {
                return ResourceHelper.LoadIcon("project.png");
            }
        }



    }
}
