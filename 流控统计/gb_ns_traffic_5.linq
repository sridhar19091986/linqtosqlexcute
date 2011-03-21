<Query Kind="SQL">
  <Connection>
    <ID>e5f5449b-aa54-4234-bda6-c0296770953c</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>fcm_21A_Gb</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

--统计采集时间
select 
min(PacketTime) M5开始时间,
max(PacketTime) M5结束时间,
datediff(s,min(PacketTime),max(PacketTime)) M5采集时长,
DATEADD(hour, 8,min(PacketTime)) 采集开始时间,
DATEADD(hour, 8,max(PacketTime)) 采集结束时间,
datediff(s,min(PacketTime),max(PacketTime)) 采集时长
from dbo._Gb_ns_traffic

--查询Gb消息数量、流量占比---------ns_traffic_MsgType
select ns_traffic_MsgType,count(*) message_num,
(count(*)+0.0)/(select count(*) from dbo._Gb_ns_traffic where len(ns_traffic_MsgType)>1) num_rate,
sum(ns_traffic_PackLen) message_len,
(sum(ns_traffic_PackLen)+0.0)/(select sum(ns_traffic_PackLen) from dbo._Gb_ns_traffic where len(ns_traffic_MsgType)>1) len_rate
from dbo._Gb_ns_traffic
where len(ns_traffic_MsgType)>1
group by ns_traffic_MsgType
order by 2 desc
GO
--按照小区计算Gb消息占比------BVCI，ns_traffic_PackLen,sum--动态SQL,行列转换
--BVCI，ns_traffic_MsgType,ns_traffic_PackLen
--动态SQL,行列转换
GO
--if exists (select * from sysobjects where id=object_id('#temp'))
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
GO
--select * from #temp
go
select msgtype,count(*) message_num,
(count(*)+0.0)/(select count(*) from #temp) num_rate,
sum(packetlen) message_len,
(sum(packetlen)+0.0)/(select sum(packetlen) from #temp) len_rate
from #temp
group by msgtype
order by 3 desc
GO
--按照小区计算Gb消息数量占比
declare @sql varchar(8000)
set @sql = 'select bvci '
select @sql = @sql + ' , sum( case msgtype when  ''' + msgtype 
+ ''' then 1   else  0  end )  ['+ msgtype  +'_num]'
 from ( select distinct msgtype  from #temp  ) as a
set @sql = @sql + ' from #temp group by bvci order by 3 desc '
exec(@sql)
go
--按照小区计算Gb消息流量占比
declare @sql varchar(8000)
set @sql = 'select bvci '
select @sql = @sql + ' , sum( case msgtype when  ''' + msgtype 
+ ''' then packetlen   else  0  end )  ['+ msgtype  +'_len]'
 from ( select distinct msgtype  from #temp  ) as a
set @sql = @sql + ' from #temp group by bvci order by 3 desc '
exec(@sql)


