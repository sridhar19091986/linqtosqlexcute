<Query Kind="SQL">
  <Connection>
    <ID>80d6b436-1f57-4aef-992f-652eb2a986cb</ID>
    <Server>192.168.1.230</Server>
    <Persist>true</Persist>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAVn6xNHxDxUmkwNAvq5q6/wAAAAACAAAAAAADZgAAqAAAABAAAAB5BZfO1dPtRitz4jmIB2IoAAAAAASAAACgAAAAEAAAAItG1PxsLKPX4ARMNpYeLUQIAAAAng3Ubk3jF7QUAAAAuu9g23ZvK2W+dh5vq+U8CkV8ft0=</Password>
    <Database>gb_23A_20100521_11</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

 select * from dbo._Gb_ns_traffic 
 where tlli in (select tlli from dbo.Gb_Paging_PS_Service where  tcp_src_port=80)
 and  ((http_host IS NOT NULL) OR (http_uri IS NOT NULL)  
 OR (http_x_online IS NOT NULL)) 
 and ip_d='10.0.0.172'
 --OR (wsp_uri IS NOT NULL)
 
-- 
--  select * from dbo._Gb_ns_traffic 
-- where tlli in (select tlli from dbo.Gb_Paging_PS_Service where  tcp_src_port=13001)
-- and  ((http_host IS NOT NULL) OR (http_uri IS NOT NULL)  
-- OR (http_x_online IS NOT NULL)) 
-- and ip_d='10.0.0.172'
-- --OR (wsp_uri IS NOT NULL)
-- 
 
 
 