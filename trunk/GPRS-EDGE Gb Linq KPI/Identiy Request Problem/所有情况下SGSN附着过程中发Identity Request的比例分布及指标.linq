<Query Kind="Statements">
  <Connection>
    <ID>69c6882b-052d-4d33-8482-fc44aaa595ea</ID>
    <Server>192.168.1.6</Server>
    <Persist>true</Persist>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAN96yok7v7Eyv6CwMzOjNtQAAAAACAAAAAAADZgAAqAAAABAAAABPq8wPbMN4vRjVWN5aY3GzAAAAAASAAACgAAAAEAAAAPUA4cdtoD3riRYDMCFP6wIIAAAAfTDM16k7waAUAAAAKpDjl8vZLqArCW3VTCUIjGdOqPE=</Password>
    <Database>sz_46C_20101012</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

TimeSpan timer=Gb_Attachs.Max(e=>e.PacketTime).Value- Gb_Attachs.Min(e=>e.PacketTime).Value;
var mTime=timer.TotalSeconds;
var tt=Gb_Attachs.Min(e=>e.PacketTime).Value.ToString()+"~"+Gb_Attachs.Max(e=>e.PacketTime).Value.ToString()+"~"+mTime.ToString();
tt.Dump();
//var totalAttatchRequest=Gb_Attachs.Where(e=>e.LAC==e.Gmm_lac).Sum(e=>e.Attach_Request);
var totalAttatchRequest=Gb_Attachs.Sum(e=>e.Attach_Request);
var ps= from p in Gb_Attachs
//    where p.LAC==p.Gmm_lac
////where p.LAC!=null
	group p by p.Pfc_mode  into psbyfilenum
	select new {
	mIdentityReuqest=psbyfilenum.Key ,
	mAttachRequest= psbyfilenum.Sum(e=>e.Attach_Request),
	mAttachRequestRate=(psbyfilenum.Sum(e=>e.Attach_Request)+0.0)/totalAttatchRequest,
	mAttachSuccessRate= (psbyfilenum.Sum(e=>e.Attach_Complete)+0.0)/psbyfilenum.Sum(e=>e.Attach_Request),
	mAttachComplete= psbyfilenum.Average(e =>e.Attach_Complete_delayFirst)
	};
	ps.OrderByDescending(e=>e.mAttachRequest).Dump ();