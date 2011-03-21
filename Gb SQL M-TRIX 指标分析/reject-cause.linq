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

--use kpi_23A
set ansi_warnings off

select * from racu

--kpi_18_2010_23A--kpi_gb_23a_20100413
--
--dbo.Gb_ATTACH
--
select Reject_Cause,count(*) reject_sum,
(count(*)+0.0)/(select sum(Attach_Reject) from dbo.Gb_ATTACH) cause_rate
from dbo.Gb_ATTACH
where Attach_Reject is not null
group by Reject_Cause
order by  2 desc
--
--PDP_Act_Reject
--
select Reject_Cause,count(*) reject_sum,
(count(*)+0.0)/(select sum(PDP_Act_Reject) from dbo.Gb_PDP_Activation) cause_rate
from dbo.Gb_PDP_Activation
where PDP_Act_Reject is not null
group by Reject_Cause
order by  2 desc
--
--dbo.Gb_Routing_Area_Update
--
select Reject_Cause,count(*) reject_sum,
(count(*)+0.0)/(select sum(RAU_Reject) from dbo.Gb_Routing_Area_Update) cause_rate
from dbo.Gb_Routing_Area_Update
where RAU_Reject is not null
group by Reject_Cause
order by  2 desc
--
--dbo.Gb_RAU_Reject
--
select Reject_Cause,count(*) reject_sum,
(count(*)+0.0)/(select sum(RAU_Reject) from dbo.Gb_RAU_Reject) cause_rate
from dbo.Gb_RAU_Reject
where RAU_Reject is not null
group by Reject_Cause
order by  2 desc
--
--
--
select * from dbo.Gb_Routing_Area_Update where RAU_Reject is not null
--
select * from dbo.Gb_Paging_PS
--
select * from dbo.Gb_RAU_Reject  where Reject_Cause='Protocol error, unspecified'
--
select *  from dbo.Gb_ATTACH where Reject_Cause='Protocol error, unspecified'
--
select radio_cause,reject_imsi,count(*)
 from dbo.Gb_ATTACH where Reject_Cause='Protocol error, unspecified'
group by radio_cause,reject_imsi order by 3 desc
--

select lac,gmmlac,Reject_Cause,count(*)
 from dbo.rauReject  
--where Reject_Cause='Protocol error, unspecified'
group by lac,gmmlac,Reject_Cause
order by 2 desc
--
select * from dbo.rauReject  where Reject_Cause='Protocol error, unspecified'


select APN,Reject_Cause,count(*) reject_sum,
(count(*)+0.0)/(select sum(PDP_Act_Reject) from dbo.Gb_PDP_Activation) cause_rate
from dbo.Gb_PDP_Activation
where PDP_Act_Reject is not null
group by APN,Reject_Cause
order by  3 desc

select APN,PDP_Act_Accept,count(*)
--,count(*) reject_sum,
--(count(*)+0.0)/(select sum(PDP_Act_Reject) from dbo.Gb_PDP_Activation) cause_rate
from dbo.Gb_PDP_Activation
--where PDP_Act_Accept is not null
group by APN,PDP_Act_Accept
order by  3 desc


GO
--按照小区计算Gb消息占比------BVCI，ns_traffic_PackLen,sum--动态SQL,行列转换
--BVCI，ns_traffic_MsgType,ns_traffic_PackLen
--动态SQL,行列转换
GO
if exists (select * from tempdb..sysobjects where id=object_id('tempdb..#temp'))
drop table #temp
GO
select imsi,
case 
	 when attach_reject is not null  then reject_imsi
	 when attach_accept is not null  then imsi
	 else 'no response'
end 
as msgtype,
ci
into #temp
from dbo.Gb_ATTACH
GO
select * from #temp
GO
select msgtype,imsi,count(*) from #temp group by msgtype,imsi order by 1 desc

select left(reject_imsi,3),count(*) ,
(count(*)+0.0)/(select count(*) from dbo.Gb_ATTACH where attach_reject is not null)
from dbo.Gb_ATTACH where attach_reject is not null
group by left(reject_imsi,3) order by 2 desc

select ci 'attach request ci',count(*) 'left(reject_imsi,3)=310 number',
(count(*)+0.0)/(select count(*) from gb_attach 
where left(reject_imsi,3)='310') 'left(reject_imsi,3)=310 rate'
from gb_attach where left(reject_imsi,3)='310' group by ci 
order by 2 desc

select '460-00-'+lac+'-'+ci 'attach request ci',
count(*) 'left(reject_imsi,3)=310 number',
(count(*)+0.0)/(select count(*) from gb_attach 
where left(reject_imsi,3)='310') 'left(reject_imsi,3)=310 rate'
from gb_attach where left(reject_imsi,3)='310' group by '460-00-'+lac+'-'+ci
order by 2 desc

'460-00-9515-3682'

select * from gb_attach where ci='3970' and left(reject_imsi,3)='310' order by reject_imsi

select * from gb_attach where left(imsi,3)='310'

--List of Mobile Country or Geographical Area Codes (Complement to ITU T Recommendation E.212 (11/98)) (Position on 1 January 2004) 
--http://www.itu.int/itudoc/itu-t/ob-lists/icc/e212_685.html

select distinct reject_imsi from gb_attach where left(reject_imsi ,3)='310'

select distinct imsi from gb_attach where left(imsi,3)='310'


select count(reject_imsi) from gb_attach where left(reject_imsi ,3)='310'

select count(imsi) from gb_attach where left(imsi,3)='310'


select * from dbo.Gb_Routing_Area_Update 
where RAU_Accept is null and rau_reject is null 
order by 2

select rau_type,count(*) 
from dbo.Gb_Routing_Area_Update
where RAU_Accept is null and rau_reject is null
group by rau_type
order by 2 desc


select ci,count(*) 
from dbo.Gb_Routing_Area_Update
where RAU_Accept is null and rau_reject is null
group by ci
order by 2 desc


select * from dbo.Gb_Routing_Area_Update 
where RAU_Request_Repeat is not null 
order by 2

--pdp激活时延的处理
GO
select * from Gb_PDP_Activation 
where  PDP_Request_Repeat is null 
order by PDP_Act_Accept_delayFirst desc

GO
select * from Gb_PDP_Activation 
where  PDP_Request_Repeat is not null 
order by PDP_Act_Accept_delayFirst desc

GO
select * from Gb_PDP_Activation 
where  RADIO_Status is not null
order by PDP_Act_Accept_delayFirst desc
GO

select  * from Gb_Routing_Area_Update
GO

select  MS_EDGE_Class ,RAU_Accept ,COUNT(*)
 from Gb_Routing_Area_Update
 group by MS_EDGE_Class ,RAU_Accept
 order by 3
 GO
 
 select  MS_GPRS_Class ,RAU_Accept ,COUNT(*)
 from Gb_Routing_Area_Update
 group by MS_GPRS_Class,RAU_Accept
 order by 3
 GO
 
 select  MS_EDGE_Class ,RAU_Request_Repeat,COUNT(*)
 from Gb_Routing_Area_Update
 group by MS_EDGE_Class ,RAU_Request_Repeat
 order by 2 desc
 go
 
 select  MS_GPRS_Class ,RAU_Accept ,COUNT(*)
 from Gb_Routing_Area_Update
 group by MS_GPRS_Class,RAU_Accept
 order by 3
 GO
 
 select * from RACU
 go
 
 select cause ,count(*) from RACU group by cause order by 2 desc
 go
 
 select * from rau
 go
 
 select lac,olac,rau_accept,COUNT(*) from rau group by lac,olac,rau_accept order by 4 desc
 go
 
 select * from rau where olac='10167' and rau_accept is null
 go
 
 select * from Gb_RAU_Reject
 go
 
 select reject_cause,COUNT(*)
 ,(COUNT(*)+0.0)/(select COUNT(*) from rau  where reject_cause is not null)
  from rau
 where reject_cause is not null
 group by reject_cause order by 2 desc
 go
 
 select * from rau where reject_cause='Protocol error, unspecified'
 
  
 select * from rau where reject_cause='Implicitly detached'