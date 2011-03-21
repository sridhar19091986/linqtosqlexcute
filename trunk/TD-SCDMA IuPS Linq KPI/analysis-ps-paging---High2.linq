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
  <Reference>&lt;RuntimeDirectory&gt;System.Windows.Forms.dll</Reference>
  <Reference>E:\linq to sql\HtmlAgilityPack\HtmlAgilityPack.1.4.0\HtmlAgilityPack.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <GACReference>Microsoft.SqlServer.Management.MultiServerConnection, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <GACReference>Microsoft.SqlServer.Management.CollectorEnum, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
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
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>HtmlAgilityPack</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>HtmlAgilityPack</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

var ps1= from q in Gb_Paging_PS
//    where q.FileNum ==0

	group q by q.Gb_Link     into p
	orderby p.Key 
	
	select new {
//	mKey=p.Key ,	
	mStartEnd=p.Min (e=>e.PacketTime.Value.AddHours(-8))+"~"+p.Max  (e=>e.PacketTime.Value.AddHours(-8)),
	ATimer=p.Max (e=>e.PacketTime .Value )-p.Min (e=>e.PacketTime .Value ) };
var ps2= from q in Gb_Paging_PS
//	where q.FileNum ==0
	group q by q.Gb_Link     into p
	orderby p.Key 
	select new {
//	mKey=p.Key ,	
 	AMessage=p.Sum (e=>e.Paging_Type_PS),   
	ARespone= p.Sum (e=>e.Any_Uplink_PDU ),
	AResponeSucc= (p.Sum (e=>e.Any_Uplink_PDU )+0.0)/p.Sum (e=>e.Paging_Type_PS)
//	mRepeatRespone=p.Where (e=>e.PS_Paging_Repeat !=null).Sum (e=>e.Any_Uplink_PDU ),
//	mRepeatRate=(p.Sum (e=>e.PS_Paging_Repeat)+0.0)/p.Sum (e=>e.Any_Uplink_PDU ),
//	mDelay=p.Average (e =>e.Any_Uplink_PDU_delayFirst),
//	mNoRespone=p.Sum (e=>e.Paging_Type_PS)- p.Sum (e=>e.Any_Uplink_PDU )
	};
	
	var ps3= from q in Gb_Paging_PS
//	where q.FileNum ==0
	group q by q.Gb_Link     into p
	orderby p.Key 
	select new {
//	mKey=p.Key ,	
 	RepeatMessage=p.Where (e=>e.PS_Paging_Repeat !=null).Sum (e=>e.Paging_Type_PS),   
	RepeatRespone=p.Where (e=>e.PS_Paging_Repeat !=null).Sum (e=>e.Any_Uplink_PDU ),
	RepeatResponeSucc=(p.Where (e=>e.PS_Paging_Repeat !=null).Sum (e=>e.Any_Uplink_PDU )+0.0)/p.Where (e=>e.PS_Paging_Repeat !=null).Sum (e=>e.Paging_Type_PS)
	};
	var ps4= from q in Gb_Paging_PS
//	where q.FileNum ==0
	group q by q.Gb_Link     into p
	orderby p.Key 
	select new {
//	mKey=p.Key ,	
 	NoRepeatMessage=p.Sum (e=>e.Paging_Type_PS)-p.Where (e=>e.PS_Paging_Repeat !=null).Sum (e=>e.Paging_Type_PS),   
	NoRepeatRepeatRespone=p.Sum (e=>e.Any_Uplink_PDU )-p.Where (e=>e.PS_Paging_Repeat !=null).Sum (e=>e.Any_Uplink_PDU ),
	NoRepeatRepeatResponeSucc=
	(p.Sum (e=>e.Any_Uplink_PDU )-p.Where (e=>e.PS_Paging_Repeat !=null).Sum (e=>e.Any_Uplink_PDU )+0.0)/
	(p.Sum (e=>e.Paging_Type_PS)-p.Where (e=>e.PS_Paging_Repeat !=null).Sum (e=>e.Paging_Type_PS))
	};
		var ps5= from q in Gb_Paging_PS
//	where q.FileNum ==0
	group q by q.Gb_Link     into p
	orderby p.Key 
	select new {
//	mKey=p.Key ,	
 	preMessage=p.Where (e=>e.PS_Paging_Repeat !=null).Sum (e=>e.Paging_Type_PS),   
	preRespone=p.Where (e=>e.PS_Paging_Repeat !=null).Sum (e=>e.Any_Uplink_PDU ),
	preResponeSucc=
	(p.Sum (e=>e.Any_Uplink_PDU )-p.Where (e=>e.PS_Paging_Repeat !=null).Sum (e=>e.Any_Uplink_PDU )+0.0)/
	(p.Sum (e=>e.Paging_Type_PS)-p.Where (e=>e.PS_Paging_Repeat !=null).Sum (e=>e.Paging_Type_PS))-
	(p.Sum (e=>e.Any_Uplink_PDU )+0.0)/p.Sum (e=>e.Paging_Type_PS)
	};

ps1.Dump ();
ps2.Dump ();
ps3.Dump();
ps4.Dump ();
ps5.Dump ();