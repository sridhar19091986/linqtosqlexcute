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
  <Namespace>System.Security</Namespace>
  <Namespace>System.Security.Permissions</Namespace>
  <Namespace>System.Security.Principal</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

ImeiTypes.Where (e=>e.Imei .IndexOf ("e+007")!=-1).Dump ();


-- Region Parameters
DECLARE @p0 NVarChar(5) SET @p0 = 'e+007'
DECLARE @p1 Int SET @p1 = -1
-- EndRegion
--SELECT [t0].[imeiType_id] AS [ImeiType_id], [t0].[imei] AS [Imei], 
--[t0].[imeiFactory] AS [ImeiFactory], [t0].[imeiModel] AS [ImeiModel], [t0].[imeiClass] AS [ImeiClass]
delete 
FROM [imeiType] 
WHERE (
	(CASE 
		WHEN (DATALENGTH(@p0) / 2) = 0 THEN 0
		ELSE CHARINDEX(@p0, [imei]) - 1
	 END)) <> @p1
