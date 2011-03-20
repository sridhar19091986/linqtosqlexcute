<Query Kind="Expression">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

c#文件关联问题(图标)
 悬赏分：0 - 解决时间：2009-1-12 12:01 
请问设置文件关联后，即设置文档默认打开的exe文件之后，如何让该文档显示(.np格式)图标显示成该exe文件的图标。 
showSystemIcon.cs
 1using System;
 2using System.Collections.Generic;
 3using System.ComponentModel;
 4using System.Data;
 5using System.Drawing;
 6using System.Text;
 7using System.Windows.Forms;
 8using System.Runtime.InteropServices;
 9
10namespace RepairTools_2._0
11{
12    class showSystemIcon
13    {
14        public static uint SHGFI_ICON = 0x100;
15        public static uint SHGFI_DISPLAYNAME = 0x200;
16        public static uint SHGFI_TYPENAME = 0x400;
17        public static uint SHGFI_ATTRIBUTES = 0x800;
18        public static uint SHGFI_ICONLOCATION = 0x1000;
19        public static uint SHGFI_EXETYPE = 0x2000;
20        public static uint SHGFI_SYSICONINDEX = 0x4000;
21        public static uint SHGFI_LINKOVERLAY = 0x8000;
22        public static uint SHGFI_SELECTED = 0x10000;
23        public static uint SHGFI_LARGEICON = 0x0;
24        public static uint SHGFI_SMALLICON = 0x1;
25        public static uint SHGFI_OPENICON = 0x2;
26        public static uint SHGFI_SHELLICONSIZE = 0x4;
27        public static uint SHGFI_PIDL = 0x8;
28        public static uint SHGFI_USEFILEATTRIBUTES = 0x10;
29
30        public static uint FILE_ATTRIBUTE_NORMAL = 0x80;
31        public static uint LVM_FIRST = 0x1000;
32        public static uint LVM_SETIMAGELIST = LVM_FIRST + 3;
33        public static uint LVSIL_NORMAL = 0;
34        public static uint LVSIL_SMALL = 1;
35
36        [DllImport("Shell32.dll")]
37        public static extern IntPtr SHGetFileInfo(string pszPath,
38            uint dwFileAttributes, ref SHFILEINFO psfi,
39            int cbfileInfo, uint uFlags);
40
41        public struct SHFILEINFO
42        {
43            public IntPtr hIcon;
44            public int iIcon;
45            public int dwAttributes;
46            public string szDisplayName;
47            public string szTypeName;
48        }
49
50        [DllImport("User32.DLL")]
51        public static extern int SendMessage(IntPtr hWnd,
52            uint Msg, IntPtr wParam, IntPtr lParam);
53
54        public void ListViewSysImages(ListView AListView)
55        {
56            SHFILEINFO vFileInfo = new SHFILEINFO();
57            IntPtr vImageList = SHGetFileInfo("", 0, ref vFileInfo,
58                Marshal.SizeOf(vFileInfo), SHGFI_SHELLICONSIZE |
59                SHGFI_SYSICONINDEX | SHGFI_LARGEICON);
60
61            SendMessage(AListView.Handle, LVM_SETIMAGELIST, (IntPtr)LVSIL_NORMAL,
62                vImageList);
63
64            vImageList = SHGetFileInfo("", 0, ref vFileInfo,
65                Marshal.SizeOf(vFileInfo), SHGFI_SHELLICONSIZE |
66                SHGFI_SYSICONINDEX | SHGFI_SMALLICON);
67            SendMessage(AListView.Handle, LVM_SETIMAGELIST, (IntPtr)LVSIL_SMALL,
68                vImageList);
69        }
70
71        public int FileIconIndex(string AFileName)
72        {
73            SHFILEINFO vFileInfo = new SHFILEINFO();
74            SHGetFileInfo(AFileName, 0, ref vFileInfo,
75                Marshal.SizeOf(vFileInfo), SHGFI_SYSICONINDEX);
76            return vFileInfo.iIcon;
77        }
78    }
79}
80



使用：
把它添加到ListView中
 1private void btnRunSearch_Click(object sender, EventArgs e)
 2        {
 3            try
 4            {
 5                ListViewItem lvi;
 6                ListViewItem.ListViewSubItem lvsi;
 7                showSystemIcon showIcon = new showSystemIcon();
 8
 9                this.lvStartupFileList.Items.Clear();
10                lvStartupFileList.BeginUpdate();
11                foreach (string str in RegistryOperate.strRunRegistry())
12                {
13                    string str1 = RegistryOperate.returnRun().GetValue(str).ToString();
14                    showIcon.ListViewSysImages(lvStartupFileList);
15                    lvi = new ListViewItem();
16                    lvi.Text = str;
17                    lvi.ImageIndex = showIcon.FileIconIndex(str1);
18                    lvsi = new ListViewItem.ListViewSubItem();
19                    lvsi.Text = str1;
20                    lvi.SubItems.Add(lvsi);
21                    lvStartupFileList.Items.Add(lvi);
22                }
23                lvStartupFileList.EndUpdate();
24            }
25            catch
26            {
27                MessageBox.Show(e.ToString());
28            }
29        } 