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
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

GO
--http://117.135.128.21:8080/images/face/newonline/173-1.gif
--IP地址的ISP名称查询
--http://211.139.150.5:8888/cnservice
print 'Gb采集时间和校正-FileNum=0||FileNum>0'
go
select 
min(PacketTime) M5解码开始时间,
max(PacketTime) M5解码结束时间,
datediff(s,min(PacketTime),max(PacketTime)) M5解码采集时长,
DATEADD(hour, -8,min(PacketTime)) 采集开始时间,
DATEADD(hour, -8,max(PacketTime)) 采集结束时间,
datediff(s,min(PacketTime),max(PacketTime)) 采集时长
from dbo._Gb_ns_traffic
go
print 'Gb统计时间和校正-FileNum=0'
go
select 
min(PacketTime) M5解码开始时间,
max(PacketTime) M5解码结束时间,
datediff(s,min(PacketTime),max(PacketTime)) M5解码采集时长,
DATEADD(hour, -8,min(PacketTime)) 采集开始时间,
DATEADD(hour, -8,max(PacketTime)) 采集结束时间,
datediff(s,min(PacketTime),max(PacketTime)) 采集时长
from dbo._Gb_ns_traffic
where FileNum=0
go
print 'Gb消息数量和流量占比-FileNum=0'
go
select ns_traffic_MsgType,count(*) message_num,
(count(*)+0.0)/(select count(*) from dbo._Gb_ns_traffic where len(ns_traffic_MsgType)>1 and FileNum=0) num_rate,
sum(ns_traffic_PackLen) message_len,
(sum(ns_traffic_PackLen)+0.0)/(select sum(ns_traffic_PackLen) from dbo._Gb_ns_traffic where len(ns_traffic_MsgType)>1 and FileNum=0) len_rate
from dbo._Gb_ns_traffic
where len(ns_traffic_MsgType)>1 and FileNum=0
group by ns_traffic_MsgType
order by 4 desc
GO
--按照小区计算Gb消息占比------BVCI，ns_traffic_PackLen,sum--动态SQL,行列转换
--BVCI，ns_traffic_MsgType,ns_traffic_PackLen
--动态SQL,行列转换
GO
if exists (select * from tempdb..sysobjects where id=object_id('tempdb..#temp'))
drop table #temp
GO
select bvci,
case 
	 when charindex('BSSGP.',ns_traffic_MsgType)>0  then 'BSSGP'
	 when charindex('GMMSM.',ns_traffic_MsgType)>0  then 'GMMSM'
	 else 'SNDCP'
end 
as msgtype,
ns_traffic_PackLen as packetlen
into #temp
from _Gb_ns_traffic
where FileNum=0
GO
--select * from #temp
go
print 'Gb信令和数据的数量和流量占比-FileNum=0'
go
go
select msgtype,count(*) message_num,
(count(*)+0.0)/(select count(*) from #temp) num_rate,
sum(packetlen) message_len,
(sum(packetlen)+0.0)/(select sum(packetlen) from #temp) len_rate
from #temp
group by msgtype
order by 3 desc
go
print '按小区统计Gb信令和数据的数量占比-FileNum=0'
go
--按照小区计算Gb消息数量占比
if exists (select * from sysobjects where id=object_id('tempa'))
drop table tempa
go
declare @sql varchar(8000)
set @sql = 'select bvci '
select @sql = @sql + ' , sum( case msgtype when  ''' + msgtype 
+ ''' then 1   else  0  end )  ['+ msgtype  +'_num]'
 from ( select distinct msgtype  from #temp  ) as a
set @sql = @sql + '  into  tempa from #temp group by bvci order by 3 desc '
exec(@sql)
go
select *,(SNDCP_num+0.0)/(GMMSM_num+SNDCP_num+BSSGP_num) SNDCP_num_rate from tempa order by 5 asc
go
print '按小区统计Gb信令和数据的流量占比-FileNum=0'
go
--按照小区计算Gb消息流量占比
if exists (select * from sysobjects where id=object_id('tempb'))
drop table tempb
go
declare @sql varchar(8000)
set @sql = 'select bvci '
select @sql = @sql + ' , sum( case msgtype when  ''' + msgtype 
+ ''' then packetlen   else  0  end )  ['+ msgtype  +'_len]'
 from ( select distinct msgtype  from #temp  ) as a
set @sql = @sql + ' into tempb from #temp group by bvci order by 3 desc '
exec(@sql)
go
select *,(SNDCP_len+0.0)/(GMMSM_len+SNDCP_len+BSSGP_len) SNDCP_len_rate from tempb order by 5 asc
go
print 'Gb下行消息和端口的消息数量占比-FileNum=0'
go
--生成消息和端口关系表
select Ns_traffic_MsgType,Tcp_s,count(*) message_num,
(count(*)+0.0)/(select count(*) from _Gb_ns_traffic where bssgp='DL-UNITDATA' and Tcp_s is not null and  FileNum=0)  num_rate,
sum(Ns_traffic_PackLen)  message_len,
(sum(Ns_traffic_PackLen)+0.0)/(select sum(Ns_traffic_PackLen) from _Gb_ns_traffic where bssgp='DL-UNITDATA' and Tcp_s is not null and  FileNum=0) len_rate
from _Gb_ns_traffic
where bssgp='DL-UNITDATA' and Tcp_s is not null and  FileNum=0
group by Ns_traffic_MsgType,Tcp_s
order by 5 desc
go
print '按小区分类统计流控速率-FileNum=0||FileNum>0'
go
--1.生成统计三
--各小区的流量分布，消息次数分布，流控分布
select a.*,b.*,a.fcms_rate/(9/0.235) ccch_num
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
print '按小区分类统计下载速率-FileNum=0'
go
--2.生成统计三,  
--各端口的流量分布，消息次数分布，流控分布
select a.*,b.*,a.bvci_rate/(59.2*1000) pdch_num
 from (
select bvci,sum(ns_traffic_PackLen) message_len
,(sum(ns_traffic_PackLen)+0.0)/(select sum(ns_traffic_PackLen) from _Gb_ns_traffic where  bvci is not null and link ='Down' and FileNum=0) len_rate
,datediff(s,min(PacketTime),max(PacketTime)) GbStat_time
,(8*sum(ns_traffic_PackLen)+0.0)/datediff(s,min(PacketTime),max(PacketTime)) bvci_rate
 from _Gb_ns_traffic
where bvci is not null and link ='Down' and FileNum=0
group by bvci
having datediff(s,min(PacketTime),max(PacketTime))>0
) as a left join (select bvci,count(*) tlli_num,sum(flow_control_ms_num) fcms_num from 
(
select e.bvci,e.message_num,f.flow_control_ms_num from (
select bvci,tlli,count(*) message_num
 from _Gb_ns_traffic
where bvci is not null and  link ='Down' and FileNum=0
group by bvci,tlli
) as e left join (
select tlli,count(*) flow_control_ms_num
 from _Gb_ns_traffic
where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS' and FileNum=0
group by tlli
) as f on e.tlli=f.tlli
) as c group by bvci
) as b on a.bvci=b.bvci
order by 2 desc
go
print '按照TCP端口统计流控效率-FileNum=0'
go
--2.生成统计三,  
--各端口的流量分布，消息次数分布，流控分布
select a.*,b.*,a.message_len/b.fcms_num perfcms_sendbyte
 from (
select Tcp_s,sum(ns_traffic_PackLen) message_len
,(sum(ns_traffic_PackLen)+0.0)/(select sum(ns_traffic_PackLen) from _Gb_ns_traffic where  Tcp_s is not null and link ='Down' and FileNum=0) len_rate
,datediff(s,min(PacketTime),max(PacketTime)) GbStat_time
--,(count(*)+0.0)/datediff(s,min(PacketTime),max(PacketTime)) Tcp_s_flow_control_ms_rate
 from _Gb_ns_traffic
where Tcp_s is not null and link ='Down' and FileNum=0
group by Tcp_s
having datediff(s,min(PacketTime),max(PacketTime))>0
) as a left join (select Tcp_s,count(*) tlli_num,sum(flow_control_ms_num) fcms_num from 
(
select e.Tcp_s,e.message_num,f.flow_control_ms_num from (
select Tcp_s,tlli,count(*) message_num
 from _Gb_ns_traffic
where Tcp_s is not null and  link ='Down' and FileNum=0
group by Tcp_s,tlli
) as e left join (
select tlli,count(*) flow_control_ms_num
 from _Gb_ns_traffic
where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS' and FileNum=0
group by tlli
) as f on e.tlli=f.tlli
) as c group by Tcp_s
) as b on a.Tcp_s=b.Tcp_s
order by 6 desc
go
print '按照UDP端口统计流控效率-FileNum=0'
go
--3.生成统计三,  
--各端口的流量分布，消息次数分布，流控分布
select a.*,b.*,a.message_len/b.fcms_num perfcms_sendbyte
 from (
select Udp_s,sum(ns_traffic_PackLen) message_len
,(sum(ns_traffic_PackLen)+0.0)/(select sum(ns_traffic_PackLen) from _Gb_ns_traffic where  Udp_s is not null and link ='Down' and FileNum=0) len_rate
,datediff(s,min(PacketTime),max(PacketTime)) GbStat_time
--,(count(*)+0.0)/datediff(s,min(PacketTime),max(PacketTime)) Udp_s_flow_control_ms_rate
 from _Gb_ns_traffic
where Udp_s is not null and link ='Down' and FileNum=0
group by Udp_s
having datediff(s,min(PacketTime),max(PacketTime))>0
) as a left join (select Udp_s,count(*) tlli_num,sum(flow_control_ms_num) fcms_num from 
(
select e.Udp_s,e.message_num,f.flow_control_ms_num from (
select Udp_s,tlli,count(*) message_num
 from _Gb_ns_traffic
where Udp_s is not null and  link ='Down' and FileNum=0
group by Udp_s,tlli
) as e left join (
select tlli,count(*) flow_control_ms_num
 from _Gb_ns_traffic
where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS' and FileNum=0
group by tlli
) as f on e.tlli=f.tlli
) as c group by Udp_s
) as b on a.Udp_s=b.Udp_s
order by 6 desc
go
print '按照IP统计流控效率-FileNum=0'
go
--3.生成统计三,  
--各端口的流量分布，消息次数分布，流控分布
select a.*,b.*,a.message_len/b.fcms_num perfcms_sendbyte
 from (
select ip_s,sum(ns_traffic_PackLen) message_len
,(sum(ns_traffic_PackLen)+0.0)/(select sum(ns_traffic_PackLen) from _Gb_ns_traffic where  ip_s is not null and link ='Down' and FileNum=0) len_rate
,datediff(s,min(PacketTime),max(PacketTime)) GbStat_time
--,(count(*)+0.0)/datediff(s,min(PacketTime),max(PacketTime)) ip_s_flow_control_ms_rate
 from _Gb_ns_traffic
where ip_s is not null and link ='Down' and FileNum=0
group by ip_s
having datediff(s,min(PacketTime),max(PacketTime))>0
) as a left join (select ip_s,count(*) tlli_num,sum(flow_control_ms_num) fcms_num from 
(
select e.ip_s,e.message_num,f.flow_control_ms_num from (
select ip_s,tlli,count(*) message_num
 from _Gb_ns_traffic
where ip_s is not null and  link ='Down' and FileNum=0
group by ip_s,tlli
) as e left join (
select tlli,count(*) flow_control_ms_num
 from _Gb_ns_traffic
where Ns_traffic_MsgType='BSSGP.FLOW-CONTROL-MS' and FileNum=0
group by tlli
) as f on e.tlli=f.tlli
) as c group by ip_s
) as b on a.ip_s=b.ip_s
order by 6 desc