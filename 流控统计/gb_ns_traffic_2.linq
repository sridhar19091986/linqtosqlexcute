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
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

--##################数据处理思路
--select * from _Gb_ns_traffic
go
--生成tlli和端口关系表
--select tlli,Ns_traffic_MsgType,Tcp_s,count(*) message_num
--from _Gb_ns_traffic
--where bssgp='DL-UNITDATA' and Tcp_s is not null
--group by tlli,Ns_traffic_MsgType,Tcp_s
--order by 4 desc
go
--生成消息和端口关系表
select Ns_traffic_MsgType,Tcp_s,count(*) message_num
from _Gb_ns_traffic
where bssgp='DL-UNITDATA' and Tcp_s is not null
group by Ns_traffic_MsgType,Tcp_s
order by 3 desc
go
--生成tlli、流控数量关系、流控时间间隔关系表
--select tlli,count(*) tlli_flow_control_ms_num,
--datediff(s,min(PacketTime),max(PacketTime)) tlli_flow_control_ms_time,
--(count(*)+0.0)/datediff(s,min(PacketTime),max(PacketTime)) tlli_flow_control_ms_rate
-- from _Gb_ns_traffic
--where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS'
--group by tlli
--having datediff(s,min(PacketTime),max(PacketTime))>0
go
--查询小区流控速率
--select * from (
--select bvci,count(*) bvci_flow_control_ms_num
--,datediff(s,min(PacketTime),max(PacketTime)) bvci_flow_control_ms_time
--,(count(*)+0.0)/datediff(s,min(PacketTime),max(PacketTime)) bvci_flow_control_ms_rate
-- from _Gb_ns_traffic
--where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS'
--group by bvci
--having datediff(s,min(PacketTime),max(PacketTime))>0
--) as a left join (select bvci,count(*) bvci_tlli_num from 
--(
--select bvci,tlli,count(*) bvci_tlli_flow_control_ms_num
-- from _Gb_ns_traffic
--where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS'
--group by bvci,tlli
--) as c group by bvci
--) as b on a.bvci=b.bvci
--order by 4 desc
go
-----------port\BSSGP.FLOW-CONTROL-MS-num\flow-contol-ms-timer---------------
--select a.*,b.Tcp_s from (
----生成tlli、流控数量关系、流控时间间隔关系表
--select tlli,count(*)  flow_control_ms_num,datediff(s,min(PacketTime),max(PacketTime)) flow_control_ms_timer
----,(count(*)+0.0)/datediff(s,min(PacketTime),max(PacketTime))
-- from _Gb_ns_traffic
--where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS'
--group by tlli
----having datediff(s,min(PacketTime),max(PacketTime))>0
--) as a left join (
----生成tlli和端口关系表
--select tlli,Ns_traffic_MsgType,Tcp_s,count(*) tlli_port
--from _Gb_ns_traffic
--where Ns_traffic_MsgType='DL-UNITDATA' and Tcp_s is not null
--group by tlli,Ns_traffic_MsgType,Tcp_s
--) as b on a.tlli=b.tlli
go
if exists (select * from master..sysobjects where id=object_id('master..port_flow_control_ms'))
drop table port_flow_control_ms
go
--###################数据处理结果
--1.生成统计库
select a.*,b.Tcp_s into port_flow_control_ms  from (
--生成tlli、流控数量关系、流控时间间隔关系表
select tlli,count(*)  flow_control_ms_num,datediff(s,min(PacketTime),max(PacketTime)) flow_control_ms_timer
--,(count(*)+0.0)/datediff(s,min(PacketTime),max(PacketTime))
 from _Gb_ns_traffic
where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS'
group by tlli
--having datediff(s,min(PacketTime),max(PacketTime))>0
) as a left join (
--生成tlli和端口关系表
select tlli,Ns_traffic_MsgType,Tcp_s,count(*) tlli_port
from _Gb_ns_traffic
where Ns_traffic_MsgType='DL-UNITDATA' and Tcp_s is not null
group by tlli,Ns_traffic_MsgType,Tcp_s
) as b on a.tlli=b.tlli
go
--select * from port_flow_control_ms
go
--2.生成统计一

select Tcp_s 业务端口,
sum(flow_control_ms_num) 流控总数
,(sum(flow_control_ms_num)+0.0)/(select sum(flow_control_ms_num) from port_flow_control_ms) 流控总数占比
,count(tlli) 用户总数
,sum(flow_control_ms_timer)  累计时间
,(sum(flow_control_ms_num)+0.0)/sum(flow_control_ms_timer)  平均每用户流控速率
from port_flow_control_ms
group by Tcp_s
having sum(flow_control_ms_timer)>0
order by  2 desc
go
--3.生成统计二
select 
min(PacketTime) 采集开始时间,
max(PacketTime) 采集结束时间,
datediff(s,min(PacketTime),max(PacketTime)) 采集时长,
(select count(*) 用户总数1
 from (select tlli,count(*) as c from _Gb_ns_traffic group by tlli ) as a) 用户总数,
(count(*)+0.0) 流控数量,
(count(*)+0.0)/datediff(s,min(PacketTime),max(PacketTime)) 流控速率
from _Gb_ns_traffic
where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS'
go
--4.生成统计三
--select a.bvci ,b.bvci_tlli_num,a.bvci_flow_control_ms_num,
--a.bvci_flow_control_ms_time,a.bvci_flow_control_percentage,a.bvci_flow_control_ms_rate from (
--select bvci,count(*) bvci_flow_control_ms_num
--,(count(*)+0.0)/(select count(*) from _Gb_ns_traffic where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS') bvci_flow_control_percentage
--,datediff(s,min(PacketTime),max(PacketTime)) bvci_flow_control_ms_time
--,(count(*)+0.0)/datediff(s,min(PacketTime),max(PacketTime)) bvci_flow_control_ms_rate
-- from _Gb_ns_traffic
--where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS'
--group by bvci
--having datediff(s,min(PacketTime),max(PacketTime))>0
--) as a left join (select bvci,count(*) bvci_tlli_num from 
--(
--select bvci,tlli,count(*) bvci_tlli_flow_control_ms_num
-- from _Gb_ns_traffic
--where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS'
--group by bvci,tlli
--) as c group by bvci
--) as b on a.bvci=b.bvci
--order by 5 desc