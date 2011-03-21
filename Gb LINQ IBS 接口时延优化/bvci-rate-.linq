<Query Kind="Statements">
  <Connection>
    <ID>f0ade2a2-7e91-40d9-9e5b-cfaeb554107a</ID>
    <Persist>true</Persist>
    <Server>192.168.1.4</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAN96yok7v7Eyv6CwMzOjNtQAAAAACAAAAAAADZgAAqAAAABAAAADFxU8yZyoUZYE+URPxWKLEAAAAAASAAACgAAAAEAAAAMXRuwOErJdyD+BjiLyVhxkIAAAAW3dTush/8OwUAAAAg4Q1V4BortthI3biT6zSV2CIR5k=</Password>
    <Database>IP_stream_21A_20100521</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

//this.Connection.Dump();

TimeSpan timer= IP_streams.Max(e=>e.PacketTime).Value- IP_streams.Min(e=>e.PacketTime).Value;
var mTime=timer.TotalSeconds;
var ip=from p in IP_streams
       group p by p.Bvci into ps
	   select new
	   {  key=ps.Key,	
	 	  mSum=ps.Sum(e=>e.Ip_length),
		  mR=(ps.Sum(e=>e.Ip_length)*8/1024)/mTime
	   };
	   
	   ip.Dump();