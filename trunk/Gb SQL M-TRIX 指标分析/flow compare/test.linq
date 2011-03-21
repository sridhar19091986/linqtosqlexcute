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
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.Dialog.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;System.Windows.Forms.dll</Reference>
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
----use kpi_18_2010_23A--kpi
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

set @name='Attach_Request'
select @num=sum(Attach_Request),@delay=0 from dbo.Gb_ATTACH  where ID_REQ is null and ID_RESP is null and Attach_Request_Repeat is null
set @total=@num
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

--set @name='Attach_Request_Repeat'
--select @num=sum(Attach_Request_Repeat),@delay=avg(Attach_Request_Repeat_delayFirst) from dbo.Gb_ATTACH  where ID_REQ is null and ID_RESP is null and Attach_Request_Repeat is null
--insert into #temp(mName,mNumber,mDelay,mRate) 
--values(@name, @num,@delay,(@num+0.0)/@total)
--
--set @name='ID_REQ'
--select @num=sum(ID_REQ),@delay=avg(ID_REQ_delayFirst) from dbo.Gb_ATTACH  where ID_REQ is null and ID_RESP is null and Attach_Request_Repeat is null
--insert into #temp(mName,mNumber,mDelay,mRate) 
--values(@name, @num,@delay,(@num+0.0)/@total)
--
--set @name='ID_RESP'
--select @num=sum(ID_RESP),@delay=avg(ID_RESP_delayFirst) from dbo.Gb_ATTACH where ID_REQ is null and ID_RESP is null and Attach_Request_Repeat is null
--insert into #temp(mName,mNumber,mDelay,mRate) 
--values(@name, @num,@delay,(@num+0.0)/@total)

set @name='Authentication_Request'
select @num=sum(Authentication_Request),@delay=avg(Authentication_Request_delayFirst) from dbo.Gb_ATTACH  where ID_REQ is null and ID_RESP is null and Attach_Request_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='Authentication_Response'
select @num=sum(Authentication_Response),@delay=avg(Authentication_Response_delayFirst) from dbo.Gb_ATTACH  where ID_REQ is null and ID_RESP is null and Attach_Request_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

--set @name='RADIO_Status'
--select @num=sum(RADIO_Status),@delay=avg(RADIO_Status_delayFirst) from dbo.Gb_ATTACH  where ID_REQ is null and ID_RESP is null and Attach_Request_Repeat is null
--insert into #temp(mName,mNumber,mDelay,mRate) 
--values(@name, @num,@delay,(@num+0.0)/@total)
--
--set @name='LLC_Discarded'
--select @num=sum(LLC_Discarded),@delay=avg(LLC_Discarded_delayFirst) from dbo.Gb_ATTACH  where ID_REQ is null and ID_RESP is null and Attach_Request_Repeat is null
--insert into #temp(mName,mNumber,mDelay,mRate) 
--values(@name, @num,@delay,(@num+0.0)/@total)
--
--set @name='Detach_Request'
--select @num=sum(Detach_Request),@delay=avg(Detach_Request_delayFirst) from dbo.Gb_ATTACH  where ID_REQ is null and ID_RESP is null and Attach_Request_Repeat is null
--insert into #temp(mName,mNumber,mDelay,mRate) 
--values(@name, @num,@delay,(@num+0.0)/@total)
--
--set @name='Attach_Reject'
--select @num=sum(Attach_Reject),@delay=avg(Attach_Reject_delayFirst) from dbo.Gb_ATTACH  where ID_REQ is null and ID_RESP is null and Attach_Request_Repeat is null
--insert into #temp(mName,mNumber,mDelay,mRate) 
--values(@name, @num,@delay,(@num+0.0)/@total)

set @name='Attach_Accept'
select @num=sum(Attach_Accept),@delay=avg(Attach_Accept_delayFirst) from dbo.Gb_ATTACH  where ID_REQ is null and ID_RESP is null and Attach_Request_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='Attach_Complete'
select @num=sum(Attach_Complete),@delay=avg(Attach_Complete_delayFirst) from dbo.Gb_ATTACH  where ID_REQ is null and ID_RESP is null and Attach_Request_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='PDP_Act_Request'
select @num=sum(PDP_Act_Request),@delay=0 from dbo.Gb_PDP_Activation where PDP_Request_Repeat is null
set @total=@num
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

set @name='PDP_Act_Accept'
select @num=sum(PDP_Act_Accept),@delay=avg(PDP_Act_Accept_delayFirst) from dbo.Gb_PDP_Activation where PDP_Request_Repeat is null
insert into #temp(mName,mNumber,mDelay,mRate) 
values(@name, @num,@delay,(@num+0.0)/@total)

GO

select * from #temp