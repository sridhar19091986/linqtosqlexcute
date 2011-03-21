<Query Kind="SQL">
  <Connection>
    <ID>69c6882b-052d-4d33-8482-fc44aaa595ea</ID>
    <Server>192.168.1.4</Server>
    <Persist>true</Persist>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAN96yok7v7Eyv6CwMzOjNtQAAAAACAAAAAAADZgAAqAAAABAAAADGarvgRIQKFUM/WjBfyyxrAAAAAASAAACgAAAAEAAAAJ6jZzHbxzvi9SJa03W825cIAAAAclJvhqVcaAwUAAAA17I1+aB2/nENjlw0DfUGTav7HMs=</Password>
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
--print 'yes,drop #temp ok'
end 
--else 
--print 'no exist #temp'
--use kpi_18_2010_23A--kpi
GO
--print '-------------'
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
create table #temp
(
mID int identity(1,1),--自增列
mName nvarchar(50) not null,--消息名称
mNumber int null,--消息数量
mDelay int null,--消息时延
mRate nvarchar(50) null--占比
)
declare @name nvarchar(50)
declare @num int
declare @delay int
declare @total int
--
set @name='Attach_Request'
select @num=sum(Attach_Request),@delay=0 from dbo.Gb_ATTACH  
set @total=@num
set @name='Attach_Complete'
select @num=sum(Attach_Complete),@delay=avg(Attach_Complete_delayFirst) from dbo.Gb_ATTACH  
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='Attach_Request'
select @num=sum(Attach_Request),@delay=0 from dbo.Gb_ATTACH  where ID_REQ is null and ID_RESP is null and Attach_Request_Repeat is null
set @total=@num
set @name='Attach_Complete_idNull_ReATTnull'
select @num=sum(Attach_Complete),@delay=avg(Attach_Complete_delayFirst) from dbo.Gb_ATTACH  where ID_REQ is null and ID_RESP is null and Attach_Request_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--

set @name='PDP_Act_Request'
select @num=sum(PDP_Act_Request),@delay=0 from dbo.Gb_PDP_Activation
set @total=@num
set @name='PDP_Act_Accept'
select @num=sum(PDP_Act_Accept),@delay=avg(PDP_Act_Accept_delayFirst) from dbo.Gb_PDP_Activation
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='RAU_Request'
select @num=sum(RAU_Request),@delay=0 from Gb_Routing_Area_Update
set @total=@num
set @name='RAU_Complete'
select @num=sum(RAU_Complete),@delay=avg(RAU_Complete_delayFirst) from Gb_Routing_Area_Update
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='Connect1x'
select @num=sum(Connect1x),@delay=0 from Gb_Connect1x
set @total=@num
set @name='Connect1x_Acknowledge'
select @num=sum(Acknowledge),@delay=avg(Acknowledge_delayFirst) from Gb_Connect1x
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='WSP_Get'
select @num=sum(Get1x),@delay=0 from Gb_Get1x
set @total=@num
set @name='WSP_Get_Acknowledge'
select @num=sum(Acknowledge),@delay=avg(Acknowledge_delayFirst) from Gb_Get1x
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='HTTP_Get'
select @num=sum(Get2x),@delay=0 from Gb_Get2x
set @total=@num
set @name='HTTP_Get_Response'
select @num=sum(Response),@delay=avg(Response_delayFirst) from Gb_Get2x
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='WSP_Post'
select @num=sum(Post1x),@delay=0 from Gb_Post1x
set @total=@num
set @name='WSP_Post_Acknowledge'
select @num=sum(Acknowledge),@delay=avg(Acknowledge_delayFirst) from Gb_Post1x
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='HTTP_Post'
select @num=sum(Post2x),@delay=0 from Gb_Post2x
set @total=@num
set @name='HTTP_Post_Response'
select @num=sum(Response),@delay=avg(Response_delayFirst) from Gb_Post2x
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='Paging_Type_PS'
select @num=sum(Paging_Type_PS),@delay=0 from dbo.Gb_Paging_PS
where PS_Paging_Repeat is null
set @total=@num
set @name='Paging_TypePS_AnyUplinkPDU'
select @num=sum(Any_Uplink_PDU),@delay=avg(Any_Uplink_PDU_delayFirst) from dbo.Gb_Paging_PS
where PS_Paging_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

GO

select * from #temp