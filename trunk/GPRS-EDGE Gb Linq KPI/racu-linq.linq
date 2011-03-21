<Query Kind="Statements">
  <Connection>
    <ID>69c6882b-052d-4d33-8482-fc44aaa595ea</ID>
    <Server>192.168.1.6</Server>
    <Persist>true</Persist>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAN96yok7v7Eyv6CwMzOjNtQAAAAACAAAAAAADZgAAqAAAABAAAABPq8wPbMN4vRjVWN5aY3GzAAAAAASAAACgAAAAEAAAAPUA4cdtoD3riRYDMCFP6wIIAAAAfTDM16k7waAUAAAAKpDjl8vZLqArCW3VTCUIjGdOqPE=</Password>
    <Database>sz_23B_20100920</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>


this.Connection.Database.Dump();

var _min=_Gb_ns_traffics.Min(e=>e.PacketTime).Value;
var min=_min>System.DateTime.Now.AddDays(-300)?_min:_Gb_ns_traffics.SingleOrDefault(e=>e.PacketNum==0 && e.FileNum==0).PacketTime.Value;
var max=_Gb_ns_traffics.Max(e=>e.PacketTime).Value;
TimeSpan timer=max- min;
var mTime=timer.TotalSeconds;
var tt=min.ToString()+"~"+max.ToString()+"~"+mTime.ToString();
tt.Dump();

var ps=from p in _Gb_ns_traffics
       group p by new {p.Tlli ,p.Ns_traffic_MsgType} into temp
	   select  temp.Key;
	   
ps.Count().Dump();

var tlli=ps.Select(p=>p.Tlli).Distinct().Count();

var mess=ps.Where(e=>e.Ns_traffic_MsgType=="BSSGP.RA-CAPABILITY-UPDATE").Select(e=>e.Tlli).Distinct().Count();

var rate=(mess+0.0)/tlli;

tlli.Dump();mess.Dump();rate.Dump();
