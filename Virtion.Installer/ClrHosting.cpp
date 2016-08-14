#include "ClrHosting.h"

#include <SDKDDKVer.h>  

#include <stdio.h>  
#include <tchar.h>  
#include <windows.h>  

#include <metahost.h>  
#include <mscoree.h>

#pragma comment(lib, "mscoree.lib")  

ICLRMetaHost        *pMetaHost = nullptr;
ICLRMetaHostPolicy  *pMetaHostPolicy = nullptr;
ICLRRuntimeHost     *pRuntimeHost = nullptr;
ICLRRuntimeInfo     *pRuntimeInfo = nullptr;


bool  ClrHosting::InitClrHosting(const wchar_t*  pwzAssemblyPath, const wchar_t* pwzTypeName)
{
	wcscpy_s(this->pwzAssemblyPath, pwzAssemblyPath);

	HRESULT hr = CLRCreateInstance(CLSID_CLRMetaHost, IID_ICLRMetaHost, (LPVOID*)&pMetaHost);
	if (FAILED(hr))
	{
		MessageBox(0, L"CLRCreateInstance", L"Error", 0);
		return false;
	}

	hr = pMetaHost->GetRuntime(L"v4.0.30319", IID_PPV_ARGS(&pRuntimeInfo));
	if (FAILED(hr))
	{
		MessageBox(0, L"CLR GetRuntime", L"Error", 0);
		return false;
	}

	hr = pRuntimeInfo->GetInterface(CLSID_CLRRuntimeHost, IID_PPV_ARGS(&pRuntimeHost));
	if (FAILED(hr))
	{
		MessageBox(0, L"CLR GetInterface", L"Error", 0);
		return false;
	}

	hr = pRuntimeHost->Start();
	if (FAILED(hr))
	{
		MessageBox(0, L"CLR Start", L"Error", 0);
		return false;
	}

	DWORD dwRet = 0;
	hr = pRuntimeHost->ExecuteInDefaultAppDomain(this->pwzAssemblyPath, pwzTypeName,
		L"Start",
		this->pwzAssemblyPath,
		&dwRet);
	if (FAILED(hr))
	{
		MessageBox(0, L"CLR ExecuteInDefaultAppDomain", L"Error", 0);
		return false;
	}

	Sleep(2000);
	return true;
}


bool ClrHosting::Execute(const wchar_t* pwzTypeName, const wchar_t* pwzMethodName, const wchar_t* pwzArgument)
{
	DWORD dwRet = 0;

	if (pRuntimeHost == nullptr)
	{
		return false;
	}

	HRESULT hr = pRuntimeHost->ExecuteInDefaultAppDomain(this->pwzAssemblyPath,
		pwzTypeName, pwzMethodName, pwzArgument,
		&dwRet);

	if (FAILED(hr))
	{
		MessageBox(0, L"CLR ExecuteInDefaultAppDomain", L"Error", 0);
		return false;
	}
	return true;
}


bool  ClrHosting::DestroyClrHosting()
{
	HRESULT hr = pRuntimeHost->Stop();
	if (FAILED(hr))
	{
		MessageBox(0, L"CLR Stop", L"Error", 0);
		return false;
	}

	if (pRuntimeInfo != nullptr)
	{
		pRuntimeInfo->Release();
		pRuntimeInfo = nullptr;
	}

	if (pRuntimeHost != nullptr)
	{
		pRuntimeHost->Release();
		pRuntimeHost = nullptr;
	}

	if (pMetaHost != nullptr)
	{
		pMetaHost->Release();
		pMetaHost = nullptr;
	}

	return true;
}