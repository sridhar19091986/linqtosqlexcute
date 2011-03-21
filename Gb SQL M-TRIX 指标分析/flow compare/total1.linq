<Query Kind="SQL">
  <Connection>
    <ID>c3e164c7-9cf2-4b0d-aa90-72b91ec7a949</ID>
    <Persist>true</Persist>
    <Server>192.168.1.17</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAN96yok7v7Eyv6CwMzOjNtQAAAAACAAAAAAADZgAAqAAAABAAAACVwTFsk9dhMIn5i8ZFS9d4AAAAAASAAACgAAAAEAAAANmSpZffgSRmeLtQTcaKcpwIAAAAOuxdiYNQsLIUAAAAUdmDuQYJvN2uV/Lw/N9gMoxASHY=</Password>
    <Database>kpi_37B_20100819_21</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.Dialog.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Reference>E:\linq to sql\HtmlAgilityPack\HtmlAgilityPack.1.4.0\HtmlAgilityPack.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <GACReference>Microsoft.SqlServer.Management.MultiServerConnection, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <GACReference>Microsoft.SqlServer.Management.CollectorEnum, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>HtmlAgilityPack</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Security</Namespace>
  <Namespace>System.Security.Permissions</Namespace>
  <Namespace>System.Security.Principal</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
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
--set @name='Attach_Request'
--select @num=sum(Attach_Request),@delay=0 from dbo.Gb_ATTACH  
--set @total=@num
--set @name='Attach_Complete'
--select @num=sum(Attach_Complete),@delay=avg(Attach_Complete_delayFirst) from dbo.Gb_ATTACH  
--insert into #temp(mName,mNumber,mDelay,mRate) 
--values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='Attach_Request_idNull_ReATTnull'
select @num=sum(Attach_Request),@delay=0 from dbo.Gb_ATTACH  where ID_REQ is null and ID_RESP is null and Attach_Request_Repeat is null
set @total=@num
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='Authentication_Request_idNull_ReATTnull'
select @num=sum(Authentication_Request),@delay=avg(Authentication_Request_delayFirst) from dbo.Gb_ATTACH  where ID_REQ is null and ID_RESP is null and Attach_Request_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='Authentication_Response_idNull_ReATTnull'
select @num=sum(Authentication_Response),@delay=avg(Authentication_Response_delayFirst) from dbo.Gb_ATTACH  where ID_REQ is null and ID_RESP is null and Attach_Request_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='Attach_Accept_idNull_ReATTnull'
select @num=sum(Attach_Accept),@delay=avg(Attach_Accept_delayFirst) from dbo.Gb_ATTACH  where ID_REQ is null and ID_RESP is null and Attach_Request_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='Attach_Complete_idNull_ReATTnull'
select @num=sum(Attach_Complete),@delay=avg(Attach_Complete_delayFirst) from dbo.Gb_ATTACH  where ID_REQ is null and ID_RESP is null and Attach_Request_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
--
--set @name='PDP_Act_Request'
--select @num=sum(PDP_Act_Request),@delay=0 from dbo.Gb_PDP_Activation
--set @total=@num
--set @name='PDP_Act_Accept'
--select @num=sum(PDP_Act_Accept),@delay=avg(PDP_Act_Accept_delayFirst) from dbo.Gb_PDP_Activation
--insert into #temp(mName,mNumber,mDelay,mRate) 
--values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='PDP_Act_Request_RePDPnull'
select @num=sum(PDP_Act_Request),@delay=0 from dbo.Gb_PDP_Activation where PDP_Request_Repeat is null
set @total=@num
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='PDP_Act_Accept_RePDPnull'
select @num=sum(PDP_Act_Accept),@delay=avg(PDP_Act_Accept_delayFirst) from dbo.Gb_PDP_Activation where PDP_Request_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='Connect1x_reConNull'
select @num=sum(Connect1x),@delay=0 from Gb_Connect1x where Connect_Repeat is null
set @total=@num
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='Connect_Reply_reConNull'
select @num=sum(Connect_Reply),@delay=avg(Connect_Reply_delayFirst) from Gb_Connect1x where Connect_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='Connect1x_Acknowledge_reConNull'
select @num=sum(Acknowledge),@delay=avg(Acknowledge_delayFirst) from Gb_Connect1x where Connect_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='WSP_Get_reGetNull'
select @num=sum(Get1x),@delay=0 from Gb_Get1x where Get_Repeat is null
set @total=@num
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='Get_Reply_reGetNull'
select @num=sum(Reply),@delay=avg(Reply_delayFirst) from Gb_Get1x where Get_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='WSP_Get_Acknowledge_reGetNull'
select @num=sum(Acknowledge),@delay=avg(Acknowledge_delayFirst) from Gb_Get1x where Get_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='HTTP_Get_reGetNull'
select @num=sum(Get2x),@delay=0 from Gb_Get2x where Get_Repeat is null
set @total=@num
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='HTTP_Get_Response_reGetNull'
select @num=sum(Response),@delay=avg(Response_delayFirst) from Gb_Get2x where Get_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='WSP_Post_rePostNull'
select @num=sum(Post1x),@delay=0 from Gb_Post1x where Post_Repeat is null
set @total=@num
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='Post_Reply_rePostNull'
select @num=sum(Reply),@delay=avg(Reply_delayFirst) from Gb_Post1x where Post_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='WSP_Post_Acknowledge_rePostNull'
select @num=sum(Acknowledge),@delay=avg(Acknowledge_delayFirst) from Gb_Post1x where Post_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='HTTP_Post_rePostNull'
select @num=sum(Post2x),@delay=0 from Gb_Post2x where Post_Repeat is null
set @total=@num
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
set @name='HTTP_Post_Response_rePostNull' 
select @num=sum(Response),@delay=avg(Response_delayFirst) from Gb_Post2x where Post_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)
--
--set @name='Paging_Type_PS'
--select @num=sum(Paging_Type_PS),@delay=0 from dbo.Gb_Paging_PS
--where PS_Paging_Repeat is null
--set @total=@num
--set @name='Paging_TypePS_AnyUplinkPDU'
--select @num=sum(Any_Uplink_PDU),@delay=avg(Any_Uplink_PDU_delayFirst) from dbo.Gb_Paging_PS
--where PS_Paging_Repeat is null
--insert into #temp(mName,mNumber,mDelay,mRate) 
--values(@name, @num,@delay,(@num+0.0)/@total)

GO

select * from #temp