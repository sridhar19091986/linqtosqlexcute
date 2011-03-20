<Query Kind="Expression">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

Home Content RSS Log in Code Green SoftwareSearch for:   Home About C#: Set File Type Association
April 14th, 2009 by Mel Leave a reply » While searching for some simple C# code to set the program and icon associated with a file type in Windows I ran across these beautiful functions that illustrate how to accomplish this by using the registry.

All credit goes to cristiscu (source).

Here’s the code:

using Microsoft.Win32;
using System.Runtime.InteropServices;
 
public class FileAssociation
{
	// Associate file extension with progID, description, icon and application
	public static void Associate(string extension, 
		   string progID, string description, string icon, string application)
	{
		Registry.ClassesRoot.CreateSubKey(extension).SetValue("", progID);
		if (progID != null && progID.Length > 0)
			using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(progID))
			{
				if (description != null)
					key.SetValue("", description);
				if (icon != null)
					key.CreateSubKey("DefaultIcon").SetValue("", ToShortPathName(icon));
				if (application != null)
					key.CreateSubKey(@"Shell\Open\Command").SetValue("", 
								ToShortPathName(application) + " \"%1\"");
			}
	}
 
	// Return true if extension already associated in registry
	public static bool IsAssociated(string extension)
	{
		return (Registry.ClassesRoot.OpenSubKey(extension, false) != null);
	}
 
	[DllImport("Kernel32.dll")]
	private static extern uint GetShortPathName(string lpszLongPath, 
		[Out] StringBuilder lpszShortPath, uint cchBuffer);
 
	// Return short path format of a file name
	private static string ToShortPathName(string longName)
	{
		StringBuilder s = new StringBuilder(1000);
		uint iSize = (uint)s.Capacity;
		uint iRet = GetShortPathName(longName, s, iSize);
		return s.ToString();
	}
}
And here’s how you’d use it:

if (!FileAssociation.IsAssociated(".ext"))
   Associate(".ext", "ClassID.ProgID", "ext File", "YourIcon.ico", "YourApplication.exe");
A couple caveats to this code that I’ve noticed:

The second parameter passed to Associate() can be any string you’d like to use to represent your program in the registry, just make sure you’re consistent with it. 



ToShortPathName() will return an empty string if the file path you pass it doesn’t exist, i.e. if you pass it a path to an icon file that doesn’t exist it won’t work. 



IsAssociated() will return true if the file extension is associated with any program, not just yours. If you want to be specific about it checking whether it’s associated with your program, then just check whether it’s default key is set to your programs ProgID. 



Previous Entry: WCF: Net.Tcp Binding on all IP Addresses 
Next Entry: WPF: Vista Blue Highlight Brush 
Posted in C#, Computers, Software 

Tags: C# FileType Registry

You can follow any responses to this entry through the RSS 2.0 Feed. You can leave a response , or trackback from your own site. 

Advertisement 
6 commentsAdd your comment 
 
china法宝
 says: 
November 3, 2009 at 10:53 pm非常好。

Reply 
 
Darth Reisender
 says: 
October 6, 2009 at 8:36 pmPromising, but… when I have my file type associated, how do I open it within my code, I mean, how does my app know that there is a file waiting to be opened?

Reply 
 
Mel
 says: 
October 6, 2009 at 8:43 pmHey Darth,

If you’d like you’re application to handle files when it’s opened, such as if the user double-clicks on a file that you’ve associated with your program, you need to check for the path of the file in your program’s command-line arguments. Like so:

string[] arguments = Environment.GetCommandLineArgs();
if (arguments.Length > 1)
{
	string filePath = arguments[1];
}
If you’d like to loop through all the files that might be sent to your program you can do it like this:

string[] arguments = Environment.GetCommandLineArgs();
for (int i = 1; i < arguments.Length; i++)
{
	string filePath = arguments[i];
}
Reply 
 
Michael
 says: 
May 18, 2010 at 7:05 amIf you are using ClickOnce:
use AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData
(null if no arguments)
instead of

Environment.GetCommandLineArgs()Reply 
 
Mel
 says: 
May 18, 2010 at 9:59 amMey Michael, thanks for the tip!

Reply 
 
haha lol
 says: 
October 2, 2009 at 7:14 pmkool

Reply 
Leave a Reply
Click here to cancel reply. 
 Name (required)
 Mail (will not be published) (required)
 Website
		   
		  
 
 
Path:  
 
   
My Latest Posts
OfficeToXps: Convert Word, Excel, and PowerPoint to XPS 
WPF: Time Control 12 Hour Format 
ProgressStream: A Stream with Read and Write events 
Call of Duty: Modern Warfare 2 Multiple Profiles 
WPF: Simple Vista Image Viewer 
WPF: CheckBox as GroupBox Header 
WPF: Creating a Pop Up Button Style 
WPF: Setting a Type Specific Property Value 
WPF: Bind Control Enabled to Checkbox Checked 
WPF: Center Child Window 

Previous Entry
 WCF: Net.Tcp Binding on all IP Addresses
 
Next Entry
 WPF: Vista Blue Highlight Brush
 
About
Change this text in the admin section of WordPress 
Back to Top 
© 2010 Code Green Software · Proudly powered by WordPress & Green Park 2 by Cordobo. 

Valid XHTML 1.0 Transitional | Valid CSS 3 

 
