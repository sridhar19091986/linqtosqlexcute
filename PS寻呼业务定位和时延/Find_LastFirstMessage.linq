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

--http://www.dpriver.com/pp/sqlformat.htm?ref=g_wangz     
IF EXISTS(SELECT * 
		  FROM   sysobjects 
		  WHERE  name = 'Find_LastFirstMessage' 
				 AND TYPE = 'p') 
  DROP PROCEDURE Find_LastFirstMessage 

GO 

CREATE PROCEDURE Find_LastFirstMessage
@ns_traffic_msgtype [NVARCHAR](50),@condition [NVARCHAR](50)
AS 
  BEGIN 
	  BEGIN --drop #LastFirstMessage     
		  IF EXISTS(SELECT * 
					FROM   sysobjects 
					WHERE  name = '#LastFirstMessage' 
						   AND TYPE = 'u') 
			DROP TABLE #LastFirstMessage 

		  IF EXISTS (SELECT * 
					 FROM   tempdb..sysobjects 
					 WHERE  id = Object_id('tempdb..#LastFirstMessage')) 
			DROP TABLE #LastFirstMessage 
	  END 

	  BEGIN --create #LastFirstMessage     
		  SELECT DISTINCT tlli as tlli 
		  INTO   #LastFirstMessage 
		  FROM   _gb_ns_traffic 
		  WHERE  ns_traffic_msgtype = @ns_traffic_msgtype 

		  PRINT '#LastFirstMessage ok' 

		  SELECT COUNT(*) 
		  FROM   #LastFirstMessage 
	  END 

	  BEGIN --drop _gb     
		  IF EXISTS(SELECT * 
					FROM   sysobjects 
					WHERE  name = '_gb' 
						   AND TYPE = 'u') 
			DROP TABLE _gb 

		  IF EXISTS (SELECT * 
					 FROM   tempdb..sysobjects 
					 WHERE  id = Object_id('tempdb.._gb')) 
			DROP TABLE _gb 
	  END 

	  BEGIN --create _gb    
		  SELECT IDENTITY(INT, 1, 1) AS id, 
				 * --select identity(int,1,1) as aa,bb into #t_temp from cc   
		  INTO   _gb 
		  FROM   _gb_ns_traffic 
		  WHERE  tlli IN (SELECT  tlli
						  FROM   #LastFirstMessage) 
		  ORDER  BY packettime, 
					packetnum 

		  PRINT '_gb ok' 

		  SELECT COUNT(*) 
		  FROM   _gb 
	  END 

	  BEGIN --drop _LastFirstMessage    
		  IF EXISTS(SELECT * 
					FROM   sysobjects 
					WHERE  name = '_LastFirstMessage' 
						   AND TYPE = 'u') 
			DROP TABLE _LastFirstMessage 

		  IF EXISTS (SELECT * 
					 FROM   tempdb..sysobjects 
					 WHERE  id = Object_id('tempdb.._LastFirstMessage')) 
			DROP TABLE _LastFirstMessage 
	  END 

	  BEGIN --create _LastFirstMessage   
		  CREATE TABLE [_LastFirstMessage] 
			( 
			   --id_num int IDENTITY(1,1), 
			   [FileNum]            [INT] NULL, 
			   [PacketNum]          [INT] NOT NULL, 
			   [PacketTime]         [DATETIME] NULL, 
			   [ns_traffic_MsgType] [NVARCHAR](50) NULL, 
			) 
		  ON [PRIMARY] 

		  PRINT '_LastFirstMessage ok' 

		  SELECT * 
		  FROM   _LastFirstMessage 
	  END 

	  --store procdure      
	  DECLARE @id                     INT, 
			  @tlli                   NVARCHAR(20), 
			  @lastPacketTime         DATETIME, 
			  @lastpacketnum          INT, 
			  @lastfilenum            INT, 
			  @lastns_traffic_msgtype NVARCHAR(50), 
			  @begintime              DATETIME, 
			  @endtime                DATETIME 
	  DECLARE order_cursor CURSOR FOR 
		SELECT id, 
			   tlli 
		FROM   _gb 
		WHERE  ns_traffic_msgtype = @ns_traffic_msgtype

	  OPEN order_cursor 

	  FETCH NEXT FROM order_cursor INTO @id, @tlli 

	  WHILE @@FETCH_STATUS = 0 -- FETCH      
		BEGIN 
			SET @begintime=Getdate() 

			--_gb   
			SELECT TOP 1 @lastpacketnum = packetnum, 
						 @lastPacketTime = packettime, 
						 @lastfilenum = filenum, 
						 @lastns_traffic_msgtype = ns_traffic_msgtype 
			FROM   _gb 
			WHERE  id < @id 
				   AND tlli = @tlli 
				   and @condition is not null
			ORDER  BY id DESC 

			IF @lastpacketnum > 0 
			  BEGIN 
				  --_LastFirstMessage     
				  INSERT INTO _LastFirstMessage 
				  VALUES      (@lastfilenum, 
							   @lastpacketnum, 
							   @lastPacketTime, 
							   @lastns_traffic_msgtype) 

				  PRINT @lastns_traffic_msgtype 

			  END 

			SET @endtime=Getdate() 

			PRINT Datediff(millisecond, @begintime, @endtime) 

			PRINT '------------' 

			SET @lastpacketnum=0 

			FETCH NEXT FROM order_cursor INTO @id, @tlli 
		END 

	  CLOSE order_cursor 

	  DEALLOCATE order_cursor 
  END 

GO 
exec Find_LastFirstMessage 'BSSGP.RA-CAPABILITY-UPDATE','ns_traffic_MsgType'
PRINT 'Find_LastFirstMessage ok'
