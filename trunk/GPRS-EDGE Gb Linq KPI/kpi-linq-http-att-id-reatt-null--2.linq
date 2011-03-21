<Query Kind="Statements">
  <Connection>
    <ID>69c6882b-052d-4d33-8482-fc44aaa595ea</ID>
    <Server>192.168.1.16</Server>
    <Persist>true</Persist>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAN96yok7v7Eyv6CwMzOjNtQAAAAACAAAAAAADZgAAqAAAABAAAABPq8wPbMN4vRjVWN5aY3GzAAAAAASAAACgAAAAEAAAAPUA4cdtoD3riRYDMCFP6wIIAAAAfTDM16k7waAUAAAAKpDjl8vZLqArCW3VTCUIjGdOqPE=</Password>
    <Database>kpi_21A_20100819_16</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

TimeSpan timer=Gb_ATTACHes.Max(e=>e.PacketTime).Value- Gb_ATTACHes.Min(e=>e.PacketTime).Value;
var mTime=timer.TotalSeconds;
var tt=Gb_ATTACHes.Min(e=>e.PacketTime).Value.ToString()+"~"+Gb_ATTACHes.Max(e=>e.PacketTime).Value.ToString()+"~"+mTime.ToString();
tt.Dump();
var totalAttatchRequest=Gb_ATTACHes.Sum(e=>e.Attach_Request);
var ps= from p in Gb_ATTACHes
//	group p by p.LAC+"~"+p.CI   into psbyfilenum
	group p by p.  into psbyfilenum
	select new {
	mIdentityReuqest=psbyfilenum.Key ,
	mAttachRequest= psbyfilenum.Sum(e=>e.Attach_Request),
	mAttachRequestRate=(psbyfilenum.Sum(e=>e.Attach_Request)+0.0)/totalAttatchRequest,
	mAttachSuccessRate= (psbyfilenum.Sum(e=>e.Attach_Complete)+0.0)/psbyfilenum.Sum(e=>e.Attach_Request),
	mAttachComplete= psbyfilenum.Average(e =>e.Attach_Complete_delayFirst)
	};
	ps.OrderByDescending(e=>e.mAttachRequest).Dump ();