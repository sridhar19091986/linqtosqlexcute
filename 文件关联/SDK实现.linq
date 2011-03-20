<Query Kind="Expression">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

文件关联(Windows SDK)
 
 
 
如果将一个数据文件与一个可执行文件关联，那么就可以通过双击数据文件来直接执行可执行文件，比如双击以txt为扩展名的文本文件，系统就会自动执行Notepad.exe文件来编辑它，这就是因为txt文件是与Notepad.exe文件关联的。

文件关联是在注册表的HKEY_CLASSES_ROOT根键中设置的。要为某种扩展名设置关联，需要在HKEY_CLASSES_ROOT根键下设置两个子键，第一个子键的名称是“.扩展名”（和数据文件的扩展名相对应），这个子键需要设置默认值，默认值的数据为HKEY_CLASSES_ROOT根键下另一个子键的名称，在这个子键下可以继续设置与这种数据文件关联的可执行文件名。

如果关联的操作方式是“打开”，那么在第二个子键中，可以继续创建“shell\open\ command”子键，并把command子键的默认值设为可执行文件名，这样双击数据文件，就会执行这个可执行文件；如果关联的操作方式是“打印”，那么可以在第二个子键中继续创建“shell\print\command”子键，同样将command子键的默认值设为执行打印操作的可执行文件名。当然，也可以只设置“open”操作的关联。经过这两个步骤，文件关联就设置好了。

以“*.test”数据文件为例，要在HKEY_CLASSES_ROOT根键下创建一个“.test”子键，把它的默认值设置为“testfile”，然后再创建一个“testfile\shell\open\command”子键，并把command键的默认值设置为指定的可执行文件名。

下面这段代码就是将“*.test”数据文件与当前可执行文件进行关联：

// associate.c
// 本程序将设置注册表，把.test文件和本程序关联起来，关联成功后，双击.test文件
// windows 将自动打开本文件
// 要设置一个程序和特定扩展名的文件关联，需要做以下2步
// 1. 在 HKEY_CLASSES_ROOT 下面建立一个以扩展名为键名的子键，比如，你要关联.test文件
//    则需要在它下面建立一个.test的子键，然后建立一个默认的键值，即只有键值没有键名的键
//    这个键值是创建第2个键的键名，比如我们取键名为testfile
// 2. 在 HKEY_CLASSES_ROOT 下面再建立一个子键，这个子键是第1步创建的键名，如testfile
//    如果你要打开一个文件，则在这子键下创建shell\open\command子键，键值为：
//    可执行文件名称 %1
//    
//   创建成功后的结构可能如下：
//   HKEY_CLASSES_ROOT
//         |
//         |---.test (有一个默认的键值 testfile)[默认 = testfile]
//         |
//         |--- testfile
//                |
//                |--shell
//                     |
//                     |--open
//                         |
//                         |--command (有一个默认的键值: 如 c:\associate.exe %1)
//                                     [默认 = c:\associate.exe %1]
#include <Windows.h>
#include "resource.h"

HWND    hWinMain = NULL;

char    szKeyEnter[] = TEXT("testfile");
char    szKeyExt1[] = TEXT(".test");
char    szKeyExt2[] = TEXT("testfile\\shell\\open\\command");
char    szParam[] = TEXT("  \"%1\"");


char    szDelSub1[] = TEXT("testfile\\shell\\open");
char    szDelSub2[] = TEXT("testfile\\shell");


void SetAssociate();
void DelAssociate();
BOOL IsAssociate();

LRESULT CALLBACK DialogProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

int WINAPI WinMain(IN HINSTANCE hInstance, IN HINSTANCE hPrevInstance, IN LPSTR lpCmdLine, IN int nShowCmd )
{
	DialogBoxParam(hInstance, MAKEINTRESOURCE(IDD_MAIN), NULL, DialogProc, 0);
	return 0;
}

void SetAssociate()
{
	HKEY    hKey;
	char    szFileName[MAX_PATH];

	if ( ERROR_SUCCESS == RegCreateKey(HKEY_CLASSES_ROOT, szKeyExt1, &hKey))
	{
		RegSetValueEx(hKey, NULL, 0, REG_SZ, szKeyEnter, sizeof(szKeyEnter));
		RegCloseKey(hKey);
	}

	if ( ERROR_SUCCESS == RegCreateKey(HKEY_CLASSES_ROOT, szKeyExt2, &hKey) )
	{
		GetModuleFileName(NULL, szFileName, sizeof(szFileName));
		lstrcat(szFileName, szParam);
		RegSetValueEx(hKey, NULL, 0, REG_EXPAND_SZ, szFileName, strlen(szFileName) + 1);
		RegCloseKey(hKey);
	}
}

void DelAssociate()
{
	HKEY hKey;
	RegDeleteKey(HKEY_CLASSES_ROOT, szKeyExt1);
	// NT下只能一层一层的删除
	// testfile\\shell\\open\\command
	if ( ERROR_SUCCESS == RegOpenKeyEx(HKEY_CLASSES_ROOT, szDelSub1, 0, KEY_WRITE, &hKey) )
	{
		RegDeleteKey(hKey, "command"); // 删除command这一层
		RegCloseKey(hKey);
	}
	if ( ERROR_SUCCESS == RegOpenKeyEx(HKEY_CLASSES_ROOT, szDelSub2, 0, KEY_WRITE, &hKey))
	{
		RegDeleteKey(hKey, "open"); // 删除open这一层
		RegCloseKey(hKey);
	}
	if ( ERROR_SUCCESS == RegOpenKeyEx(HKEY_CLASSES_ROOT, szKeyEnter, 0, KEY_WRITE, &hKey))
	{
		RegDeleteKey(hKey, "shell"); // 删除shell这一层
		RegCloseKey(hKey);
	}
	RegDeleteKey(HKEY_CLASSES_ROOT, szKeyEnter); // 删除testfile这一层
}

BOOL IsAssociate()
{
	HKEY    hKey;
	
	if ( ERROR_SUCCESS == RegOpenKeyEx(HKEY_CLASSES_ROOT, szKeyExt1, 0, KEY_WRITE, &hKey) )
	{
		RegCloseKey(hKey);
		return TRUE;
	}
	return FALSE;
}

LRESULT CALLBACK DialogProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	switch ( uMsg )
	{
	case WM_CLOSE:
		EndDialog(hWnd, 0);
		break;
	case WM_INITDIALOG:
		hWinMain = hWnd;
		if ( IsAssociate() )
		{
			CheckDlgButton(hWnd, IDC_ASSOCIATE, BST_CHECKED);
		}
		break;
	case WM_COMMAND:
		if ( LOWORD(wParam) == IDC_ASSOCIATE &&
			 HIWORD(wParam) == BN_CLICKED )
		{
			if ( IsDlgButtonChecked(hWinMain, IDC_ASSOCIATE) )
				SetAssociate();
			else
				DelAssociate();
		}
		break;
	default:
		return FALSE;
	}
	return TRUE;
}

// resource.h
#define IDD_MAIN                        101
#define IDC_ASSOCIATE                   1000
#define IDC_STATIC                      -1

// associate.rc
#include "resource.h"
#include "afxres.h"
/////////////////////////////////////////////////////////////////////////////
//
// Dialog
//

IDD_MAIN DIALOG DISCARDABLE  0, 0, 143, 73
STYLE DS_MODALFRAME | WS_POPUP | WS_CAPTION | WS_SYSMENU
CAPTION "文件关联示例"
FONT 10, "宋体"
BEGIN
	CONTROL         "将本程序关联到 .test 文件",IDC_ASSOCIATE,"Button",
					BS_AUTOCHECKBOX | BS_MULTILINE | WS_TABSTOP,7,7,129,18
	LTEXT           "说明：关联之后，当你双击 .test文件的时候，windows会自动打开本程序，取消关联，把前面的勾去掉即可",
					IDC_STATIC,7,31,129,35
END

当程序运行起来之后，你选中复选框之后，就将本程序和.test文件关联起来了。这时候你可以关闭程序，然后随便建立一个空的.test文件，比如a.test，你双击一下这个a.test文件，你发现本程序被启动起来了，然后你取消复选框关闭程序，再双击a.test你发现程序就不会再启动。这是一个简单的关联实现。你可以参照本程序，设计出更复杂的关联，甚至可以将关联的文件图标也改掉。

本程序理论部分来源于罗云彬32位汇编语言一书，源代码也是由汇编语言改写而来。查看该程序的汇编版本，可以参看此书。

author: cnhnyu
e-mail: cnhnyu<AT>gmail<DOT>com
qq: 94483026
   
 
