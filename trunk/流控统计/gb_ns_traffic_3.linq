<Query Kind="SQL">
  <Connection>
    <ID>e5f5449b-aa54-4234-bda6-c0296770953c</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>master</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

--drop table  _Gb_ns_traffic

--select top 10000 * into master..[_Gb_ns_traffic] from _Gb_ns_traffic

--查询信令、数据总数占比---------ns_traffic_MsgType
--select ns_traffic_MsgType,count(*) message_num,
--(count(*)+0.0)/(select count(*) from _Gb_ns_traffic where len(ns_traffic_MsgType)>1) message_rate
--from dbo._Gb_ns_traffic
--where len(ns_traffic_MsgType)>1
--group by ns_traffic_MsgType
--order by 2 desc
go
--查询小区流控速率
--select bvci,count(*) bvci_flow_control_ms_num
--,datediff(s,min(PacketTime),max(PacketTime)) bvci_flow_control_ms_time
--,(count(*)+0.0)/datediff(s,min(PacketTime),max(PacketTime)) bvci_flow_control_ms_rate
-- from dbo._Gb_ns_traffic
--where ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS'
--group by bvci
--having datediff(s,min(PacketTime),max(PacketTime))>0
--order by 4 desc
go
--按照小区统计流量分布-------------------link,bvci
--select bvci,sum(ns_traffic_PackLen),
--(sum(ns_traffic_PackLen)+0.0)/(select sum(ns_traffic_PackLen) from _Gb_ns_traffic where link ='Down') 
--from _Gb_ns_traffic
--where link ='Down'
--group by bvci
--order by 2 desc


--3.生成统计三,  
--各端口的流量分布，消息次数分布，流控分布
select a.*,b.*,a.message_len/b.fcms_num perfcms_sendbyte
 from (
select ip_s,sum(ns_traffic_PackLen) message_len
,(sum(ns_traffic_PackLen)+0.0)/(select sum(ns_traffic_PackLen) from _Gb_ns_traffic where  ip_s is not null and link ='Down') len_rate
,datediff(s,min(PacketTime),max(PacketTime)) GbStat_time
--,(count(*)+0.0)/datediff(s,min(PacketTime),max(PacketTime)) ip_s_flow_control_ms_rate
 from _Gb_ns_traffic
where ip_s is not null and link ='Down'
group by ip_s
having datediff(s,min(PacketTime),max(PacketTime))>0
) as a left join (select ip_s,count(*) tlli_num,sum(flow_control_ms_num) fcms_num from 
(
select e.ip_s,e.message_num,f.flow_control_ms_num from (
select ip_s,tlli,count(*) message_num
 from _Gb_ns_traffic
where ip_s is not null and  link ='Down'
group by ip_s,tlli
) as e left join (
select tlli,count(*) flow_control_ms_num
 from _Gb_ns_traffic
where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS'
group by tlli
) as f on e.tlli=f.tlli
) as c group by ip_s
) as b on a.ip_s=b.ip_s
order by 2 desc