<Query Kind="SQL">
  <Connection>
    <ID>e5f5449b-aa54-4234-bda6-c0296770953c</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>kpi_23A</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.Dialog.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <GACReference>Microsoft.SqlServer.Management.MultiServerConnection, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <GACReference>Microsoft.SqlServer.Management.CollectorEnum, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

--PS寻呼的业务定位临时解决办法
--1.格式化表，删除有重复的数据、创建索引、增加一些列
--delete  from _gb_ns_traffic where packetnum 
--in (
--select packetnum 
--from _gb_ns_traffic 
--group by packetnum
--having count(*)>1
--)
--GO
--alter table _gb_ns_traffic alter column packetnum int not null;
--GO
--alter table _gb_ns_traffic add constraint packetnum_pk primary key(packetnum);
--go
--1.创建2个临时表
go
--创建PS寻呼的临时表
if exists(select * from sysobjects where name='#ps' and type='u')
drop table #ps
if exists (select * from tempdb..sysobjects where id=object_id('tempdb..#ps'))
drop table #ps
go
select * into #ps from _gb_ns_traffic where ns_traffic_MsgType='BSSGP.PAGING-PS' and tlli is not null 
print '#ps ok'
select count(*) from #ps
go
--创建http-get、http-host等不为空的临时表
if exists(select * from sysobjects where name='#http' and type='u')
drop table #http
if exists (select * from tempdb..sysobjects where id=object_id('tempdb..#http'))
drop table #http
go
select * into #http from _gb_ns_traffic 
where Http_uri is not null or http_host is not null or Http_x_online is not null or Wsp_uri is not null
print '#http ok'
select count(*) from #http
--2.创建存储过程,
--精确计算用户通过代理发送ps-paging前最后一次的http-get的内容
if exists(select * from sysobjects where name='update_ns_traffic_table' and type='p')
drop procedure update_ns_traffic_table
go
create procedure update_ns_traffic_table
AS
BEGIN
declare @packetnum int
declare @tlli nvarchar(200)--临时变量，用来保存关联值tlli  用户标识
declare @Http_uri nvarchar(200)--临时变量，用来保存更新值@Http_uri
declare @http_host nvarchar(200)--临时变量，用来保存更新值@http_host
declare @Http_x_online nvarchar(200)--临时变量，用来保存更新值@Http_x_online
declare @Wsp_uri nvarchar(200)--临时变量，用来保存更新值@Wsp_uri
declare @lastHttpFrame int
declare @begintime datetime
declare @endtime datetime 
declare order_cursor cursor for
select packetnum,tlli from #ps
open order_cursor --打开游标
FETCH NEXT FROM order_cursor INTO @packetnum,@tlli  --开始循环游标变量
WHILE @@FETCH_STATUS = 0 --返回被 FETCH  语句执行的最后游标的状态 
begin  
	set @begintime=getdate() 
	if exists(select * from #http where packetnum<@packetnum and tlli=@tlli)
	begin
		select @lastHttpFrame=max(packetnum) from #http where packetnum<@packetnum and tlli=@tlli
		select @Http_uri=Http_uri,@http_host=http_host,@Http_x_online=Http_x_online,@Wsp_uri=Wsp_uri
		from #http where packetnum=@lastHttpFrame
		update _gb_ns_traffic set  Http_uri=@Http_uri,http_host=@http_host,Http_x_online=@Http_x_online,Wsp_uri=@Wsp_uri
		where packetnum=@packetnum 
	    print @packetnum
	    print @lastHttpFrame
	    print @Http_uri 
	    print @http_host 
	    print @Http_x_online 
	    print @Wsp_uri
	end
	set @endtime=getdate() 
	print datediff(millisecond,@begintime,@endtime)
	print '------------'
	print '------------'
	print '------------'
  FETCH NEXT FROM order_cursor INTO @packetnum,@tlli --开始循环游标变量
end
CLOSE order_cursor--关闭游标
DEALLOCATE order_cursor--释放游标
END
go
--3.执行存储过程
exec update_ns_traffic_table
go
print 'update_ns_traffic_table ok'