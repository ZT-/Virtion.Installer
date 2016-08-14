/*
  Copyright (C) 2013 Birunthan Mohanathas

  This program is free software; you can redistribute it and/or
  modify it under the terms of the GNU General Public License
  as published by the Free Software Foundation; either version 2
  of the License, or (at your option) any later version.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program; if not, write to the Free Software
  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

#ifndef RM_INSTALLER_INSTALL_H_
#define RM_INSTALLER_INSTALL_H_

enum InstallType : char
{
	Standard,
	Portable
};

enum InstallArch : char
{
	X32,
	X64
};

struct InstallOptions
{
	WCHAR targetPath[MAX_PATH];
	InstallType type;
	InstallArch arch;
	LCID language;
	BOOL launchOnLogin;
};

void DoInstall(InstallOptions& options);

#endif