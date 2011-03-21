<Query Kind="Statements">
  <Connection>
    <ID>c3e164c7-9cf2-4b0d-aa90-72b91ec7a949</ID>
    <Persist>true</Persist>
    <Server>192.168.1.6</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAN96yok7v7Eyv6CwMzOjNtQAAAAACAAAAAAADZgAAqAAAABAAAACVwTFsk9dhMIn5i8ZFS9d4AAAAAASAAACgAAAAEAAAANmSpZffgSRmeLtQTcaKcpwIAAAAOuxdiYNQsLIUAAAAUdmDuQYJvN2uV/Lw/N9gMoxASHY=</Password>
    <Database>sz_46C_20101012</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

TimeSpan timer=Gb_Attachs.Max(e=>e.PacketTime).Value- Gb_Attachs.Min(e=>e.PacketTime).Value;
var mTime=timer.TotalSeconds;
var tt=Gb_Attachs.Min(e=>e.PacketTime).Value.ToString()+"~"+Gb_Attachs.Max(e=>e.PacketTime).Value.ToString()+"~"+mTime.ToString();
tt.Dump();
//var totalAttatchRequest=Gb_Attachs.Where(e=>e.LAC==e.Gmm_lac).Sum(e=>e.Attach_Request);
var totalAttatchRequest=Gb_Attachs.Where(e=>e.Attach_Request!=null).Count();
var ps= from p in Gb_Attachs
//    where p.TLLI!=null && p.PTMSI !=null
where p.Authentication_Request==null
//where p.Attach_Request_Repeat!=null
    where p.PTMSI==null
	group p by p.Reject_Cause into psbyfilenum
	select new {
	Reject_Cause=psbyfilenum.Key ,
	mAttachRequest= psbyfilenum.Where(e=>e.Attach_Request!=null).Count(),
	mAttachRequestRate=(psbyfilenum.Where(e=>e.Attach_Request!=null).Count()+0.0)/totalAttatchRequest,
	mAttachRepeatRate=(psbyfilenum.Where(e=>e.Attach_Request_Repeat!=null).Count()+0.0)/totalAttatchRequest,
	mAttachSuccessRate= (psbyfilenum.Sum(e=>e.Attach_Complete)+0.0)/psbyfilenum.Where(e=>e.Attach_Request!=null).Count(),
	mAttachComplete= psbyfilenum.Average(e =>e.Attach_Complete_delayFirst)
	};
	ps.OrderByDescending(e=>e.mAttachRequest).Dump ();