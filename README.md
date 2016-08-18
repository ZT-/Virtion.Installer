# Virtion.Installer
A beautiful and easy to use installer on windows

This project uses WPF to make a beautiful GUI, and uses LzmaSDK to uncompress package files.
![image](https://github.com/ZT-/Virtion.Installer/tree/master/Image/screenshot.png)
 
#How to use it?
1. Open Virtion.Installer.Packager.exe and click the menu "New".
2. Input your project name and browser local path to save project,It will create some default files in the path.
3. Copy files whitch you want to pack up into the installer to "Package Files" in your project path.
4. finish some information on the main window.
  - AppName:Name of Your product Name,whitch will display on desktop icon and start menu after install
  - Version: version
  - IconPath: icon of installer.exe
  - MainModule: Main exe of your product's
  - UiModule: GUI of installer
  - UninstallModule: Uninstall.exe
5. Click the menu "Build" and look the result on the output plane.if success the installer.exe will appera on the "Output" folder in your project path. 
6. Save project and exit.

#Module Description
+ Installer is made up of two parts
 - Virtion.Installer.UI is the GUI of installer,I use wpf to write this part,you can use other framework or other language to write it,only if call some simple interfaces whitch i supplied.
 - Virtion.Installer is the core module of install,this part use c++ writen.It used LzmaSDK uncompress 7z package form PE resourse. And it has some other helper function like create disktop icon,start menu icon and create uninstall info on system register table.
+ Packger is Virtion.Installer.Packager module,it also use wpf to write gui.The rc.exe and link.exe is a little part of vs2013 complier.
+ uninstall module read uninstall.dat to remove install files and other info form you computer.

#Working Principle
wait ... 
