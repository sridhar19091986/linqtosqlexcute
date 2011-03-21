<Query Kind="SQL">
  <Connection>
    <ID>e5f5449b-aa54-4234-bda6-c0296770953c</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>fcm_23A_Gb</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>


select COUNT (*) from dbo.A_Originating_Call

select * from dbo.A_Originating_Call where Setup is not null

select * from dbo.A_Originating_Call where Assignment_Request is not null

select * from dbo.A_Originating_Call where Assignment_Complete is not null

select * from dbo.A_Originating_Call where Alerting is not null 

select * from dbo.A_Originating_Call where Assignment_Complete is not null and Alerting is null