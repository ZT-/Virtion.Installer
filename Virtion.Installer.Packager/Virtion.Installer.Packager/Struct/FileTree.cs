using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Virtion.Installer.Packager.Struct
{
    public class FileTreeItem
    {
        public string Name;
        public string Type;
        public List<FileTreeItem> Children;
    }
}
