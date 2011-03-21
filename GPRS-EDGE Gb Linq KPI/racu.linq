<Query Kind="SQL">
  <Connection>
    <ID>69c6882b-052d-4d33-8482-fc44aaa595ea</ID>
    <Server>192.168.1.6</Server>
    <Persist>true</Persist>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAN96yok7v7Eyv6CwMzOjNtQAAAAACAAAAAAADZgAAqAAAABAAAABPq8wPbMN4vRjVWN5aY3GzAAAAAASAAACgAAAAEAAAAPUA4cdtoD3riRYDMCFP6wIIAAAAfTDM16k7waAUAAAAKpDjl8vZLqArCW3VTCUIjGdOqPE=</Password>
    <Database>gb_23A_20100329_11</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

--3	RACU和DBPFC的用户占比
--统计23A 3月30日上午忙时9点多约24分钟的Gb数据，可以看到，BSC发RACU的用户比例是29.018%，
--即BSC丢失用户的IMSI和无线接入性能数据的概率是29.018%，BSC发DBPFC的用户比例是0.277%，即用户在进行数据业务时，Um接口或者Gb接口出现传输误码的概率是0.277%。
--Gb Message Type	TLLI count	TLLI rate	Cause
--BSSGP.DOWNLOAD-BSS-PFC	366	0.277%	Um接口或者Gb接口出现有传输误码
--BSSGP.RA-CAPABILITY-UPDATE	38402	29.018%	BSC有丢失用户的IMSI和无线接入性能数据
--ALL Message Type	132338	——	——
set ansi_warnings off
GO
if exists (select * from tempdb..sysobjects where id=object_id('tempdb..#temp'))
begin
--select * from  #temp
drop table #temp
--print 'yes,drop #temp ok'
end 
GO
 select tlli,ns_traffic_MsgType,COUNT(*) as gCount
 into #temp
 from dbo._Gb_ns_traffic
 group by  tlli,ns_traffic_MsgType
 order by 3 desc
GO
 select count(*) from (select distinct tlli from #temp) as a
GO
  select ns_traffic_msgtype,COUNT(*)
  from #temp
  group by  ns_traffic_msgtype
  order by 1