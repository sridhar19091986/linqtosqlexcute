<Query Kind="SQL">
  <Connection>
    <ID>69c6882b-052d-4d33-8482-fc44aaa595ea</ID>
    <Server>192.168.1.6</Server>
    <Persist>true</Persist>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAN96yok7v7Eyv6CwMzOjNtQAAAAACAAAAAAADZgAAqAAAABAAAABPq8wPbMN4vRjVWN5aY3GzAAAAAASAAACgAAAAEAAAAPUA4cdtoD3riRYDMCFP6wIIAAAAfTDM16k7waAUAAAAKpDjl8vZLqArCW3VTCUIjGdOqPE=</Password>
    <Database>gb_21A_20100524_14</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

--   --23A数据库
--   use kpi_23A
--   --10A数据库
--   use kpi 
set ansi_warnings off

   select * from dbo._Gb_ns_traffic

	--消息类型
	select ns_traffic_MsgType,COUNT(*)
	from dbo._Gb_ns_traffic group by ns_traffic_MsgType
	order by 2 desc
   --无响应的问题
	select * from dbo._Gb_ns_traffic where ns_traffic_MsgType='BSSGP.DOWNLOAD-BSS-PFC'
	--BSS向SGSN索要 无线接入性能 的问题
   select * from dbo._Gb_ns_traffic where ns_traffic_MsgType='BSSGP.RA-CAPABILITY-UPDATE'
   
   select * from  dbo.Gb_Flow_Control_MS
	--计算SGSN响应流控的平均时延
   select  avg(flow_control_ms_ack_delayfirst) from  dbo.Gb_Flow_Control_MS
   
   
   
--无响应的问题
declare  @pfc int
declare  @racu int
declare  @tlli int
select COUNT(*) from (select distinct tlli  tlli_sum from dbo._Gb_ns_traffic 
 where ns_traffic_MsgType='BSSGP.DOWNLOAD-BSS-PFC') as a
 go
--BSS向SGSN索要 无线接入性能 的问题
 select distinct tlli from dbo._Gb_ns_traffic 
 where ns_traffic_MsgType='BSSGP.RA-CAPABILITY-UPDATE'
 
 
--3	RACU和DBPFC的用户占比
--统计23A 3月30日上午忙时9点多约24分钟的Gb数据，可以看到，BSC发RACU的用户比例是29.018%，
--即BSC丢失用户的IMSI和无线接入性能数据的概率是29.018%，BSC发DBPFC的用户比例是0.277%，即用户在进行数据业务时，Um接口或者Gb接口出现传输误码的概率是0.277%。
--Gb Message Type	TLLI count	TLLI rate	Cause
--BSSGP.DOWNLOAD-BSS-PFC	366	0.277%	Um接口或者Gb接口出现有传输误码
--BSSGP.RA-CAPABILITY-UPDATE	38402	29.018%	BSC有丢失用户的IMSI和无线接入性能数据
--ALL Message Type	132338	——	——

 select tlli,ns_traffic_MsgType,COUNT(*) as gCount
 into #temp
 from dbo._Gb_ns_traffic
 group by  tlli,ns_traffic_MsgType
 order by 3 desc
 
 select count(*) from (select distinct tlli from #temp) as a
 
  select ns_traffic_msgtype,COUNT(*)
  from #temp
  group by  ns_traffic_msgtype
  order by 1