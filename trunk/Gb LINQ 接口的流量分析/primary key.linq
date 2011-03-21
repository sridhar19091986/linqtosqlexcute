<Query Kind="SQL">
  <Connection>
    <ID>69c6882b-052d-4d33-8482-fc44aaa595ea</ID>
    <Server>192.168.1.4</Server>
    <Persist>true</Persist>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAN96yok7v7Eyv6CwMzOjNtQAAAAACAAAAAAADZgAAqAAAABAAAADGarvgRIQKFUM/WjBfyyxrAAAAAASAAACgAAAAEAAAAJ6jZzHbxzvi9SJa03W825cIAAAAclJvhqVcaAwUAAAA17I1+aB2/nENjlw0DfUGTav7HMs=</Password>
    <Database>IP_stream_37b_20100521_new4</Database>
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

--alter table IP_stream add sid int identity(1,1)  null;
--alter   table  IP_stream   drop   constraint   sid_pk
go
--alter table IP_stream drop column sid
go
alter table IP_stream alter column FileNum int not null
go
alter table IP_stream alter column PacketNum int not null
go
alter table IP_stream add constraint sid_pk primary key(FileNum,PacketNum)
go

--delete from IP_stream where FileNum=1

--ALTER TABLE Persons
--ADD CONSTRAINT pk_PersonID PRIMARY KEY (Id_P,LastName)