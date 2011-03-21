<Query Kind="Statements">
  <Connection>
    <ID>47a1fe48-7cd1-403f-9bdf-82ec1f237cab</ID>
    <Server>localhost</Server>
    <Persist>true</Persist>
    <Database>RNC681_linq</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.Dialog.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;System.Windows.Forms.dll</Reference>
  <Reference>E:\linq to sql\HtmlAgilityPack\HtmlAgilityPack.1.4.0\HtmlAgilityPack.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.Dialog.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Reference>E:\linq to sql\HtmlAgilityPack\HtmlAgilityPack.1.4.0\HtmlAgilityPack.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <GACReference>Microsoft.SqlServer.Management.MultiServerConnection, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <GACReference>Microsoft.SqlServer.Management.CollectorEnum, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
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
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>HtmlAgilityPack</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

Stream 和 byte[] 之间的转换

/* - - - - - - - - - - - - - - - - - - - - - - - - 
 * Stream 和 byte[] 之间的转换
 * - - - - - - - - - - - - - - - - - - - - - - - */
/// <summary>
/// 将 Stream 转成 byte[]
/// </summary>
public byte[] StreamToBytes(Stream stream)
{
	byte[] bytes = new byte[stream.Length];
	stream.Read(bytes, 0, bytes.Length);

	// 设置当前流的位置为流的开始
	stream.Seek(0, SeekOrigin.Begin);
	return bytes;
}

/// <summary>
/// 将 byte[] 转成 Stream
/// </summary>
public Stream BytesToStream(byte[] bytes)
{
	Stream stream = new MemoryStream(bytes);
	return stream;
}


/* - - - - - - - - - - - - - - - - - - - - - - - - 
 * Stream 和 文件之间的转换
 * - - - - - - - - - - - - - - - - - - - - - - - */
/// <summary>
/// 将 Stream 写入文件
/// </summary>
public void StreamToFile(Stream stream,string fileName)
{
	// 把 Stream 转换成 byte[]
	byte[] bytes = new byte[stream.Length];
	stream.Read(bytes, 0, bytes.Length);
	// 设置当前流的位置为流的开始
	stream.Seek(0, SeekOrigin.Begin);

	// 把 byte[] 写入文件
	FileStream fs = new FileStream(fileName, FileMode.Create);
	BinaryWriter bw = new BinaryWriter(fs);
	bw.Write(bytes);
	bw.Close();
	fs.Close();
}

/// <summary>
/// 从文件读取 Stream
/// </summary>
public Stream FileToStream(string fileName)
{            
	// 打开文件
	FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
	// 读取文件的 byte[]
	byte[] bytes = new byte[fileStream.Length];
	fileStream.Read(bytes, 0, bytes.Length);
	fileStream.Close();
	// 把 byte[] 转换成 Stream
	Stream stream = new MemoryStream(bytes);
	return stream;
}