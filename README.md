# Virtion.Installer
A beautiful and easy to use installer on windows

This project uses WPF to make a beautiful GUI, and uses LzmaSDK to uncompress package files.

#How to use it?
1.Open Virtion.Installer.Packager.exe and click the menu "New".
2.Input your project name and browser local path to save project,It will create some default files in the path.
3.Copy files whitch you want to pack up into the installer to "Package Files" in your project path.
4.finish some information on the main window.
  -AppName:Name of Your product Name,whitch will display on desktop icon and start menu after install
  -Version: version
  -IconPath: icon of installer.exe 
  -MainModuel: Main exe of your product's
  -UiModuel: GUI of installer
  -UninstallModuel: Uninstall.exe
5.Click the menu "Build" and look the result on the output plane.if success the installer.exe will appera on the "Output" 
  folder in your project path.
6.Save project and exit.


