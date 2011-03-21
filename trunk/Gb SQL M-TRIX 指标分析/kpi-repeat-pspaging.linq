<Query Kind="SQL">
  <Connection>
    <ID>e5f5449b-aa54-4234-bda6-c0296770953c</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>kpi_23A</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

--use kpi_gb_23a_20100413
set ansi_warnings off
GO
if exists (select * from tempdb..sysobjects where id=object_id('tempdb..#temp'))
begin
--select * from  #temp
drop table #temp
--print 'yes,drop #temp ok'
end 
else 
print 'no exist #temp'
GO
print '-------------'
GO
declare @dbname nvarchar(50)
select   @dbname=db_name()   --SQLServer中获取当前数据库名称
print @dbname
GO
select db_name()  BSC_Name
,CONVERT(VARCHAR(19),DATEADD(hour, -8,min(PacketTime)),21)+'-'+CONVERT(VARCHAR(8),DATEADD(hour, -8,max(PacketTime)),108)  GbStat_time,
datediff(s,min(PacketTime),max(PacketTime)) Stat_time
 from dbo.Gb_ATTACH
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

set @name='Paging_Type_PS'
select @num=sum(Paging_Type_PS),@delay=0 from dbo.Gb_Paging_PS
where PS_Paging_Repeat is null
set @total=@num
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='PS_Paging_Repeat'
select @num=sum(PS_Paging_Repeat),@delay=avg(PS_Paging_Repeat_delayFirst) from dbo.Gb_Paging_PS
where PS_Paging_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='Any_Uplink_PDU'
select @num=sum(Any_Uplink_PDU),@delay=avg(Any_Uplink_PDU_delayFirst) from dbo.Gb_Paging_PS
where PS_Paging_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)


GO

select * from #temp