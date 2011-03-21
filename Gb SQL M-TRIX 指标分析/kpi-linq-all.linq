<Query Kind="Statements">
  <Connection>
    <ID>80d6b436-1f57-4aef-992f-652eb2a986cb</ID>
    <Server>192.168.1.230</Server>
    <Persist>true</Persist>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAVn6xNHxDxUmkwNAvq5q6/wAAAAACAAAAAAADZgAAqAAAABAAAAAYCm0KMNXj+W/icNxdmCGJAAAAAASAAACgAAAAEAAAAFzaekbF/eQEqpeMkdPhkJ4IAAAA4EHAwra6d3gUAAAAz0MEOj13JGdmUSiNGV3P3IINa6w=</Password>
    <Database>gb_23A_20100521_11</Database>
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

	var ps= from p in Gb_Paging_PS
	where p.PS_Paging_Repeat==null
	group p by p.FileNum into psbyfilenum
	orderby psbyfilenum.Key 
	select new {
	mFile=psbyfilenum.Key ,
	mTime=Gb_Paging_PS.Where (e=>e.FileNum ==psbyfilenum.Key).Select(e=>e.PacketTime).Min (),
	mName="Paging_TypePS_AnyUplinkPDU",
	mMessage= Gb_Paging_PS.Where (e=>e.FileNum ==psbyfilenum.Key).Where (e=>e.Any_Uplink_PDU!=null).Select(e=>e.Paging_Type_PS).Sum (),
	mSuccess= (Gb_Paging_PS.Where (e=>e.FileNum ==psbyfilenum.Key).Where (e=>e.Any_Uplink_PDU!=null).Select(e=>e.Paging_Type_PS).Sum ()+0.0)/Gb_Paging_PS.Where (e=>e.FileNum ==psbyfilenum.Key).Select(e=>e.Paging_Type_PS).Sum (),
	mDelay= Gb_Paging_PS.Where (e=>e.FileNum ==psbyfilenum.Key).Where (e=>e.Any_Uplink_PDU!=null).Select(e =>e.Any_Uplink_PDU_delayFirst).Average ()
	};
	ps.Dump ();
	
	