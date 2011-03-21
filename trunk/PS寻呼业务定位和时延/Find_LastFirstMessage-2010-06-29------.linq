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
CREATE PROCEDURE Find_lastfirstmessage @ns_traffic_msgtype      [NVARCHAR](50), 
									   @condition               [NVARCHAR](50), 
									   @find_message_tlli_all_t [NVARCHAR](50), 
									   @last_this_id_t          [NVARCHAR](50) 
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
					WHERE  name = @find_message_tlli_all_t 
						   AND TYPE = 'u') 
			EXEC ('drop table '+ @find_message_tlli_all_t) 

		  IF EXISTS (SELECT * 
					 FROM   tempdb..sysobjects 
					 WHERE  id = Object_id(N'[tempdb..[' + 
										   @find_message_tlli_all_t + 
										   ']') 
					) 
			EXEC ('drop table '+ @find_message_tlli_all_t) 
	  END 

	  BEGIN --create _gb    
		  --select identity(int,1,1) as aa,bb into #t_temp from cc    
		  EXEC('   SELECT IDENTITY(INT, 1, 1) AS id,   *           INTO  ' + 
		  @find_message_tlli_all_t + 
'   FROM   _gb_ns_traffic    WHERE  tlli IN (SELECT tlli    FROM   #thismessage)    ORDER  BY packettime,  packetnum ' 
	) 

	PRINT @find_message_tlli_all_t + ' ok' 

	EXEC('ALTER TABLE '+ @find_message_tlli_all_t + ' ADD CONSTRAINT id_pk_' + 
	@find_message_tlli_all_t + ' PRIMARY KEY(id)') 

	EXEC('SELECT COUNT(*) AS find_message_tlli_all_message FROM  '+ 
	@find_message_tlli_all_t) 
END 

	BEGIN --drop _ThisMessageIndex           
		IF EXISTS(SELECT * 
				  FROM   sysobjects 
				  WHERE  name = '_ThisMessageIndex' 
						 AND TYPE = 'u') 
		  DROP TABLE _ThisMessageIndex 

		IF EXISTS (SELECT * 
				   FROM   tempdb..sysobjects 
				   WHERE  id = Object_id('tempdb.._ThisMessageIndex')) 
		  DROP TABLE _ThisMessageIndex 
	END 

	BEGIN --create _ThisMessageIndex    all tlli number   
		EXEC('   SELECT id,   tlli    INTO   _ThisMessageIndex    FROM   '+ 
		@find_message_tlli_all_t + 
		'   WHERE  ns_traffic_msgtype = '''+@ns_traffic_msgtype+'''   ORDER  BY id ') 

		PRINT '_ThisMessageIndex ok' 

		ALTER TABLE _ThisMessageIndex ADD CONSTRAINT id_pk_thismessageindex 
		PRIMARY 
		KEY(id) 

		SELECT COUNT(*) AS '_ThisMessageIndex' 
		FROM   _ThisMessageIndex 
	END 

	BEGIN --drop _LastFirstMessage          
		IF EXISTS(SELECT * 
				  FROM   sysobjects 
				  WHERE  name = @last_this_id_t 
						 AND TYPE = 'u') 
		  EXEC('DROP TABLE '+@last_this_id_t) 

		IF EXISTS (SELECT * 
				   FROM   tempdb..sysobjects 
				   WHERE  id = Object_id(N'[tempdb..[' + @last_this_id_t + ']')) 
		  EXEC('DROP TABLE '+@last_this_id_t) 
	END 

	BEGIN --create _LastFirstMessage   
		EXEC('   CREATE TABLE ['+ @last_this_id_t+ 
']  (     [id]                 INT IDENTITY(1, 1),     [LastFirstMessageID] [INT] NULL,     [ThisMessageID]      [INT] NULL  )' 
	) 

	PRINT @last_this_id_t + ' ok' 

	EXEC('   SELECT *    FROM  '+ @last_this_id_t) 
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
	  FROM   _ThisMessageIndex 
	  ORDER  BY id 

	OPEN order_cursor 

	FETCH NEXT FROM order_cursor INTO @id, @tlli 

	WHILE @@FETCH_STATUS = 0 -- FETCH            
	  BEGIN 
		  SET @begintime=Getdate() 

		  --_gb    
		  EXEC('set '+@LastFirstMessage_id+'=(SELECT  MAX(id)  FROM   '+ 
		  @find_message_tlli_all_t + ' WHERE  id < '+@id+'    AND tlli = '+@tlli 
		  + 
		  '    AND '+ @condition+ ' IS NOT NULL )') 

		  IF @LastFirstMessage_id > 0 
			BEGIN 
				--_LastFirstMessage     
				EXEC('   INSERT INTO ['+@last_this_id_t+ 
				']    (lastfirstmessageid,     thismessageid)    VALUES      (' 
				+ 
				@LastFirstMessage_id+', '+ @id) 

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

	EXEC('ALTER TABLE '+ @last_this_id_t + ' ADD CONSTRAINT id_pk_'+ 
	@last_this_id_t + ' PRIMARY KEY(id)') 
END 

GO  