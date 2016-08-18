using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using SevenZip;


namespace Virtion.Installer.Packager
{
    public class Complier : DependencyObject
    {
        public Dictionary<string, string> RecourseMap;
        public Dictionary<string, string> CommandMap;
        private string installdat;
        private string rcRecourse;
        private string rcCommand;
        private string linkCommand;
        private string builderRootPath
        {
            get { return App.MainWindow.BuilderPath; }
        }
        private string outputPath
        {
            get { return App.MainWindow.OutputPath; }
        }
        private Process process;

        public Complier()
        {

        }

        private void Log(string text)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                App.MainWindow.P_Log.Log(text);
            }));
        }

        public void Start()
        {
            App.MainWindow.P_Log.Clear();
            try
            {
                this.CompressPackage();
            }
            catch (Exception ex)
            {
                this.Log(ex.ToString());
            }
        }

        private void SetRecourseValue()
        {
            this.RecourseMap = new Dictionary<string, string>();
            RecourseMap["{Icon}"] = App.MainWindow.InstallFilesPath + App.MainWindow.CurrentProject.IconPath;
            RecourseMap["{Package}"] = this.outputPath + "pack";
            RecourseMap["{UiModule}"] = App.MainWindow.InstallFilesPath + App.MainWindow.CurrentProject.UiModule;
            RecourseMap["{UninstallModule}"] = App.MainWindow.InstallFilesPath + App.MainWindow.CurrentProject.UninstallModule;
            RecourseMap["{MainEXE}"] = App.MainWindow.CurrentProject.MainModule;
            RecourseMap["{AppName}"] = App.MainWindow.CurrentProject.AppName;
            RecourseMap["{Version}"] = App.MainWindow.CurrentProject.Version;
        }

        private void SetCommandValue()
        {
            string platform = App.MainWindow.CurrentProject.Platform.ToString();
            this.CommandMap = new Dictionary<string, string>();
            CommandMap["{BuilderRoot}"] = this.builderRootPath;
            CommandMap["{Platform}"] = platform;
            CommandMap["{OutputPath}"] = this.outputPath;
            CommandMap["{LibPath}"] = this.builderRootPath + platform + "\\Lib\\";
            CommandMap["{ObjPath}"] = this.builderRootPath + platform + "\\Obj\\";
        }

        private bool LoadDefaultFile()
        {
            try
            {
                this.installdat = File.ReadAllText(this.builderRootPath + "install.dat");
                this.rcRecourse = File.ReadAllText(this.builderRootPath + "Installer.rc");
                this.rcCommand = File.ReadAllText(this.builderRootPath + "rc.cmd");
                this.linkCommand = File.ReadAllText(this.builderRootPath + "link.cmd");
            }
            catch (Exception ex)
            {
                this.Log(ex.ToString());
                return false;
            }
            return true;
        }

        private bool CreateInstallDat()
        {
            foreach (var item in this.RecourseMap)
            {
                this.installdat = this.installdat.Replace(item.Key, item.Value);
            }
            string s = outputPath + "Install.Dat";
            RecourseMap["{InstallData}"] = s;
            try
            {
                File.WriteAllText(s, this.installdat, Encoding.Unicode);
            }
            catch (Exception ex)
            {
                this.Log(ex.ToString());
                return false;
            }
            return true;
        }

        private bool CreateRcFile()
        {
            foreach (var item in this.RecourseMap)
            {
                this.rcRecourse = this.rcRecourse.Replace(item.Key, item.Value);
            }
            string s = outputPath + "Installer.rc";
            CommandMap["{RcFile}"] = s;
            try
            {
                File.WriteAllText(s, this.rcRecourse, Encoding.Unicode);
            }
            catch (Exception ex)
            {
                this.Log(ex.ToString());
                return false;
            }
            return true;
        }

        private void CreateCommand()
        {
            foreach (var item in this.CommandMap)
            {
                this.rcCommand = this.rcCommand.Replace(item.Key, item.Value);
            }
            //this.rcCommand += "\r\nexit";
            File.WriteAllText(outputPath + "rc.cmd", this.rcCommand, Encoding.ASCII);
            foreach (var item in this.CommandMap)
            {
                this.linkCommand = this.linkCommand.Replace(item.Key, item.Value);
            }
            this.linkCommand = this.linkCommand.Replace("\r\n", " ");
            //this.linkCommand += "\r\nexit";
            File.WriteAllText(outputPath + "link.cmd", this.linkCommand, Encoding.ASCII);
        }


        private void RunCommand(string cmd, EventHandler exitCallBack)
        {
            ProcessStartInfo info = new ProcessStartInfo(outputPath + cmd);
            info.UseShellExecute = false;
            info.RedirectStandardError = true;
            info.RedirectStandardOutput = true;
            info.CreateNoWindow = true;
            process = new Process();
            process.StartInfo = info;
            process.ErrorDataReceived += p_ErrorDataReceived;
            process.OutputDataReceived += p_OutputDataReceived;
            process.Exited += exitCallBack;
            process.EnableRaisingEvents = true;
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            //p.WaitForExit();
        }

        void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Data) == false)
            {
                this.Log(e.Data);
            }
        }

        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Data) == false)
            {
                this.Log(e.Data);
            }
        }

        private void CompressPackage()
        {
            SevenZipBase.SetLibraryPath(App.CurrentPath + "7z64.dll");
            SevenZipCompressor cmp = new SevenZipCompressor();
            cmp.Compressing += new EventHandler<ProgressEventArgs>(Cmp_Compressing);
            cmp.FileCompressionStarted += new EventHandler<FileNameEventArgs>(Cmp_FileCompressionStarted);
            cmp.CompressionFinished += new EventHandler<EventArgs>(Cmp_CompressionFinished);
            cmp.ArchiveFormat = OutArchiveFormat.SevenZip;
            cmp.CompressionLevel = CompressionLevel.High;
            cmp.BeginCompressDirectory(App.MainWindow.PackageFilesPath, this.outputPath + "pack");
        }

        private void Cmp_Compressing(object sender, ProgressEventArgs e)
        {
            //pb_Compress.Value += (e.PercentDelta);
        }

        private void Cmp_FileCompressionStarted(object sender, FileNameEventArgs e)
        {
            //l_CompressStatus.Content = String.Format("Compressing \"{0}\"", e.FileName);
        }

        private void Cmp_CompressionFinished(object sender, EventArgs e)
        {
            this.Log("Packgae > " + this.outputPath + "pack");
            if (LoadDefaultFile() == false)
            {
                return;
            }
            this.SetRecourseValue();
            if (this.CreateInstallDat() == false)
            {
                return;
            }
            this.Log("Res > " + this.outputPath + "install.dat");
            this.SetCommandValue();
            if (this.CreateRcFile() == false)
            {
                return;
            }

            this.Log("Res > " + this.outputPath + "Installer.rc");
            this.CreateCommand();
            this.RunCommand("rc.cmd", this.RcCmd_Exit);
        }

        private void RcCmd_Exit(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.Log("Res > " + this.outputPath + "Installer.Res");
                process.Close();
                this.RunCommand("link.cmd", this.LinkCmd_Exit);
            }));
        }

        private void LinkCmd_Exit(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.Log("Link > " + this.outputPath + "Installer.exe");
                process.Close();
            }));
        }


    }
}
