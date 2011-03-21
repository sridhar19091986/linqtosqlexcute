<Query Kind="Program">
  <Connection>
    <ID>114e7c14-f3fa-4b35-9a8b-364c9e7f21bf</ID>
    <Server>localhost</Server>
    <Persist>true</Persist>
    <Database>fcms_23A_Gb_21</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.Dialog.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;System.Windows.Forms.dll</Reference>
  <GACReference>Microsoft.SqlServer.Management.MultiServerConnection, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <GACReference>Microsoft.SqlServer.Management.CollectorEnum, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

//PS寻呼业务定位的2种linq方法，
//1.在PS寻呼和HTTP GET/POST之间即小集合直接做连接处理。这种方法分解了查询大集合的问题，体现分而治之。
//2.在Gb所有消息即大集合中做处理,这种方法存在查询效率的问题。
void Main()
{
//	var m=get(10000);
	var m=getNew();
	m.Dump ();	      
}
private IEnumerable<Gb_Post2x> getNew()
{
	foreach(var p in Gb_Paging_PS_Services )   //PS寻呼集合
	{
	var mes=from q in Gb_Post2xes      //HTTP POST集合
	         where  q.URI_Main!=null && q.TLLI  == p.TLLI   && q.PacketNum <p.PacketNum
			 orderby q.PacketNum descending
			 select q;
	var firstMess=mes.FirstOrDefault();
	var secondMess=mes.Skip (1).FirstOrDefault();
	firstMess.Dump ();
	yield return firstMess;
	yield return secondMess;
	}
}

// Define other methods and classes here
private IEnumerable<_Gb_ns_traffic> get(int max)
{
	var pg=from p in _Gb_ns_traffics
			where p.Ns_traffic_MsgType=="BSSGP.PAGING-PS" && p.PacketNum <max
			select p;
	foreach(var p in pg )
	{
	var mes=from q in _Gb_ns_traffics 
	         where  q.Http_uri !=null && q.Tlli == p.Tlli  && q.PacketNum <p.PacketNum
			 orderby q.PacketNum descending
			 select q;
	var firstMess=mes.FirstOrDefault();
	var secondMess=mes.Skip (1).FirstOrDefault();
	yield return firstMess;
	yield return secondMess;
	}
}
