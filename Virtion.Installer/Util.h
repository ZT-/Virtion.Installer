#ifndef RM_INSTALLER_UTIL_H_
#define RM_INSTALLER_UTIL_H_


BOOL IsSystem64Bit();

BOOL IsProcessUserAdmin();
BOOL CanProcessUserElevate();

BOOL CopyDirectory(const WCHAR* fromPath, const WCHAR* toPath);
bool CreateShortcutFile(const WCHAR* filePath, const WCHAR* targetPath);

bool GetRegistryDword(HKEY rootKey, const WCHAR* subKey, const WCHAR* value, DWORD* data);
bool GetRegistryString(HKEY rootKey, const WCHAR* subKey, const WCHAR* value, WCHAR* data, DWORD dataSize);
bool SetRegistryDword(HKEY rootKey, const WCHAR* subKey, const WCHAR* value, DWORD data);
bool SetRegistryString(HKEY rootKey, const WCHAR* subKey, const WCHAR* value, const WCHAR* data);


bool DownloadFile(const WCHAR* url, const WCHAR* file);


#endif