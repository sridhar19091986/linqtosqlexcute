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

--sql???????    
--http://www.dpriver.com/pp/sqlformat.htm?ref=g_wangz         
IF EXISTS(SELECT * 
		  FROM   sysobjects 
		  WHERE  name = 'Find_LastFirstMessage' 
				 AND TYPE = 'p') 
  DROP PROCEDURE find_lastfirstmessage 

GO 

--??????    
--2????1????2?????????    
CREATE PROCEDURE Find_lastfirstmessage @ns_traffic_msgtype [NVARCHAR](50), 
									   @condition          [NVARCHAR](50) 
AS 
  BEGIN 
	  BEGIN --drop #ThisMessage         
		  IF EXISTS(SELECT * 
					FROM   sysobjects 
					WHERE  name = '#ThisMessage' 
						   AND TYPE = 'u') 
			DROP TABLE #thismessage 

		  IF EXISTS (SELECT * 
					 FROM   tempdb..sysobjects 
					 WHERE  id = Object_id('tempdb..#ThisMessage')) 
			DROP TABLE #thismessage 
	  END 

	  BEGIN --create #ThisMessage  distinct tlli number   
		  SELECT DISTINCT tlli AS tlli 
		  INTO   #thismessage 
		  FROM   _gb_ns_traffic 
		  WHERE  ns_traffic_msgtype = @ns_traffic_msgtype 

		  PRINT '#ThisMessage ok' 

		  SELECT COUNT(*) AS '#ThisMessage' 
		  FROM   #thismessage 
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
				 * 
		  --select identity(int,1,1) as aa,bb into #t_temp from cc       
		  INTO   _gb 
		  FROM   _gb_ns_traffic 
		  WHERE  tlli IN (SELECT tlli 
						  FROM   #thismessage) 
		  ORDER  BY packettime, 
					packetnum 

		  PRINT '_gb ok' 

		  ALTER TABLE _gb ADD CONSTRAINT id_pk_gb PRIMARY KEY(id) 

		  SELECT COUNT(*) AS '_gb' 
		  FROM   _gb 
	  END 

	  BEGIN --drop #ThisMessageIndex         
		  IF EXISTS(SELECT * 
					FROM   sysobjects 
					WHERE  name = '#ThisMessageIndex' 
						   AND TYPE = 'u') 
			DROP TABLE #thismessageindex 

		  IF EXISTS (SELECT * 
					 FROM   tempdb..sysobjects 
					 WHERE  id = Object_id('tempdb..#ThisMessageIndex')) 
			DROP TABLE #thismessageindex 
	  END 

	  BEGIN --create #ThisMessageIndex    all tlli number      
		  SELECT id, 
				 tlli 
		  INTO   #thismessageindex 
		  FROM   _gb 
		  WHERE  ns_traffic_msgtype = @ns_traffic_msgtype 
		  ORDER  BY id 

		  PRINT '#ThisMessageIndex ok' 

		  ALTER TABLE #thismessageindex ADD CONSTRAINT id_pk_thismessageindex 
		  PRIMARY 
		  KEY(id) 

		  SELECT COUNT(*) AS '#ThisMessageIndex' 
		  FROM   #thismessageindex 
	  END 

	  BEGIN --drop _LastFirstMessage        
		  IF EXISTS(SELECT * 
					FROM   sysobjects 
					WHERE  name = '_LastFirstMessage' 
						   AND TYPE = 'u') 
			DROP TABLE _lastfirstmessage 

		  IF EXISTS (SELECT * 
					 FROM   tempdb..sysobjects 
					 WHERE  id = Object_id('tempdb.._LastFirstMessage')) 
			DROP TABLE _lastfirstmessage 
	  END 

	  BEGIN --create _LastFirstMessage       
		  CREATE TABLE [_LastFirstMessage] 
			( 
			   [id]                 INT IDENTITY(1, 1), 
			   [LastFirstMessageID] [INT] NULL, 
			   [ThisMessageID]      [INT] NULL 
			) 

		  PRINT '_LastFirstMessage ok' 

		  SELECT * 
		  FROM   _lastfirstmessage 
	  END 

	  --store procdure          
	  DECLARE @id                  INT, 
			  @tlli                NVARCHAR(20), 
			  @LastFirstMessage_id INT, 
			  @begintime           DATETIME, 
			  @endtime             DATETIME 
	  DECLARE order_cursor CURSOR FOR 
		SELECT id, 
			   tlli 
		FROM   #thismessageindex 
		ORDER  BY id 

	  OPEN order_cursor 

	  FETCH NEXT FROM order_cursor INTO @id, @tlli 

	  WHILE @@FETCH_STATUS = 0 -- FETCH          
		BEGIN 
			SET @begintime=Getdate() 

			--_gb       
			SELECT @LastFirstMessage_id = MAX(id) 
			FROM   _gb 
			WHERE  id < @id 
				   AND tlli = @tlli 
				   AND @condition IS NOT NULL 

			IF @LastFirstMessage_id > 0 
			  BEGIN 
				  --_LastFirstMessage     
				  INSERT INTO [_LastFirstMessage] 
							  (lastfirstmessageid, 
							   thismessageid) 
				  VALUES      (@LastFirstMessage_id, 
							   @id) 

				  PRINT @LastFirstMessage_id 

				  PRINT @id 
			  END 

			SET @endtime=Getdate() 

			PRINT Datediff(millisecond, @begintime, @endtime) 

			PRINT '------------' 

			SET @LastFirstMessage_id =0 

			FETCH NEXT FROM order_cursor INTO @id, @tlli 
		END 

	  CLOSE order_cursor 

	  DEALLOCATE order_cursor 

	  ALTER TABLE _lastfirstmessage ADD CONSTRAINT id_pk_lastfirstmessage 
	  PRIMARY 
	  KEY(id) 
  END 

GO 