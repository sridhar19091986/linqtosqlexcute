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
declare @dbname nvarchar(50)
select   @dbname=db_name()   --SQLServer中获取当前数据库名称
print @dbname
GO
select db_name()  BSC_Name
,CONVERT(VARCHAR(19),DATEADD(hour, -8,min(PacketTime)),21)+'-'+CONVERT(VARCHAR(8),DATEADD(hour, -8,max(PacketTime)),108)  GbStat_time,
datediff(s,min(PacketTime),max(PacketTime)) Stat_time
 from dbo.Gb_ATTACH
GO
select  TCP_Src_Port
,count(*)  PS寻呼量
,(count(*)+0.0)/(select count(*) from dbo.Gb_Paging_PS_Service where TCP_Src_Port is not null) PS寻呼量占比
,(count(*)+0.0)/datediff(s,min(PacketTime),max(PacketTime)) PS寻呼速率
,(count(Paging_PS_Response)+0.0)/count(*) PS寻呼成功率
,avg(Paging_PS_Response_delayFirst) PS寻呼平均时延
from dbo.Gb_Paging_PS_Service
where TCP_Src_Port is not null
group by  TCP_Src_Port
having datediff(s,min(PacketTime),max(PacketTime))>0
order by 2 desc