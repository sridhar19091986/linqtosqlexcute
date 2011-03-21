<Query Kind="SQL">
  <Connection>
    <ID>d196856f-f29e-4a35-85e9-6a8d5b1a2964</ID>
    <Persist>true</Persist>
    <Server>.\SQLEXPRESS</Server>
    <Database>master</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

select a.bvci,a.GbStat_time,a.Stat_time
,a.message_len,b.fcms_num
,c.tcp14000_message_len,d.tcp14000_fcms_num
,(c.tcp14000_message_len+0.0)/a.message_len tcp14000_len_rate
,(d.tcp14000_fcms_num+0.0)/b.fcms_num tcp14000_fcms_rate
,8*(tcp14000_fcms_num/a.Stat_time+0.0)/(9/0.235)   tcp14000_fcms_ccch_rate
into Gb_23A
 from (
select bvci,sum(ns_traffic_PackLen) message_len
,CONVERT(VARCHAR(19),DATEADD(hour, -8,min(PacketTime)),21)+'-'+CONVERT(VARCHAR(8),DATEADD(hour, -8,max(PacketTime)),108)  GbStat_time,
datediff(s,min(PacketTime),max(PacketTime)) Stat_time
 from _Gb_ns_traffic
where bvci is not null and link ='Down' 
group by bvci
having sum(ns_traffic_PackLen) >0 and datediff(s,min(PacketTime),max(PacketTime))>0
) as a left join (
select bvci,count(*) fcms_num
 from _Gb_ns_traffic
where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS' 
group by bvci
having count(*)>0 
) as b on a.bvci=b.bvci left join (
select bvci,sum(ns_traffic_PackLen) tcp14000_message_len
 from _Gb_ns_traffic
where Tcp_s='14000' and link ='Down' 
group by bvci
) as c  on  a.bvci=c.bvci left join (
select bvci,count(*)  tcp14000_fcms_num
 from _Gb_ns_traffic
where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS' 
and tlli in (select tlli from _Gb_ns_traffic where Tcp_s='14000')
group by bvci
) as d on a.bvci=d.bvci
order by tcp14000_fcms_ccch_rate desc