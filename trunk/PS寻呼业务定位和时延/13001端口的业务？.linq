<Query Kind="Expression">
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

from _ in _Gb_ns_traffics
where _.Tcp_d == "13001" 
select new
{
	_.FileNum,
	_.PacketNum,
	_.BeginFrameNum,
	_.PacketTime,
	_.PacketTimems,
	_.DumpFor,
	_.Ns_traffic,
	_.Ns_traffic_PackLen,
	_.Ns_traffic_MsgType,
	_.Bvci,
	_.Tlli,
	_.Link,
	_.Bssgp,
	_.Http_uri,
	_.Http_host,
	_.Http_x_online,
	_.Wsp_uri,
	_.Ip_s,
	_.Ip_d,
	_.Tcp_s,
	_.Tcp_d,
	_.Udp_s,
	_.Udp_d
}