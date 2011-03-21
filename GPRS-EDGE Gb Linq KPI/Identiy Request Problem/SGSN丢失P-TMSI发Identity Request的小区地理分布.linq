<Query Kind="Statements">
  <Connection>
    <ID>c3e164c7-9cf2-4b0d-aa90-72b91ec7a949</ID>
    <Persist>true</Persist>
    <Server>192.168.1.16</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAN96yok7v7Eyv6CwMzOjNtQAAAAACAAAAAAADZgAAqAAAABAAAACVwTFsk9dhMIn5i8ZFS9d4AAAAAASAAACgAAAAEAAAANmSpZffgSRmeLtQTcaKcpwIAAAAOuxdiYNQsLIUAAAAUdmDuQYJvN2uV/Lw/N9gMoxASHY=</Password>
    <Database>kpi_14A_20100820_12</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

TimeSpan timer=Gb_Attachs.Max(e=>e.PacketTime).Value- Gb_Attachs.Min(e=>e.PacketTime).Value;
var mTime=timer.TotalSeconds;
var tt=Gb_Attachs.Min(e=>e.PacketTime).Value.ToString()+"~"+Gb_Attachs.Max(e=>e.PacketTime).Value.ToString()+"~"+mTime.ToString();
tt.Dump();
var totalAttatchRequest=Gb_Attachs.Where(e=>e.LAC==e.Gmm_lac).Where(e=>e.Request_id2=="IMSI").Sum(e=>e.Attach_Request);
//var totalAttatchRequest=Gb_Attachs.Sum(e=>e.Attach_Request);
var ps= from p in Gb_Attachs
    where p.LAC==p.Gmm_lac
where p.Request_id2=="IMSI"
	group p by p.LAC+"~"+p.CI   into psbyfilenum
	select new {
	mIdentityReuqest=psbyfilenum.Key ,
	mAttachRequest= psbyfilenum.Sum(e=>e.Attach_Request),
	mAttachRequestRate=(psbyfilenum.Sum(e=>e.Attach_Request)+0.0)/totalAttatchRequest,
	mAttachSuccessRate= (psbyfilenum.Sum(e=>e.Attach_Complete)+0.0)/psbyfilenum.Sum(e=>e.Attach_Request),
	mAttachComplete= psbyfilenum.Average(e =>e.Attach_Complete_delayFirst)
	};
	ps.OrderByDescending(e=>e.mAttachRequest).Dump ();