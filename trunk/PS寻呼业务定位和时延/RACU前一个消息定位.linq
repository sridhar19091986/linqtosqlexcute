<Query Kind="Program">
  <Connection>
    <ID>80d6b436-1f57-4aef-992f-652eb2a986cb</ID>
    <Server>192.168.1.230</Server>
    <Persist>true</Persist>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAVn6xNHxDxUmkwNAvq5q6/wAAAAACAAAAAAADZgAAqAAAABAAAAB18CgpZ+YIQnFmLu/BBmKXAAAAAASAAACgAAAAEAAAAOr3Xl2CnxTzPmTZN436rw0IAAAA9REqgl56zLcUAAAAKtM6yVrsKHKQUbXQ6TyPN/yL60c=</Password>
    <Database>gb_23A_20100521_11</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.Dialog.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <GACReference>Microsoft.SqlServer.Management.MultiServerConnection, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <GACReference>Microsoft.SqlServer.Management.CollectorEnum, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

static void Main()
		{
			var m = GetLastFirstMessage("BSSGP.RA-CAPABILITY-UPDATE", 10);
			m.Dump();
//			foreach (_Gb_ns_traffic a in m)
//			{
//				_racu r = new _racu();
//				r.FileNum = a.FileNum;
//				r.PacketNum = a.PacketNum;
//				r.PacketTime = a.PacketTime;
//				r.Ns_traffic_MsgType = a.Ns_traffic_MsgType;
//				_racus.InsertOnSubmit(r);
//				SubmitChanges();
//			}
		}

		private static IEnumerable<_gb> GetLastFirstMessage(string mess, int max)
		{
			var racu = from p in _gbs.Take (max)
					   where p.Ns_traffic_MsgType == mess
					   orderby p.Id  
					   select p;
			foreach (var p in racu)
			{
				var last = from q in _gbs.Take(max)
						   where q.Tlli == p.Tlli && q.Id < p.Id 
						  
						   select q;
				var lastfirstMess = last.FirstOrDefault();
				var secondfirstMess = last.Skip(1).FirstOrDefault();
				yield return lastfirstMess;
				yield return secondfirstMess;
			}
		}