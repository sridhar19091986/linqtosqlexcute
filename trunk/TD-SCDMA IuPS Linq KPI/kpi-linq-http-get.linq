<Query Kind="Statements">
  <Connection>
    <ID>80d6b436-1f57-4aef-992f-652eb2a986cb</ID>
    <Server>192.168.1.230</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAVn6xNHxDxUmkwNAvq5q6/wAAAAACAAAAAAADZgAAqAAAABAAAAAYCm0KMNXj+W/icNxdmCGJAAAAAASAAACgAAAAEAAAAFzaekbF/eQEqpeMkdPhkJ4IAAAA4EHAwra6d3gUAAAAz0MEOj13JGdmUSiNGV3P3IINa6w=</Password>
    <Database>gb_23A_20100329_11</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.Dialog.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;System.Windows.Forms.dll</Reference>
  <Reference>E:\linq to sql\HtmlAgilityPack\HtmlAgilityPack.1.4.0\HtmlAgilityPack.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <GACReference>Microsoft.SqlServer.Management.MultiServerConnection, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <GACReference>Microsoft.SqlServer.Management.CollectorEnum, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>HtmlAgilityPack</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

var ps= from p in Gb_Get2xes
	group p by p.FileNum into psbyfilenum
	orderby psbyfilenum.Key 
	select new {
	mFile=psbyfilenum.Key ,
	//	mTime=psbyfilenum.Where (e=>e.FileNum ==psbyfilenum.Key).Select(e=>e.PacketTime.Value.AddHours(8)).Min (),
		mTime=psbyfilenum.Where (e=>e.FileNum ==psbyfilenum.Key).Select(e=>e.PacketTime.Value.AddHours(-8)).Min (),
	mName="HTTP_Get_Response",
	mMessage=psbyfilenum.Where (e=>e.FileNum ==psbyfilenum.Key).Where (e=>e.Response!=null).Select(e=>e.Response ).Sum (),
	mSuccess= (psbyfilenum.Where (e=>e.FileNum ==psbyfilenum.Key).Where (e=>e.Response!=null).Select(e=>e.Response ).Sum ()+0.0)/psbyfilenum.Where (e=>e.FileNum ==psbyfilenum.Key).Select(e=>e.Get2x).Sum (),
	mDelay= psbyfilenum.Where (e=>e.FileNum ==psbyfilenum.Key).Where (e=>e.Response!=null).Select(e =>e.Response_delayFirst).Average ()
	};
	ps.Dump ();