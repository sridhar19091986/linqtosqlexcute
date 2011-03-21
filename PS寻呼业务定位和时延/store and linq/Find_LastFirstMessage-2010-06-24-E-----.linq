<Query Kind="SQL">
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
  <GACReference>Microsoft.SqlServer.Management.MultiServerConnection, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <GACReference>Microsoft.SqlServer.Management.CollectorEnum, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

--??????   
--??RACU??????????   
EXEC Find_lastfirstmessage 
  'BSSGP.RA-CAPABILITY-UPDATE', 
  'ns_traffic_MsgType' 

--??PS???????   
EXEC Find_lastfirstmessage 
  'BSSGP.PAGING-PS', 
  'Http_uri' 

--????????   
GO 

PRINT 'Find_LastFirstMessage ok' 

GO 

SELECT * 
FROM   _lastfirstmessage 

GO 

SELECT * 
FROM   _gb 
order by id

GO 

SELECT * 
FROM   _gb, 
	   _lastfirstmessage 
WHERE  _gb.id = _lastfirstmessage.lastfirstmessageid 

GO 

SELECT * 
FROM   _gb, 
	   _lastfirstmessage 
WHERE  _gb.id = _lastfirstmessage.thismessageid 