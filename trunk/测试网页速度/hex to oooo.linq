<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.Dialog.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;System.Windows.Forms.dll</Reference>
  <Reference>E:\linq to sql\HtmlAgilityPack\HtmlAgilityPack.1.4.0\HtmlAgilityPack.dll</Reference>
  <GACReference>Microsoft.SqlServer.Management.MultiServerConnection, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <GACReference>Microsoft.SqlServer.Management.CollectorEnum, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>HtmlAgilityPack</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Security</Namespace>
  <Namespace>System.Security.Permissions</Namespace>
  <Namespace>System.Security.Principal</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

 static void Main()
		{
			String decodedString = DecodedString(@"\u2344\u5435\u9876");//将字节数组解码
			Console.WriteLine("Decoded String:" + decodedString); //解码结果:?吵顶
			Console.ReadLine();
		}

		//单字符转换
		private static ushort GetByte(char ch)
		{
			ushort rtnNum = 0;
			switch (ch)
			{
				case 'a':
				case 'A': rtnNum = 10; break;
				case 'b':
				case 'B': rtnNum = 11; break;
				case 'c':
				case 'C': rtnNum = 12; break;
				case 'd':
				case 'D': rtnNum = 13; break;
				case 'e':
				case 'E': rtnNum = 14; break;
				case 'f':
				case 'F': rtnNum = 15; break;
				default: rtnNum = ushort.Parse(ch.ToString()); break;

			}
			return rtnNum;
		}
		
		/// <summary>
		/// 转换一个字符
		/// </summary>
		/// <param name="unicodeSingle"></param>
		/// <returns></returns>
		private  static string ConvertSingle(string unicodeSingle)
		{
			if (unicodeSingle.Length!=4) 
				return null ;
			  byte[] unicodeBytes = new byte[2]{ 0, 0 };
			  for (int i = 0; i < 4; i++)
			  {
				  switch (i)
				  {
					  case 0: unicodeBytes[1] += (byte)(GetByte(unicodeSingle[i]) * 16); break;
					  case 1: unicodeBytes[1] += (byte)GetByte(unicodeSingle[i]); break;
					  case 2: unicodeBytes[0] += (byte)(GetByte(unicodeSingle[i]) * 16); break;
					  case 3: unicodeBytes[0] += (byte)GetByte(unicodeSingle[i]); break;
				  }
			  }
			  return Encoding.Unicode.GetString(unicodeBytes);
		}
		private static String DecodedString(String str)
		{

			String[] aStr = str.Split(new String []{ @"\u"},StringSplitOptions.RemoveEmptyEntries);
			StringBuilder sb = new StringBuilder();
			if (aStr.Length > 0)
			{
				for (int i = 0; i < aStr.Length; i++)
				{
					sb.Append(ConvertSingle(aStr[i]));
				}
			}
			return sb.ToString();
		}
	
