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
</Query>

set ansi_warnings off
GO
if exists (select * from tempdb..sysobjects where id=object_id('tempdb..#temp'))
begin
--select * from  #temp
drop table #temp
print 'yes,drop #temp ok'
end 
else 
print 'no exist #temp'
--use kpi_18_2010_23A--kpi
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
create table #temp
(
messageID int identity(1,1),--自增列
messageName nvarchar(50) not null,--消息名称
messageNumber int null,--消息数量
messageDelay int null,--消息时延
messageRate nvarchar(50) null--占比
)

GO 
declare @name nvarchar(50)
declare @num int
declare @delay int
declare @total int

set @name='Attach_Request'
select @num=sum(Attach_Request),@delay=0 from dbo.Gb_ATTACH  
set @total=@num
insert into #temp(messageName,messageNumber,messageDelay,messageRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='Attach_Request_Repeat'
select @num=sum(Attach_Request_Repeat),@delay=avg(Attach_Request_Repeat_delayFirst) from dbo.Gb_ATTACH  
insert into #temp(messageName,messageNumber,messageDelay,messageRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='ID_REQ'
select @num=sum(ID_REQ),@delay=avg(ID_REQ_delayFirst) from dbo.Gb_ATTACH  
insert into #temp(messageName,messageNumber,messageDelay,messageRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='ID_RESP'
select @num=sum(ID_RESP),@delay=avg(ID_RESP_delayFirst) from dbo.Gb_ATTACH  
insert into #temp(messageName,messageNumber,messageDelay,messageRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='Authentication_Request'
select @num=sum(Authentication_Request),@delay=avg(Authentication_Request_delayFirst) from dbo.Gb_ATTACH  
insert into #temp(messageName,messageNumber,messageDelay,messageRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='Authentication_Response'
select @num=sum(Authentication_Response),@delay=avg(Authentication_Response_delayFirst) from dbo.Gb_ATTACH  
insert into #temp(messageName,messageNumber,messageDelay,messageRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='RADIO_Status'
select @num=sum(RADIO_Status),@delay=avg(RADIO_Status_delayFirst) from dbo.Gb_ATTACH  
insert into #temp(messageName,messageNumber,messageDelay,messageRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='LLC_Discarded'
select @num=sum(LLC_Discarded),@delay=avg(LLC_Discarded_delayFirst) from dbo.Gb_ATTACH  
insert into #temp(messageName,messageNumber,messageDelay,messageRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='Detach_Request'
select @num=sum(Detach_Request),@delay=avg(Detach_Request_delayFirst) from dbo.Gb_ATTACH  
insert into #temp(messageName,messageNumber,messageDelay,messageRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='Attach_Reject'
select @num=sum(Attach_Reject),@delay=avg(Attach_Reject_delayFirst) from dbo.Gb_ATTACH  
insert into #temp(messageName,messageNumber,messageDelay,messageRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='Attach_Accept'
select @num=sum(Attach_Accept),@delay=avg(Attach_Accept_delayFirst) from dbo.Gb_ATTACH  
insert into #temp(messageName,messageNumber,messageDelay,messageRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='Attach_Complete'
select @num=sum(Attach_Complete),@delay=avg(Attach_Complete_delayFirst) from dbo.Gb_ATTACH  
insert into #temp(messageName,messageNumber,messageDelay,messageRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

GO

select * from #temp