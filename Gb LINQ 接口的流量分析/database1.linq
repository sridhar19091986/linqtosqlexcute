<Query Kind="SQL">
  <Connection>
    <ID>27d89e94-3492-4017-9db8-6f859e59aa6c</ID>
    <Server>localhost</Server>
    <Persist>true</Persist>
    <Database>IP_Stream</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.Dialog.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;System.Windows.Forms.dll</Reference>
  <Reference>E:\linq to sql\HtmlAgilityPack\HtmlAgilityPack.1.4.0\HtmlAgilityPack.dll</Reference>
  <GACReference>Microsoft.SqlServer.Management.MultiServerConnection, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <GACReference>Microsoft.SqlServer.Management.CollectorEnum, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>HtmlAgilityPack</Namespace>
  <Namespace>System.Net</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

if exists (select 1
			from  sysobjects
		   where  id = object_id('dbo.mLocatingType')
			and   type = 'U')
   drop table dbo.mLocatingType
go
--//每1条消息 帧号、小区、业务、终端、包大小
/*==============================================================*/
/* Table: mLocatingType                                       */
/*==============================================================*/
create table dbo.mLocatingType (
   mLocatingType_id    numeric              identity(1, 1),
--   //消息帧号，方便回查
   fileNum        int                  null,
   frame        int                  null,
-- //小区类型
   bvci  nvarchar(50)         collate Chinese_PRC_CI_AS null,
   lacCI  nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ciConverType nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ciConverClass nvarchar(5)         collate Chinese_PRC_CI_AS null,
-- //具体用户
   tlli  nvarchar(50)         collate Chinese_PRC_CI_AS null,
   imsi  nvarchar(50)         collate Chinese_PRC_CI_AS null,
-- //用户类型，上网本、终端	  
   imei  nvarchar(50)         collate Chinese_PRC_CI_AS null,
   msimeiType  nvarchar(500)         collate Chinese_PRC_CI_AS null,
   msimeiClass  nvarchar(5)         collate Chinese_PRC_CI_AS null,
--   //业务类型
   trafficType nvarchar(50)         collate Chinese_PRC_CI_AS null,
   uriType  nvarchar(500)         collate Chinese_PRC_CI_AS null,
   protocolType  nvarchar(50)         collate Chinese_PRC_CI_AS null,
   portType  nvarchar(50)         collate Chinese_PRC_CI_AS null,
   responseType nvarchar(50)         collate Chinese_PRC_CI_AS null,
--   //消息长度
    mLen        int                  null,
   constraint PK_mLocatingType_id primary key nonclustered (mLocatingType_id )
		 on "PRIMARY"
)
on "PRIMARY"
go