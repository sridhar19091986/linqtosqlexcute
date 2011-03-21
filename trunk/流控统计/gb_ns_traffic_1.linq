<Query Kind="SQL">
  <Connection>
    <ID>e5f5449b-aa54-4234-bda6-c0296770953c</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>master</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

--USE Stream_Anlysis_2008_04A
--GO
--use Stream_Anlysis_2010_04A
--GO
--use stream_radio_status
--GO
--查询 
--select top 100 * from _gb_ns_traffic
GO
--只需要20000行数据
--delete from _gb_ns_traffic where packetnum>2000
--
--GO
----1.格式化表，删除有重复的数据、创建索引、增加一些列
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
--GO
--alter table _gb_ns_traffic add Ip_s nvarchar(200) null;
--GO
--alter table _gb_ns_traffic add Tcp_s nvarchar(400) null;
--GO
--alter table _gb_ns_traffic add Http_uri nvarchar(600) null;
--GO
----2.创建存储过程,更新点击的url,执行存储过程
--if exists(select * from sysobjects where name='update_Ip_s_Tcp_s_Http_uri' and type='p')
--drop procedure update_Ip_s_Tcp_s_Http_uri
--GO
--create procedure update_Ip_s_Tcp_s_Http_uri
--as 
--BEGIN
--declare @packetnum int
--declare @link nvarchar(100)
--declare @Ip_s nvarchar(200)
--declare @Tcp_s nvarchar(400)
--declare @Http_uri nvarchar(600)
--declare order_cursor cursor for select packetnum,link from _gb_ns_traffic  
--open order_cursor --打开游标
--FETCH NEXT FROM order_cursor INTO @packetnum,@link  --开始循环游标变量
--WHILE @@FETCH_STATUS = 0 --返回被 FETCH  语句执行的最后游标的状态
--  Begin    
--	--上行方向取  目标IP PORT     
--	if(@link='Up') 
--	  select @Ip_s=ip_d,@Tcp_s=isnull(tcp_d,'')+isnull(udp_d,''),
--			 @Http_uri=isnull(http_uri,'')+isnull(wsp_uri,'')+isnull(http_x_online,'')
--	  from _gb_ns_traffic where packetnum=@packetnum
--	else
--	--下行方向取  源IP PORT
--	  select @Ip_s=ip_s,@Tcp_s=isnull(tcp_s,'')+isnull(udp_s,''),
--			 @Http_uri=isnull(http_uri,'')+isnull(wsp_uri,'')+isnull(http_x_online,'')
--	  from _gb_ns_traffic where packetnum=@packetnum
--	--更新值
--	begin
--	  print @packetnum
--	  update _gb_ns_traffic set Ip_s=@Ip_s,Tcp_s=@Tcp_s,Http_uri=@Http_uri where packetnum=@packetnum
--	end  
--	FETCH NEXT FROM order_cursor INTO @packetnum,@link 
--  End 
--CLOSE order_cursor--关闭游标
--DEALLOCATE order_cursor--释放游标
--END 
--GO
--exec update_Ip_s_Tcp_s_Http_uri
--GO
----3.创建存储过程,精确计算Gb流量,执行存储过程
--if exists(select * from sysobjects where name='update_ns_traffic_table' and type='p')
--drop procedure update_ns_traffic_table
--GO
--create procedure update_ns_traffic_table
--AS
--BEGIN
--declare @packetnum int
--declare @tlli nvarchar(200)--临时变量，用来保存关联值tlli  用户标识
--declare @Ip_s nvarchar(200)--临时变量，用来保存更新值Ip_s  业务IP
--declare @Tcp_s nvarchar(400)--临时变量，用来保存更新值Tcp_s   业务端口
--declare @link nvarchar(200) --临时变量，用来保存link  方向标识
--declare order_cursor cursor for select packetnum,link,tlli 
--from _gb_ns_traffic where len(Ip_s)<2 and len(Tcp_s)<2  --Ip_s和Tcp_s为空的行
--open order_cursor --打开游标
--FETCH NEXT FROM order_cursor INTO @packetnum,@link,@tlli  --开始循环游标变量
--WHILE @@FETCH_STATUS = 0 --返回被 FETCH  语句执行的最后游标的状态
--  Begin         
--	  if(@link='Up') 
--		--获取更新值ip、port     如果是上行方向，选这条消息之后下行方向最开始的第1个包，且这个包的IP不是空
--		select @Ip_s=Ip_s,@Tcp_s=Tcp_s from _gb_ns_traffic 
--		where packetnum=
--		(select min(packetnum) 
--		from _gb_ns_traffic 
--		where packetnum>@packetnum 
--		and link ='Down' 
--		and len(Ip_s)>1
--		and len(Tcp_s)>1 
--		and tlli=@tlli)
--	  else		   
--		--获取更新值ip、port  如果是下行方向，选这条消息之前下行方向最后的1个包，且这个包的IP不是空
--		select @Ip_s=Ip_s,@Tcp_s=Tcp_s from _gb_ns_traffic 
--		where packetnum=
--		(select max(packetnum)
--		from _gb_ns_traffic
--		where packetnum<@packetnum 
--		and link ='Down' 
--		and len(Ip_s)>1
--		and len(Tcp_s)>1 
--		and tlli=@tlli)
--	  --更新值	
--	  begin
--		 PRINT  @packetnum
--		 update _gb_ns_traffic set Ip_s=@Ip_s,Tcp_s=@Tcp_s where packetnum=@packetnum 
--	  end 
--	  FETCH NEXT FROM order_cursor INTO @packetnum,@link,@tlli --开始循环游标变量
--  End 
--CLOSE order_cursor--关闭游标
--DEALLOCATE order_cursor--释放游标
--END
--GO
--exec update_ns_traffic_table
--GO


------
--查询执行结果
--select top 100 * from dbo._gb_ns_traffic 
--GO
--if exists(select * from sysobjects where name='_Gb_ns_traffic' and type='u')
--drop table _Gb_ns_traffic
--GO
----生成_Gb_ns_traffic
--select top 100000 * into _Gb_ns_traffic from dbo._gb_ns_traffic 
--GO
----查询生成的
--select top 100 * from _Gb_ns_traffic
--GO
--查询信令、数据总数占比---------ns_traffic_MsgType
--select ns_traffic_MsgType,count(*),
--(count(*)+0.0)/(select count(*) from dbo._Gb_ns_traffic where len(ns_traffic_MsgType)>1)
--from dbo._Gb_ns_traffic
--where len(ns_traffic_MsgType)>1
--group by ns_traffic_MsgType
--order by 2 desc
GO
--信令流量、有效流量的总流量占比------ns_traffic_PackLen,sum
declare @dataStream int
declare @signallingStream int
declare @allStream int
set @dataStream=(select sum(ns_traffic_PackLen) from _Gb_ns_traffic 
where charindex('BSSGP.',ns_traffic_MsgType)>0 or charindex('GMMSM.',ns_traffic_MsgType)>0)
set @allStream =(select sum(ns_traffic_PackLen) from _Gb_ns_traffic)
select @signallingStream=@allStream-@dataStream
print cast(@dataStream as nvarchar(50)) +'-'+
	  cast(@signallingStream as nvarchar(50)) +'-'+
	  cast(@allStream as nvarchar(50)) +'-'+
	  cast ((@dataStream+0.0)/@allStream as nvarchar(50))
GO
--信令包、流量包的总个数占比------ns_traffic_PackLen,count
declare @dataStream int
declare @signallingStream int
declare @allStream int
set @dataStream=(select count(ns_traffic_PackLen) from _Gb_ns_traffic 
where charindex('BSSGP.',ns_traffic_MsgType)>0 or charindex('GMMSM.',ns_traffic_MsgType)>0)
set @allStream =(select count(ns_traffic_PackLen) from _Gb_ns_traffic)
select @signallingStream=@allStream-@dataStream
print cast(@dataStream as nvarchar(50)) +'-'+
	  cast(@signallingStream as nvarchar(50)) +'-'+
	  cast(@allStream as nvarchar(50)) +'-'+
	  cast ((@dataStream+0.0)/@allStream as nvarchar(50))
GO
--按照下行、IP统计流量分布---------------------link,Ip_s
select link,Ip_s,sum(ns_traffic_PackLen),
(sum(ns_traffic_PackLen)+0.0)/(select sum(ns_traffic_PackLen) from _Gb_ns_traffic where link ='Down') 
from _Gb_ns_traffic
where link ='Down'
group by link,Ip_s
order by 4 desc
GO
--按照下行、PORT统计流量分布------------------link,Tcp_s
select link,Tcp_s,sum(ns_traffic_PackLen),
(sum(ns_traffic_PackLen)+0.0)/(select sum(ns_traffic_PackLen) from _Gb_ns_traffic where link ='Down') 
from _Gb_ns_traffic
where link ='Down'
group by link,Tcp_s
order by 4 desc
GO
--按照下行、小区统计流量分布-------------------link,bvci
select link,bvci,sum(ns_traffic_PackLen),
(sum(ns_traffic_PackLen)+0.0)/(select sum(ns_traffic_PackLen) from _Gb_ns_traffic where link ='Down') 
from _Gb_ns_traffic
group by link,bvci
order by 3 desc
GO
--按照点击量计算分布---------------------------------Http_uri
select Http_uri,count(*),
(count(*)+0.0)/(select count(*) from _Gb_ns_traffic where len(Http_uri)>2) 
from _Gb_ns_traffic
where len(Http_uri)>2
group by Http_uri
order by 3 desc
GO
--按照点击量计算分布---------------------------- Ip_s
select Ip_s,count(*),
(count(*)+0.0)/(select count(*) from _Gb_ns_traffic where len(Http_uri)>2) 
from _Gb_ns_traffic
where len(Http_uri)>2
group by Ip_s
order by 3 desc
GO
--按照点击量计算分布---------------------------Tcp_s
select Tcp_s,count(*),
(count(*)+0.0)/(select count(*) from _Gb_ns_traffic where len(Http_uri)>2) 
from _Gb_ns_traffic
where len(Http_uri)>2
group by Tcp_s
order by 3 desc
GO
--http://117.135.128.21:8080/images/face/newonline/173-1.gif
--IP地址的ISP名称查询
--http://211.139.150.5:8888/cnservice