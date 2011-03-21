<Query Kind="SQL">
  <Connection>
    <ID>e5f5449b-aa54-4234-bda6-c0296770953c</ID>
    <Server>.\SQLEXPRESS</Server>
    <Database>kpi_23A</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.Dialog.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;System.Windows.Forms.dll</Reference>
  <GACReference>Microsoft.SqlServer.Management.MultiServerConnection, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <GACReference>Microsoft.SqlServer.Management.CollectorEnum, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

--创建存储过程,
--精确计算#racu交互前的一条消息
if exists(select * from sysobjects where name='update_racu' and type='p')
drop procedure update_racu
go
create procedure update_racu
AS
BEGIN
begin --创建#racu临时表
if exists(select * from sysobjects where name='#racu' and type='u')
drop table #racu
if exists (select * from tempdb..sysobjects where id=object_id('tempdb..#racu'))
drop table #racu
end
begin
select * into #racu from _gb_ns_traffic where ns_traffic_MsgType='BSSGP.RA-CAPABILITY-UPDATE'
print '#racu ok'
select count(*) from #racu
end
begin --创建_racu临时表
if exists(select * from sysobjects where name='_racu' and type='u')
drop table _racu
if exists (select * from tempdb..sysobjects where id=object_id('tempdb.._racu'))
drop table _racu
end
begin
CREATE TABLE [_racu](
	[FileNum] [int] NULL,
	[PacketNum] [int] NOT NULL,
	[PacketTime] [datetime] NULL,
	[ns_traffic_MsgType] [nvarchar](200) NULL,
) ON [PRIMARY]
print '_racu ok'
select count(*) from _racu
end
--定义临时变量
declare @PacketTime datetime,@lastPacketTime datetime
declare @packetnum int,@lastpacketnum int
declare @tlli nvarchar(200)
declare @begintime datetime
declare @endtime datetime 
declare order_cursor cursor for
select packetnum,PacketTime,tlli from #racu
open order_cursor --打开游标
FETCH NEXT FROM order_cursor INTO @packetnum,@PacketTime,@tlli --开始循环游标变量
WHILE @@FETCH_STATUS = 0 --返回被 FETCH  语句执行的最后游标的状态 
begin  
	set @begintime=getdate() 
	--判断前面是否有消息--找出前面那条消息的 帧号和时间
	select top 1 @lastpacketnum=packetnum,@lastPacketTime=PacketTime
		from _Gb_ns_traffic where packetnum<@packetnum and PacketTime<@PacketTime and tlli=@tlli
		order by PacketTime desc
	if @lastpacketnum>0
	begin
		--按照时间和帧号  插入 新表
		insert into _racu 
		select FileNum,PacketNum,PacketTime,ns_traffic_MsgType
		from _Gb_ns_traffic 
		where packetnum=@lastpacketnum and PacketTime=@lastPacketTime
		print @lastpacketnum 
	    print @packetnum
		print @lastPacketTime
	    print @PacketTime 
	end
	set @endtime=getdate() 
	print datediff(millisecond,@begintime,@endtime)
	print '------------'
	print '------------'
	print '------------'
	set @lastpacketnum=0
  FETCH NEXT FROM order_cursor INTO @packetnum,@PacketTime,@tlli  --开始循环游标变量
end
CLOSE order_cursor--关闭游标
DEALLOCATE order_cursor--释放游标
END
go
--3.执行存储过程
--exec update_racu
go
print 'update_racu ok'