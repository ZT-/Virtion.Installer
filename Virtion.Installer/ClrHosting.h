#pragma once

class ClrHosting
{
public:
	ClrHosting()
	{
	}

	~ClrHosting()
	{
		this->DestroyClrHosting();
	}

	bool  InitClrHosting(const wchar_t*  pwzAssemblyPath, const wchar_t* pwzTypeName);
	bool  DestroyClrHosting();
	bool  Execute(const wchar_t* pwzTypeName, const wchar_t* pwzMethodName, const wchar_t* pwzArgument);

public:
	wchar_t pwzAssemblyPath[256];


};


