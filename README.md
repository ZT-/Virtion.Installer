# Virtion.Installer
A beautiful and easy to use <b>Installer</b> on windows

#Packager
![image](https://raw.githubusercontent.com/ZT-/Virtion.Installer/master/Other/Image/screenshot.png)
 
#Installer
![image](https://raw.githubusercontent.com/ZT-/Virtion.Installer/master/Other/Image/installer.png)

#Download
[Virtion.Packager V1.0](http://example.net/)

#Feature
+ Customizabl user interface module of installer.
+ Easy user <b>Packager</b> help you pack a installer more efficient.
+ Free and open source.
+ Support x86 and x64 platform.

#How to use Packager?
1. Open Virtion.Installer.Packager.exe and click the menu "New".
2. Input your project name and browser local path to save project,It will create some default files in the path.
3. Copy files whitch you want to pack up into the installer to "Package Files" in your project path.
4. finish some information on the main window.
  + AppName:Name of Your product Name,whitch will display on desktop icon and start menu after install
  + Version: version
  + IconPath: icon of installer.exe
  + MainModule: Main exe of your product's
  + UiModule: GUI of installer
  + UninstallModule: Uninstall.exe
5. Click the menu "Build" and look the result on the output plane.if success the installer.exe will appera on the "Output" folder in your project path. 
6. Save project and exit.

#How to make myself style Installer?
1. You can you i support ui module "Virtion.Installer.UI" in the project.
 + If you want to change installer background image just replace "Virtion.Installer.UI/Resource/bg.png".
 + If you want to change the default installer path just open "Virtion.Installer.UI/MainWindow.xaml.cs" replace
 ```
  public const string DefaultPath = "D:\\";
 ```
 + At the end you should rebuild the wpf project "Virtion.Installer.UI".
2. If you don't like to use wpf, you can use other framework or other language to write it,only if call some simple interfaces whitch i supplied.
 + You can read the source code "Virtion.Installer\Installer.cpp",learn how to use some interfaces.
 + It may be a little complex now,I want support other language mode in the future.

#Module Description
+ Installer is made up of two parts
 - Virtion.Installer.UI is the GUI of installer,I use wpf to write this part,you can use other framework or other language to write it,only if call some simple interfaces whitch i supplied.
 - Virtion.Installer is the core module of install,this part use c++ writen.It used LzmaSDK uncompress 7z package form PE resourse. And it has some other helper function like create disktop icon,start menu icon and create uninstall info on system register table.
+ Packger is Virtion.Installer.Packager module,it also use wpf to write gui.The rc.exe and link.exe is a little part of vs2013 complier.
+ uninstall module read uninstall.dat to remove install files and other info form you computer.

#Working Principle
+ About the installer:
 -This project uses WPF to make a beautiful GUI, and uses LzmaSDK to uncompress package files.
+ How C++ call C# method?
 -I use "CLR Hosting" Load the .net clr module,then call the method,C# call C++ DLL interface use "DllImport".
+ How packager builder a exe?
 -I divide a little part of vs2013 complier, put it into "Builder",then create cmd scripts real time,at last run the scripts use rc.exe and link.exe make a exe.