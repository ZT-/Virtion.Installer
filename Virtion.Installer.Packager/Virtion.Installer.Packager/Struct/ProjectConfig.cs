using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Virtion.Installer.Packager.Struct
{
    public enum Platform
    {
        X86,
        X64
    }

    public class ProjectConfig
    {
        public string ProjectName;
        public string Version;
        public string AppName;
        public string MainModule;
        public string IconPath;
        public string UiModule;
        public string UninstallModule;
        public Platform Platform;
        //public FileTreeItem PackageFiles;
    }


}
