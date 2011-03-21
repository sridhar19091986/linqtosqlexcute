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
		  WHERE  name = 'update_racu' 
				 AND TYPE = 'p') 
  DROP PROCEDURE update_racu 

GO 

CREATE PROCEDURE Update_racu
@ns_traffic_msgtype [NVARCHAR](50),@condition [NVARCHAR](50)
AS 
  BEGIN 
	  BEGIN --drop #racu     
		  IF EXISTS(SELECT * 
					FROM   sysobjects 
					WHERE  name = '#racu' 
						   AND TYPE = 'u') 
			DROP TABLE #racu 

		  IF EXISTS (SELECT * 
					 FROM   tempdb..sysobjects 
					 WHERE  id = Object_id('tempdb..#racu')) 
			DROP TABLE #racu 
	  END 

	  BEGIN --create #racu     
		  SELECT * 
		  INTO   #racu 
		  FROM   _gb_ns_traffic 
		  WHERE  ns_traffic_msgtype = @ns_traffic_msgtype 

		  PRINT '#racu ok' 

		  SELECT COUNT(*) 
		  FROM   #racu 
	  END 

	  BEGIN --drop #gb     
		  IF EXISTS(SELECT * 
					FROM   sysobjects 
					WHERE  name = '#gb' 
						   AND TYPE = 'u') 
			DROP TABLE #gb 

		  IF EXISTS (SELECT * 
					 FROM   tempdb..sysobjects 
					 WHERE  id = Object_id('tempdb..#gb')) 
			DROP TABLE #gb 
	  END 

	  BEGIN --create #gb    
		  SELECT IDENTITY(INT, 1, 1) AS id, 
				 * --select identity(int,1,1) as aa,bb into #t_temp from cc   
		  INTO   #gb 
		  FROM   _gb_ns_traffic 
		  WHERE  tlli IN (SELECT DISTINCT tlli 
						  FROM   #racu) 
		  ORDER  BY packettime, 
					packetnum 

		  PRINT '#gb ok' 

		  SELECT COUNT(*) 
		  FROM   #gb 
	  END 

	  BEGIN --drop _racu    
		  IF EXISTS(SELECT * 
					FROM   sysobjects 
					WHERE  name = '_racu' 
						   AND TYPE = 'u') 
			DROP TABLE _racu 

		  IF EXISTS (SELECT * 
					 FROM   tempdb..sysobjects 
					 WHERE  id = Object_id('tempdb.._racu')) 
			DROP TABLE _racu 
	  END 

	  BEGIN --create _racu   
		  CREATE TABLE [_racu] 
			( 
			   --id_num int IDENTITY(1,1), 
			   [FileNum]            [INT] NULL, 
			   [PacketNum]          [INT] NOT NULL, 
			   [PacketTime]         [DATETIME] NULL, 
			   [ns_traffic_MsgType] [NVARCHAR](50) NULL, 
			) 
		  ON [PRIMARY] 

		  PRINT '_racu ok' 

		  SELECT * 
		  FROM   _racu 
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
		FROM   #gb 
		WHERE  ns_traffic_msgtype = @ns_traffic_msgtype

	  OPEN order_cursor 

	  FETCH NEXT FROM order_cursor INTO @id, @tlli 

	  WHILE @@FETCH_STATUS = 0 -- FETCH      
		BEGIN 
			SET @begintime=Getdate() 

			--#gb   
			SELECT TOP 1 @lastpacketnum = packetnum, 
						 @lastPacketTime = packettime, 
						 @lastfilenum = filenum, 
						 @lastns_traffic_msgtype = ns_traffic_msgtype 
			FROM   #gb 
			WHERE  id < @id 
				   AND tlli = @tlli 
				   and @condition is not null
			ORDER  BY id DESC 

			IF @lastpacketnum > 0 
			  BEGIN 
				  --_racu     
				  INSERT INTO _racu 
				  VALUES      (@lastfilenum, 
							   @lastpacketnum, 
							   @lastPacketTime, 
							   @lastns_traffic_msgtype) 

				  PRINT @lastpacketnum 

				  PRINT @lastPacketTime 
			  END 

			SET @endtime=Getdate() 

			PRINT Datediff(millisecond, @begintime, @endtime) 

			PRINT '------------' 

			PRINT '------------' 

			PRINT '------------' 

			SET @lastpacketnum=0 

			FETCH NEXT FROM order_cursor INTO @id, @tlli 
		END 

	  CLOSE order_cursor 

	  DEALLOCATE order_cursor 
  END 

GO 
exec update_racu 'BSSGP.RA-CAPABILITY-UPDATE' 
PRINT 'update_racu ok' 