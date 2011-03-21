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

--1.生成统计三
--各小区的流量分布，消息次数分布，流控分布
select a.*,b.*
from (
select bvci,count(*) fcms_num
,(count(*)+0.0)/(select count(*) from _Gb_ns_traffic where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS') fcms_per
,datediff(s,min(PacketTime),max(PacketTime)) GbStat_time
,(count(*)+0.0)/datediff(s,min(PacketTime),max(PacketTime)) fcms_rate
 from _Gb_ns_traffic
where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS'
group by bvci
having datediff(s,min(PacketTime),max(PacketTime))>0
) as a left join (select bvci,count(*)  tlli_num from 
(
select bvci,tlli,count(*) bvci_tlli_flow_control_ms_num
 from _Gb_ns_traffic
where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS'
group by bvci,tlli
) as c group by bvci
) as b on a.bvci=b.bvci
order by 5 desc
go
--2.生成统计三,  
--各端口的流量分布，消息次数分布，流控分布
select a.*,b.*,a.message_len/b.fcms_num perfcms_sendbyte
 from (
select Tcp_s,sum(ns_traffic_PackLen) message_len
,(sum(ns_traffic_PackLen)+0.0)/(select sum(ns_traffic_PackLen) from _Gb_ns_traffic where  Tcp_s is not null and link ='Down') len_rate
,datediff(s,min(PacketTime),max(PacketTime)) GbStat_time
--,(count(*)+0.0)/datediff(s,min(PacketTime),max(PacketTime)) Tcp_s_flow_control_ms_rate
 from _Gb_ns_traffic
where Tcp_s is not null and link ='Down'
group by Tcp_s
having datediff(s,min(PacketTime),max(PacketTime))>0
) as a left join (select Tcp_s,count(*) tlli_num,sum(flow_control_ms_num) fcms_num from 
(
select e.Tcp_s,e.message_num,f.flow_control_ms_num from (
select Tcp_s,tlli,count(*) message_num
 from _Gb_ns_traffic
where Tcp_s is not null and  link ='Down'
group by Tcp_s,tlli
) as e left join (
select tlli,count(*) flow_control_ms_num
 from _Gb_ns_traffic
where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS'
group by tlli
) as f on e.tlli=f.tlli
) as c group by Tcp_s
) as b on a.Tcp_s=b.Tcp_s
order by 2 desc
--3.生成统计三,  
--各端口的流量分布，消息次数分布，流控分布
select a.*,b.*,a.message_len/b.fcms_num perfcms_sendbyte
 from (
select Udp_s,sum(ns_traffic_PackLen) message_len
,(sum(ns_traffic_PackLen)+0.0)/(select sum(ns_traffic_PackLen) from _Gb_ns_traffic where  Udp_s is not null and link ='Down') len_rate
,datediff(s,min(PacketTime),max(PacketTime)) GbStat_time
--,(count(*)+0.0)/datediff(s,min(PacketTime),max(PacketTime)) Udp_s_flow_control_ms_rate
 from _Gb_ns_traffic
where Udp_s is not null and link ='Down'
group by Udp_s
having datediff(s,min(PacketTime),max(PacketTime))>0
) as a left join (select Udp_s,count(*) tlli_num,sum(flow_control_ms_num) fcms_num from 
(
select e.Udp_s,e.message_num,f.flow_control_ms_num from (
select Udp_s,tlli,count(*) message_num
 from _Gb_ns_traffic
where Udp_s is not null and  link ='Down'
group by Udp_s,tlli
) as e left join (
select tlli,count(*) flow_control_ms_num
 from _Gb_ns_traffic
where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS'
group by tlli
) as f on e.tlli=f.tlli
) as c group by Udp_s
) as b on a.Udp_s=b.Udp_s
order by 2 desc
--测试
--select * from (
--select Tcp_s,tlli,count(*) message_num
-- from _Gb_ns_traffic
--where Tcp_s is not null and  link ='Down'
--group by Tcp_s,tlli
--) as e left join (
--select tlli,count(*) flow_control_ms_num
-- from _Gb_ns_traffic
--where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS'
--group by tlli
--) as f on e.tlli=f.tlli