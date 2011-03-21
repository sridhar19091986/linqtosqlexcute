<Query Kind="SQL">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>msqq</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

use [msqq]
GO
BULK INSERT msqqBulk2 
	FROM 'G:\chensheng\200好友QQ心跳\接收群消息\aa.csv' 
	WITH 
	( 
		FIELDTERMINATOR = ';', 
		ROWTERMINATOR = '\n' 
	)
	
update msqqBulk2 set message_type='接收群消息' where message_type='4'