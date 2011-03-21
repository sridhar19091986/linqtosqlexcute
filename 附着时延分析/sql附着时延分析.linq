<Query Kind="SQL">
  <Connection>
    <ID>11c6a23a-6059-4922-aa34-1052d170d544</ID>
    <Persist>true</Persist>
    <Server>192.168.1.230</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAVn6xNHxDxUmkwNAvq5q6/wAAAAACAAAAAAADZgAAqAAAABAAAACm5o2Mk/JuJi7UAFRwf+FAAAAAAASAAACgAAAAEAAAAOp2DCFl678vKOeyqLL1hJQIAAAAq8VUB0siTTcUAAAAEwD3rymrBtkdIpV6Bjy6L7OHStg=</Password>
    <Database>gb_23A_20100521_11</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

set ansi_warnings off
--附着过程中   附着时延按照小区分布
select ci
,count(Attach_Request) Attach_Request
,count(Attach_Request_Repeat) Attach_Request_Repeat,avg(Attach_Request_Repeat_delayFirst) Attach_Request_Repeat_delay
,count(ID_REQ) ID_REQ,avg(ID_REQ_delayFirst) ID_REQ_delay
,count(ID_RESP) ID_RESP,avg(ID_RESP_delayFirst) ID_RESP_delay
,count(Authentication_Request) Authentication_Request,avg(Authentication_Request_delayFirst) Authentication_Request_delay
,count(Authentication_Response) Authentication_Response,avg(Authentication_Response_delayFirst)  Authentication_Response_delay
,count(Attach_Accept) Attach_Accept,avg(Attach_Accept_delayFirst) Attach_Accept_delay 
,count(Attach_Complete) Attach_Complete ,avg(Attach_Complete_delayFirst) Attach_Complete_delay
,(count(Attach_Complete)+0.0)/count(Attach_Request)
from Gb_ATTACH
--where ci is not null and Attach_Complete is not null
group by ci
order by Attach_Complete_delay

--select * from Gb_ATTACH where ci=3920