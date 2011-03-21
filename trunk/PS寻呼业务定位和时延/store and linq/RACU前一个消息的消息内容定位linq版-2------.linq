<Query Kind="Program">
  <Connection>
    <ID>e5f5449b-aa54-4234-bda6-c0296770953c</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>master</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.Dialog.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;System.Windows.Forms.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <GACReference>Microsoft.SqlServer.Management.MultiServerConnection, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <GACReference>Microsoft.SqlServer.Management.CollectorEnum, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.Data.Linq.Mapping</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

	void Main()
	{
        //string mess = @"BSSGP.RA-CAPABILITY-UPDATE";
		string mess = @"BSSGP.PAGING-PS";
		/*
		这个存储过程生成2张表，
		1、出现这条消息的TLLI的所有消息关联表，=>_gb
		2、这条消息（this_id)出现时他前一条消息是什么消息（last_id)  =>_LastFirstMessage
		SELECT * FROM   _gb, _lastfirstmessage WHERE  _gb.id = _lastfirstmessage.thismessageid
		*/
		initTable(mess);  
		IEnumerable<_LastFirstMessage> m = GetLastFirstMessage(mess); //输出某消息到一个集合中
		this._LastFirstMessages.InsertAllOnSubmit(m);  //datacontext和table的关联
		this.SubmitChanges();
	}
	private IEnumerable<_LastFirstMessage> GetLastFirstMessage(string mess)
	{
		var messages = from a in _gbs
					   orderby a.Id
					   select a;
		var racu = from p in messages
				   where p.Ns_traffic_MsgType == mess
				   select p;
		foreach (var p in racu)
		{
			int i = 0;
			var last = from q in messages.Take(p.Id - 1)  //take(x)输出前面x个元素,不能包含自己
					   where q.Tlli == p.Tlli
					   orderby q.Id descending
					   select q;
			var lastfirstMess = last.Where(e => e.Ns_traffic_MsgType != null);
			var _lastfirstMess = lastfirstMess.FirstOrDefault();
			if (lastfirstMess.Any())
			{
				i++;
				_LastFirstMessage lastfirst = new _LastFirstMessage { Id = i, LastFirstMessageID = _lastfirstMess.Id, ThisMessageID = p.Id };
				yield return lastfirst;
			}
		}
	}

	private void initTable(string mess)
	{
		string _LastFirstMessage = @"
		   IF EXISTS(SELECT * 
					FROM   sysobjects 
					WHERE  name = '_LastFirstMessage' 
						   AND TYPE = 'u') 
			DROP TABLE _LastFirstMessage;
		   IF EXISTS (SELECT * 
					 FROM   tempdb..sysobjects 
					 WHERE  id = Object_id('tempdb.._LastFirstMessage')) 
		    DROP TABLE _LastFirstMessage;
		   CREATE TABLE [_LastFirstMessage] 
			( 
			   [id]                 INT IDENTITY(1, 1), 
			   [LastFirstMessageID] [INT] NULL, 
			   [ThisMessageID]      [INT] NULL 
			)  
		   ON [PRIMARY] ;
		  ALTER TABLE _LastFirstMessage ADD CONSTRAINT id_pk_LastFirstMessage 
		  PRIMARY 
		  KEY(id);";//增加主键约束
		this.ExecuteCommand(_LastFirstMessage);

		string _gb = @"	    
		  IF EXISTS(SELECT * 
					FROM   sysobjects 
					WHERE  name = '#LastFirstMessage' 
						   AND TYPE = 'u') 
			DROP TABLE #LastFirstMessage;
		  IF EXISTS (SELECT * 
					 FROM   tempdb..sysobjects 
					 WHERE  id = Object_id('tempdb..#LastFirstMessage')) 
			DROP TABLE #LastFirstMessage; 
		  SELECT DISTINCT tlli as tlli 
		  INTO   #LastFirstMessage 
		  FROM   _gb_ns_traffic 
		  WHERE  ns_traffic_msgtype = '" + mess + @"';
		  IF EXISTS(SELECT * 
					FROM   sysobjects 
					WHERE  name = '_gb' 
						   AND TYPE = 'u') 
			DROP TABLE _gb;
		  IF EXISTS (SELECT * 
					 FROM   tempdb..sysobjects 
					 WHERE  id = Object_id('tempdb.._gb')) 
			DROP TABLE _gb;
		  SELECT IDENTITY(INT, 1, 1) AS id, * 
		  INTO   _gb 
		  FROM   _gb_ns_traffic 
		  WHERE  tlli IN (SELECT  tlli
						  FROM   #LastFirstMessage) 
		  ORDER  BY packettime, 
					packetnum;
		  ALTER TABLE _gb ADD CONSTRAINT id_pk_gb PRIMARY KEY(id) ";
		//增加主键约束
		this.ExecuteCommand(_gb);
	}