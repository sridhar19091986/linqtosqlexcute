<Query Kind="SQL">
  <Connection>
    <ID>d196856f-f29e-4a35-85e9-6a8d5b1a2964</ID>
    <Persist>true</Persist>
    <Server>.\SQLEXPRESS</Server>
    <Database>kpi_23A</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

--use kpi_18_2010_23A--kpi_gb_23a_20100413

--use kpi_23A
set ansi_warnings off
GO
if exists (select * from tempdb..sysobjects where id=object_id('tempdb..#temp'))
begin
select * from  #temp
drop table #temp
print 'yes,drop #temp ok'
end 
else 
print 'no exist #temp'

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

set @name='PDP_Act_Request'
select @num=sum(PDP_Act_Request),@delay=0 from dbo.Gb_PDP_Activation
where RADIO_Status is null
set @total=@num
insert into #temp(messageName,messageNumber,messageDelay,messageRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='PDP_Request_Repeat'
select @num=sum(PDP_Request_Repeat),@delay=avg(PDP_Request_Repeat_delayFirst) from dbo.Gb_PDP_Activation
where RADIO_Status is null
insert into #temp(messageName,messageNumber,messageDelay,messageRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='RADIO_Status'
select @num=sum(RADIO_Status),@delay=avg(RADIO_Status_delayFirst) from dbo.Gb_PDP_Activation
where RADIO_Status is null
insert into #temp(messageName,messageNumber,messageDelay,messageRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='LLC_Discarded'
select @num=sum(LLC_Discarded),@delay=avg(LLC_Discarded_delayFirst) from dbo.Gb_PDP_Activation
where RADIO_Status is null
insert into #temp(messageName,messageNumber,messageDelay,messageRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='Detach_Request'
select @num=sum(Detach_Request),@delay=avg(Detach_Request_delayFirst) from dbo.Gb_PDP_Activation
where RADIO_Status is null
insert into #temp(messageName,messageNumber,messageDelay,messageRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='PDP_Act_Reject'
select @num=sum(PDP_Act_Reject),@delay=avg(PDP_Act_Reject_delayFirst) from dbo.Gb_PDP_Activation
where RADIO_Status is null
insert into #temp(messageName,messageNumber,messageDelay,messageRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='PDP_Act_Accept'
select @num=sum(PDP_Act_Accept),@delay=avg(PDP_Act_Accept_delayFirst) from dbo.Gb_PDP_Activation
where RADIO_Status is null
insert into #temp(messageName,messageNumber,messageDelay,messageRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

GO

select * from #temp
