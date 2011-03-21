<Query Kind="Program">
  <Connection>
    <ID>27d89e94-3492-4017-9db8-6f859e59aa6c</ID>
    <Server>localhost</Server>
    <Persist>true</Persist>
    <Database>IP_Stream</Database>
    <ShowServer>true</ShowServer>
  </Connection>
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

void Main()
		{
			var m = from q in get()
					group q by q.web into a
					select new
					{
						b = a.Key,
//						d=a.Select (e=>e.uri ),
						c = a.Count(),
					};
					
			m.OrderByDescending (e=>e.c).Dump ();
		}
		public class ipWeb
		{
			public decimal? id;
			public string uri;
			public string port;
			public string web;
		}
		public IEnumerable<ipWeb> get()
		{
		Hashtable hs=uriKey();
			var message = MLocatingTypes.Where (e=>e.UriType !=null);
			foreach (var m in message)
			{
				string s = m.UriType ;
				string[] sArray = s.Split(new char[3]{'/','.','-'});
				foreach (string i in sArray){
						IEnumerable<char> stringQuery =
						   from ch in i
						   where Char.IsLetter(ch)
						   select ch;
						  StringBuilder sb = new StringBuilder();
						foreach (char c in stringQuery)
							sb.Append(c);
					if (sb.Length  > 1)
					if(!hs.Contains(sb.ToString()))
					{
						ipWeb a = new ipWeb();
						a.id = m.MLocatingType_id;
                        a.uri =s;
						a.port = m.PortType;
						a.web = sb.ToString();
						yield return a;
					}}
			}
		}
		public Hashtable uriKey()
		{
		Hashtable hs=new Hashtable();
		hs.Add ("com",0);
hs.Add ("http",1);
		hs.Add ("cn",2);
		hs.Add ("wap",3);
		hs.Add ("blog",4);
hs.Add ("info",5);
hs.Add ("gif",6);
hs.Add ("net",7);
		hs.Add ("app",8);
	hs.Add ("index",9);
	hs.Add ("images",10);
	hs.Add ("jpg",11);
	hs.Add ("news",12);
	hs.Add ("png",13);
	hs.Add ("exe",14);
	hs.Add ("do",15);
	hs.Add ("api",16);
	hs.Add ("aspx",17);
	return hs;
		}
		