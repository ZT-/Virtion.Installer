#include "stdafx.h"
#include "Install.h"
#include "Resource.h"
#include "ClrHosting.h"

#include <atlstr.h>
#include <fstream>
#include <sstream>
#include <string>
#include <map>


void ExtractPayload(const void* payload, size_t payloadSize);
void CreateShortCut(CString csLinkPath, CString csExePath, CString csIconPath);
void CreateDesktopIcon(const wchar_t * iconName, const wchar_t * ExePath, const wchar_t * startPath);
void CreateStartMenu(const wchar_t* appName, const wchar_t* exePath, const wchar_t* uninstallEXE, const wchar_t*  startPath);
bool CreateUninstallRegistryInfo(const wchar_t* displayName, const wchar_t* dir, const wchar_t* exe);

wchar_t g_CurrentPath[MAX_PATH];
const wchar_t* TypeName = L"Virtion.Installer.UI.App";
const wchar_t* UninstallKey = L"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\";
HINSTANCE g_hInstance;
std::map<std::wstring, std::wstring> InstallDataTable;
extern std::wstring g_IconPath;
extern std::wstring g_StartMenuPath;
ClrHosting* clrHosting = new ClrHosting();
std::wstring  exeFullPath;

void DoInstall(const wchar_t* destFolder)
{
	HRSRC payload = FindResource(g_hInstance, MAKEINTRESOURCE(IDR_LZMA), L"LZMA");

	const size_t payloadSize = SizeofResource(g_hInstance, payload);
	const void* payloadData = LockResource(LoadResource(g_hInstance, payload));

	SetCurrentDirectory(destFolder);

	ExtractPayload(payloadData, payloadSize);
}


void LoadUIModule()
{
	wchar_t tmpPath[MAX_PATH] = {};
	GetTempPath(MAX_PATH, tmpPath);

	PathAppend(tmpPath, L"\\Virtion.Installer.UI.exe");

	HRSRC payload = FindResource(g_hInstance, MAKEINTRESOURCE(IDR_UI), L"UI");

	const size_t payloadSize = SizeofResource(g_hInstance, payload);
	const void* payloadData = LockResource(LoadResource(g_hInstance, payload));

	std::ofstream  uiFile;
	uiFile.open(tmpPath, std::ios::ate | std::ios::binary);
	uiFile.write((const char*)payloadData, payloadSize);
	uiFile.close();

	clrHosting->InitClrHosting(tmpPath, TypeName);

}

void InstallFinish()
{
	clrHosting->Execute(TypeName, L"InstallFinish", exeFullPath.c_str());
}

int oldValue;
void SetInstallPercent(int value)
{
	wchar_t buff[10] = {};
	wsprintfW(buff, L"%d", value);
	if (value == 0)
	{
		clrHosting->Execute(TypeName, L"SetProgressValue", L"1");
	}
	if (oldValue != value)
	{
		TRACE(_T("SetInstallPercent=<%s>\n"), buff);
		clrHosting->Execute(TypeName, L"SetProgressValue", buff);
	}
	oldValue = value;
}


void ReadInstallData()
{
	HRSRC payload = FindResource(g_hInstance, MAKEINTRESOURCE(IDR_INSTALL_DATA), L"IDATA");

	const size_t payloadSize = SizeofResource(g_hInstance, payload);
	const wchar_t* payloadData = (const wchar_t*)LockResource(LoadResource(g_hInstance, payload));

	using namespace std;
	wstring  buff(payloadData, payloadSize / 2);
	wstringstream ss(buff);

	wstring key;
	wstring value;

	do
	{
		ss >> key >> value;
		InstallDataTable[key] = value;
	} while (ss.eof() == false);

}

void ExtractUninstallExe(const wchar_t* destFloder)
{
	HRSRC payload = FindResource(g_hInstance, MAKEINTRESOURCE(IDR_UNINSTALL_EXE), L"UEXE");
	const size_t payloadSize = SizeofResource(g_hInstance, payload);
	const void* payloadData = (const void*)LockResource(LoadResource(g_hInstance, payload));

	CString path = destFloder;
	path += "\\uninstall.exe";
	std::ofstream  file;
	file.open(path, std::ios::ate | std::ios::binary);
	file.write((const char*)payloadData, payloadSize);
	file.close();

}

void SaveUninstallData(const wchar_t* destFloder)
{
	using namespace std;
	wstring info = L"FOLDER>";
	info += destFloder;
	info += L"\r\n";
	info += L"FOLDER>";
	info += g_StartMenuPath.c_str();
	info += L"\r\n";
	info += L"FILE>";
	info += g_IconPath.c_str();
	info += L"\r\n";
	info += L"REG>";
	info += UninstallKey;
	info += InstallDataTable[L"AppName"].c_str();
	info += L"\r\n";

	CString path = destFloder;
	path += "\\uninstall.dat";

	std::ofstream  file;
	file.open(path, std::ios::ate | std::ios::binary);
	file.write("\xff\xfe", 2);
	file.write((const char*)info.c_str(), info.length() * 2);
	file.close();

}

void DoExtractInstall(wchar_t* destFloder, bool isShortCut, bool isStartMenu)
{
	BOOL result = PathFileExists(destFloder);
	if (result == false)
	{
		CreateDirectory(destFloder, NULL);
	}

	DoInstall(destFloder);
	ExtractUninstallExe(destFloder);

	using namespace  std;
	wstring appName = InstallDataTable[L"AppName"];
	wstring desktopIconName = appName;
	desktopIconName += L".lnk";

	wstring  mainEXE = InstallDataTable[L"MainEXE"];
	exeFullPath = destFloder;
	exeFullPath += L"\\";
	exeFullPath += mainEXE;

	if (isShortCut == true)
	{
		CreateDesktopIcon(desktopIconName.c_str(), exeFullPath.c_str(), destFloder);
	}

	wstring uninstallFullPath =destFloder;
	uninstallFullPath += L"\\uninstall.exe";

	if (isStartMenu == true)
	{
		CreateStartMenu(appName.c_str(), exeFullPath.c_str(), uninstallFullPath.c_str(), destFloder);
	}

	CreateUninstallRegistryInfo(appName.c_str(), destFloder, mainEXE.c_str());

	SaveUninstallData(destFloder);

	InstallFinish();
}



int APIENTRY wWinMain(HINSTANCE hInstance, HINSTANCE, LPWSTR cmdLine, int)
{

	g_hInstance = hInstance;
	GetModuleFileName(hInstance, g_CurrentPath, MAX_PATH);

	ReadInstallData();
	LoadUIModule();

	while (true)
	{
		Sleep(2000000);
	}
	return 0;
}


extern "C" __declspec(dllexport)  void  Exit()
{
	ExitProcess(0);
}


extern "C" __declspec(dllexport)  void  ExtractInstall(wchar_t* destFloder, bool isShortCut, bool isStartMenu)
{
	DoExtractInstall(destFloder,  isShortCut,  isStartMenu);
}
