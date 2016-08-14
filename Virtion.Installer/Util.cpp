#include "StdAfx.h"
#include "Util.h"

#include <shellapi.h>
#include <shlobj.h>
#include <shlwapi.h>


//
// System functions.
//

BOOL IsSystem64Bit()
{
#ifdef _WIN64
	return TRUE;
#else
	BOOL system64 = FALSE;

	typedef BOOL(WINAPI * FPIsWow64Process)(HANDLE hProcess, BOOL* Wow64Process);
	auto isWow64Process = (FPIsWow64Process)GetProcAddress(
		GetModuleHandle(L"kernel32"), "IsWow64Process");
	if (isWow64Process)
	{
		isWow64Process(GetCurrentProcess(), &system64);
	}

	return system64;
#endif
}

//
// Process functions.
//

BOOL IsProcessUserAdmin()
{
	BOOL runningAsAdmin = FALSE;

	// Allocate and initialize a SID of the administrators group.
	PSID adminGroupSid = nullptr;
	SID_IDENTIFIER_AUTHORITY NtAuthority = { SECURITY_NT_AUTHORITY };
	if (AllocateAndInitializeSid(
		&NtAuthority,
		2,
		SECURITY_BUILTIN_DOMAIN_RID,
		DOMAIN_ALIAS_RID_ADMINS,
		0, 0, 0, 0, 0, 0,
		&adminGroupSid))
	{
		// Check if the primary access token of the process has the admin group SID.
		if (!CheckTokenMembership(nullptr, adminGroupSid, &runningAsAdmin))
		{
			runningAsAdmin = TRUE;
		}

		FreeSid(adminGroupSid);
		adminGroupSid = nullptr;
	}

	return runningAsAdmin;
}




bool CreateShortcutFile(const WCHAR* filePath, const WCHAR* targetPath)
{
	IShellLink* psl;
	HRESULT hr = CoCreateInstance(CLSID_ShellLink, nullptr, CLSCTX_INPROC_SERVER, IID_IShellLink, (void**)&psl);
	if (SUCCEEDED(hr))
	{
		IPersistFile* ppf;
		hr = psl->QueryInterface(IID_IPersistFile, (void**)&ppf);
		if (SUCCEEDED(hr))
		{
			psl->SetPath(filePath);
			hr = ppf->Save(targetPath, TRUE);

			ppf->Release();
		}

		psl->Release();
	}

	return SUCCEEDED(hr);
}

bool IsDirectoryWritable()
{
	return TRUE;
}

//
// Registry functions.
//

bool GetRegistryDword(HKEY rootKey, const WCHAR* subKey, const WCHAR* value, DWORD* data)
{
	DWORD type;
	DWORD dataSize = sizeof(DWORD);
	return (SHGetValue(rootKey, subKey, value, &type, data, &dataSize) == ERROR_SUCCESS &&
		type == REG_DWORD);
}

bool GetRegistryString(HKEY rootKey, const WCHAR* subKey, const WCHAR* value, WCHAR* data, DWORD bufferLength)
{
	DWORD type;
	DWORD dataSize = bufferLength * sizeof(data[0]);
	return (SHGetValue(rootKey, subKey, value, &type, data, &dataSize) == ERROR_SUCCESS &&
		type == REG_SZ);
}

bool SetRegistryData(DWORD type, HKEY rootKey, const WCHAR* subKey, const WCHAR* value, BYTE* data, DWORD dataSize)
{
	bool result = FALSE;
	HKEY regKey;
	if (RegCreateKeyEx(rootKey, subKey, 0, 0, 0, KEY_SET_VALUE, nullptr, &regKey, nullptr) == ERROR_SUCCESS)
	{
		if (RegSetValueEx(regKey, value, 0, type, data, dataSize) == ERROR_SUCCESS)
		{
			result = TRUE;
		}

		RegCloseKey(regKey);
	}

	return result;
}

bool SetRegistryDword(HKEY rootKey, const WCHAR* subKey, const WCHAR* value, DWORD data)
{
	return SetRegistryData(REG_DWORD, rootKey, subKey, value, (BYTE*)&data, sizeof(DWORD));
}

bool SetRegistryString(HKEY rootKey, const WCHAR* subKey, const WCHAR* value, const WCHAR* data)
{
	size_t dataSize = (wcslen(data) + 1) * sizeof(WCHAR);
	return SetRegistryData(REG_SZ, rootKey, subKey, value, (BYTE*)data, (DWORD)dataSize);
}


bool DeleteRegistryKey(HKEY rootKey, const WCHAR* subKey, const WCHAR* delKey)
{
	HKEY hKey;
	if (ERROR_SUCCESS == ::RegOpenKeyEx(rootKey, subKey, 0, KEY_SET_VALUE, &hKey))
	{
		if (ERROR_SUCCESS != RegDeleteKey(hKey, delKey))
		{
			RegCloseKey(hKey);
			return false;
		}
	}
	RegCloseKey(hKey);
	return true;
}

bool IsExistRegistryKey(HKEY rootKey, const WCHAR* subKey)
{
	HKEY hKey;
	if (ERROR_SUCCESS == ::RegOpenKeyEx(rootKey, subKey, 0, KEY_QUERY_VALUE, &hKey))
	{
		return true;
	}
	return false;
}

/*
[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\ек╧Ц]
"DisplayName"="ек╧Ц"
"DisplayVersion"="1.301"
"Publisher"=""
"HelpLink"=""
"DisplayIcon"="I:\\Desktop\PanguanProject\\PanguangProject.exe"
"UninstallString"="I:\\Desktop\\PanguanProject\\PanguangProject.exe"
"InstallLocation"="I:\\Desktop\\PanguanProject\\"
*/

bool CreateUninstallRegistryInfo(const wchar_t* displayName, const wchar_t* dir, const wchar_t* exe)
{
	wchar_t subKey[MAX_PATH] = L"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\";
	wcscat_s(subKey, displayName);
	SetRegistryString(HKEY_LOCAL_MACHINE, subKey, L"DisplayName", displayName);

	wchar_t displayIcon[MAX_PATH] = {};
	wcscpy_s(displayIcon, dir);
	wcscat_s(displayIcon, L"\\");
	wcscat_s(displayIcon, exe);
	SetRegistryString(HKEY_LOCAL_MACHINE, subKey, L"DisplayIcon", displayIcon);

	SetRegistryString(HKEY_LOCAL_MACHINE, subKey, L"InstallLocation", dir);

	wchar_t uninstallString[MAX_PATH] = {};
	wcscpy_s(uninstallString, dir);
	wcscat_s(uninstallString, L"\\uninstall.exe");
	SetRegistryString(HKEY_LOCAL_MACHINE, subKey, L"UninstallString", uninstallString);


	SetRegistryString(HKEY_LOCAL_MACHINE, subKey, L"Publisher", L"Virtion.ZT");

	return true;
}