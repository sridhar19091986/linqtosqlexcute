<Query Kind="SQL">
  <Connection>
    <ID>11c6a23a-6059-4922-aa34-1052d170d544</ID>
    <Persist>true</Persist>
    <Server>192.168.1.230</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAVn6xNHxDxUmkwNAvq5q6/wAAAAACAAAAAAADZgAAqAAAABAAAACm5o2Mk/JuJi7UAFRwf+FAAAAAAASAAACgAAAAEAAAAOp2DCFl678vKOeyqLL1hJQIAAAAq8VUB0siTTcUAAAAEwD3rymrBtkdIpV6Bjy6L7OHStg=</Password>
    <Database>gb_37b_20100520_22</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

--use kpi_18_2010_23A--kpi_gb_23a_20100413
--use kpi_gz_17a_20100326
set ansi_warnings off
--use kpi_23A
--
GO
if exists (select * from tempdb..sysobjects where id=object_id('tempdb..#temp'))
begin
--select * from  #temp
drop table #temp
--print 'yes,drop #temp ok'
end 
--else 
--print 'no exist #temp'
--GO
--print '-------------'
GO
GO
declare @dbname nvarchar(50)
select   @dbname=db_name()   --SQLServer中获取当前数据库名称
--print @dbname
if(@dbname!='gb_gz17A_')
select db_name()  BSC_Name
,CONVERT(VARCHAR(19),DATEADD(hour, -8,min(PacketTime)),21)+'-'+CONVERT(VARCHAR(8),DATEADD(hour, -8,max(PacketTime)),108)  GbStat_time,
datediff(s,min(PacketTime),max(PacketTime)) Stat_time
 from dbo.Gb_ATTACH
 else
 select db_name()  BSC_Name
,CONVERT(VARCHAR(19),DATEADD(hour, 0,min(PacketTime)),21)+'-'+CONVERT(VARCHAR(8),DATEADD(hour, 0,max(PacketTime)),108)  GbStat_time,
datediff(s,min(PacketTime),max(PacketTime)) Stat_time
 from dbo.Gb_ATTACH
GO
GO
GO
create table #temp
(
mID int identity(1,1),--自增列
mName nvarchar(50) not null,--消息名称
mNumber int null,--消息数量
mDelay int null,--消息时延
mRate nvarchar(50) null--占比
)

GO 
declare @name nvarchar(50)
declare @num int
declare @delay int
declare @total int

set @name='PDP_Act_Request'
select @num=sum(PDP_Act_Request),@delay=0 from dbo.Gb_PDP_Activation
set @total=@num
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='PDP_Request_Repeat'
select @num=sum(PDP_Request_Repeat),@delay=avg(PDP_Request_Repeat_delayFirst) from dbo.Gb_PDP_Activation
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='RADIO_Status'
select @num=sum(RADIO_Status),@delay=avg(RADIO_Status_delayFirst) from dbo.Gb_PDP_Activation
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='LLC_Discarded'
select @num=sum(LLC_Discarded),@delay=avg(LLC_Discarded_delayFirst) from dbo.Gb_PDP_Activation
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='Detach_Request'
select @num=sum(Detach_Request),@delay=avg(Detach_Request_delayFirst) from dbo.Gb_PDP_Activation
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='PDP_Act_Reject'
select @num=sum(PDP_Act_Reject),@delay=avg(PDP_Act_Reject_delayFirst) from dbo.Gb_PDP_Activation
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='PDP_Act_Accept'
select @num=sum(PDP_Act_Accept),@delay=avg(PDP_Act_Accept_delayFirst) from dbo.Gb_PDP_Activation
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

GO

select * from #temp