#include "stdafx.h"


extern "C" {
#include "lzma/7zAlloc.h"
#include "lzma/7zFile.h"
#include "lzma/7zVersion.h"
#include "lzma/LzmaDec.h"
#include "lzma/7zCrc.h"
#include "lzma/7z.h"
#include "lzma/7zAlloc.h"
#include "lzma/7zMemInStream.h"
}  // extern "C"

#include <Shlwapi.h>


void SetInstallPercent(int value);

void ExtractPayload(const void* payload, size_t payloadSize)
{
	CMemInStream memStream;
	MemInStream_Init(&memStream, payload, payloadSize);

	CrcGenerateTable();

	ISzAlloc alloc = { SzAlloc, SzFree };
	CSzArEx db;
	SzArEx_Init(&db);
	SRes res = SzArEx_Open(&db, &memStream.s, &alloc, &alloc);
	if (res != SZ_OK)
	{
		SzArEx_Free(&db, &alloc);
		return;
	}

	WCHAR buffer[MAX_PATH];
	UInt32 blockIndex = 0xFFFFFFFF;
	Byte* outBuffer = 0;
	size_t outBufferSize = 0;

	TRACE(_T("Total  Size=<%d>\n"), payloadSize);


	for (UInt32 i = 0; i < db.db.NumFiles; i++)
	{
		SetInstallPercent((i * 100) / (db.db.NumFiles));

		size_t offset = 0;
		size_t outSizeProcessed = 0;
		const CSzFileItem* f = db.db.Files + i;

		SzArEx_GetFileNameUtf16(&db, i, (UInt16*)buffer);

		WCHAR* destPath = buffer;

		if (!f->IsDir)
		{
			res = SzArEx_Extract(
				&db, &memStream.s, i, &blockIndex, &outBuffer, &outBufferSize, &offset,
				&outSizeProcessed, &alloc, &alloc);
			if (res != SZ_OK)
			{
				break;
			}
		}

		//TRACE(_T("Excract file_no out_size=<%d %d>\n"), i, offset);

		for (size_t j = 0; destPath[j] != L'\0'; ++j)
		{
			if (destPath[j] == L'/')
			{
				destPath[j] = L'\0';
				CreateDirectory(destPath, NULL);
				destPath[j] = CHAR_PATH_SEPARATOR;
			}
		}

		if (f->IsDir)
		{
			CreateDirectory(destPath, NULL);
			continue;
		}

		CSzFile outFile;
		if (OutFile_OpenW(&outFile, destPath))
		{
			res = SZ_ERROR_FAIL;
			break;
		}
		size_t processedSize = outSizeProcessed;
		if (File_Write(&outFile, outBuffer + offset, &processedSize) != 0 ||
			processedSize != outSizeProcessed)
		{
			res = SZ_ERROR_FAIL;
			break;
		}
		if (File_Close(&outFile))
		{
			res = SZ_ERROR_FAIL;
			break;
		}

		if (f->AttribDefined) SetFileAttributesW(destPath, f->Attrib);
	}

	IAlloc_Free(&alloc, outBuffer);
	SzArEx_Free(&db, &alloc);

	if (res == SZ_OK)
	{
		// Success.
	}
}

