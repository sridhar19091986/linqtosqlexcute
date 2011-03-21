<Query Kind="Statements">
  <Connection>
    <ID>e5f5449b-aa54-4234-bda6-c0296770953c</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>master</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>


var q=from p in _Gb_ns_traffics
	where p.Tcp_s.Contains("13001")
	select p.Tlli ;
	
var l=from p in _Gb_ns_traffics 
	where q.Contains (p.Tlli )
	select new{p.Http_uri,p.Wsp_uri ,p.Http_host ,p.Http_x_online,p.Ip_s};
	
	l.Dump ();


var m=	from p in _Gb_ns_traffics
	where p.Http_host.Contains("fetion")||p.Http_uri.Contains("fetion")||p.Http_x_online.Contains("fetion")
//	group p by p.Http_host  
select new{ p.Http_uri,p.Wsp_uri ,p.Http_host ,p.Http_x_online,p.Ip_s}; m.Dump ();
	
//	
//	q.Dump ();
//	
//	var q= from p in _Gb_ns_traffics
//	   where p.Tcp_s.Contains("13001")
//	   select p;
//	   
//	   q.Dump ();
//	
//	var categories =
//	from p in _Gb_ns_traffics
//	group p by new
//	{
//		p.CategoryID,
//		p.SupplierID
//	}
//		into g
//		select new
//			{
//				g.Key,
//				g
//			};