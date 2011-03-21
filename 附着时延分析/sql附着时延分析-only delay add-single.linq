<Query Kind="SQL">
  <Connection>
    <ID>80d6b436-1f57-4aef-992f-652eb2a986cb</ID>
    <Server>192.168.1.230</Server>
    <Persist>true</Persist>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAVn6xNHxDxUmkwNAvq5q6/wAAAAACAAAAAAADZgAAqAAAABAAAAB5BZfO1dPtRitz4jmIB2IoAAAAAASAAACgAAAAEAAAAItG1PxsLKPX4ARMNpYeLUQIAAAAng3Ubk3jF7QUAAAAuu9g23ZvK2W+dh5vq+U8CkV8ft0=</Password>
    <Database>gb_23A_20100521_11</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

set ansi_warnings off
--附着过程中   附着时延按照小区分布
select ci
--,avg(Attach_Request_Repeat_delayFirst) Attach_Request_Repeat_delay
--,avg(ID_REQ_delayFirst) ID_REQ_delay
--,avg(ID_RESP_delayFirst) ID_RESP_delay
,avg(Authentication_Request_delayFirst) SGSN_reponse_Authentication_Request_delay
,avg(Authentication_Response_delayFirst)-avg(Authentication_Request_delayFirst)  MS_reponse_Authentication_Response_delay
,avg(Attach_Accept_delayFirst)-avg(Authentication_Response_delayFirst) SGSN_reponse_Attach_Accept_delay 
,avg(Attach_Complete_delayFirst)-avg(Attach_Accept_delayFirst) MS_reponse_Attach_Complete_delay
,avg(Attach_Complete_delayFirst) Attach_Complete_delay
--,(count(Attach_Complete)+0.0)/count(Attach_Request) Attach_Complete_rate
from Gb_ATTACH
 where ID_REQ is null and ID_RESP is null and Attach_Request_Repeat is null and Attach_Complete is not null
--where ci is not null and Attach_Complete is not null
group by ci
--order by Attach_Complete_delay

--select * from Gb_ATTACH where ci=3873