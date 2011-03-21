<Query Kind="Program">
  <Connection>
    <ID>e5f5449b-aa54-4234-bda6-c0296770953c</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>kpi_23A</Database>
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
						c = a.Count(),
					};
					
			m.OrderByDescending (e=>e.c).Dump ();
		}
		public class ipWeb
		{
			public int? id;
			public string ip;
			public string port;
			public string web;
		}
		public IEnumerable<ipWeb> get()
		{
			var message = Gb_Post1xes;
			foreach (var m in message)
			{
				string s = m.URI;
				string[] sArray = s.Split(new char[2]{'/','.'});
				foreach (string i in sArray){
						IEnumerable<char> stringQuery =
						   from ch in i
						   where Char.IsLetter(ch)
						   select ch;
						  StringBuilder sb = new StringBuilder();
						foreach (char c in stringQuery)
							sb.Append(c);
					if (sb.Length  > 1)
					{
						ipWeb a = new ipWeb();
						a.id = m.PacketNum;
						a.ip = m.Dest_IP;
						a.port = m.Dest_Port;
						a.web = sb.ToString();
						yield return a;
					}}
			}
		}