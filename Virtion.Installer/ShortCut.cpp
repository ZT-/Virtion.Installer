#include <Windows.h>
#include <atlstr.h>
#include <Shlwapi.h>
#include <shlobj.h>   
#include <string>

#pragma comment(lib, "shell32.lib")

std::wstring g_IconPath;
std::wstring g_StartMenuPath;

void GetDesktopPath(wchar_t* path)
{
	SHGetSpecialFolderPath(nullptr, path, CSIDL_DESKTOPDIRECTORY, FALSE);
}

void GetStartMenuPath(wchar_t* path)
{
	SHGetSpecialFolderPath(nullptr, path, CSIDL_PROGRAMS, FALSE);
}

void CreateShortCut(CString csLinkPath, CString csExePath, CString startPath)
{
	HRESULT hres;
	//hres = ::CoInitialize(NULL);
	//if (S_OK == hres)
	//{
	IShellLink * pShellLink;
	hres = ::CoCreateInstance(CLSID_ShellLink, NULL, CLSCTX_INPROC_SERVER, IID_IShellLink, (void **)&pShellLink);
	if (SUCCEEDED(hres))
	{
		pShellLink->SetPath(csExePath);

		if (PathFileExists(startPath))
			pShellLink->SetWorkingDirectory(startPath);

		pShellLink->SetHotkey(MAKEWORD('R', HOTKEYF_SHIFT | HOTKEYF_CONTROL));

		IPersistFile *pPersistFile;
		hres = pShellLink->QueryInterface(IID_IPersistFile, (void **)&pPersistFile);
		if (SUCCEEDED(hres))
		{
			hres = pPersistFile->Save(csLinkPath, TRUE);
			pPersistFile->Release();
		}
		pShellLink->Release();
	}
	::CoUninitialize();
	//}
}

void CreateDesktopIcon(const wchar_t * iconName, const wchar_t * ExePath, const wchar_t * startPath)
{
	wchar_t  path[MAX_PATH];
	GetDesktopPath(path);

	CString iconPath;
	iconPath += path;
	iconPath += "\\";
	iconPath += iconName;
	g_IconPath = iconPath;
	CreateShortCut(iconPath, ExePath, startPath);
}

void CreateStartMenu(const wchar_t* appName, const wchar_t* exePath, const wchar_t* uninstallEXE, const wchar_t*  startPath)
{
	wchar_t  path[MAX_PATH];
	GetStartMenuPath(path);

	CString folderPath = path;
	folderPath += "\\";
	folderPath += appName;

	BOOL result = PathFileExists(folderPath);
	if (result == false)
	{
		CreateDirectory(folderPath, nullptr);
	}
	g_StartMenuPath = folderPath;

	CString iconPath = folderPath + "\\";
	iconPath += appName;
	iconPath += ".lnk";
	CreateShortCut(iconPath, exePath, startPath);

	CString uninstallIconPath = folderPath + "\\";
	uninstallIconPath += "п╤ть.lnk";
	CreateShortCut(uninstallIconPath, uninstallEXE, startPath);

}