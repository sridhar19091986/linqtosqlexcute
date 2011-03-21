<Query Kind="Statements">
  <Connection>
    <ID>c3e164c7-9cf2-4b0d-aa90-72b91ec7a949</ID>
    <Persist>true</Persist>
    <Server>192.168.1.16</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAN96yok7v7Eyv6CwMzOjNtQAAAAACAAAAAAADZgAAqAAAABAAAACVwTFsk9dhMIn5i8ZFS9d4AAAAAASAAACgAAAAEAAAANmSpZffgSRmeLtQTcaKcpwIAAAAOuxdiYNQsLIUAAAAUdmDuQYJvN2uV/Lw/N9gMoxASHY=</Password>
    <Database>RNC681_20100720_11_28_06</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var ps= from p in Gb_ATTACHes
	group p by p.FileNum into psbyfilenum
	orderby psbyfilenum.Key 
	select new {
	mFile=psbyfilenum.Key ,
	//	mTime=psbyfilenum.Where (e=>e.FileNum ==psbyfilenum.Key).Select(e=>e.PacketTime.Value.AddHours(8)).Min (),
		mTime=psbyfilenum.Where (e=>e.FileNum ==psbyfilenum.Key).Select(e=>e.PacketTime.Value.AddHours(-8)).Min (),
	mName="Attach_Complete_idNull_ReATTnull",
	mMessage= psbyfilenum.Where (e=>e.FileNum ==psbyfilenum.Key).Where (e=>e.Attach_Complete!=null&&e.Attach_Request_Repeat==null && e.ID_REQ ==null).Select(e=>e.Attach_Complete).Sum (),
	mSuccess= (psbyfilenum.Where (e=>e.FileNum ==psbyfilenum.Key).Where (e=>e.Attach_Complete!=null&&e.Attach_Request_Repeat==null && e.ID_REQ ==null).Select(e=>e.Attach_Complete).Sum ()+0.0)/psbyfilenum.Where (e=>e.FileNum ==psbyfilenum.Key).Where (e=>e.Attach_Request_Repeat==null && e.ID_REQ ==null).Select(e=>e.Attach_Request).Sum (),
	mDelay= psbyfilenum.Where (e=>e.FileNum ==psbyfilenum.Key).Where (e=>e.Attach_Complete!=null&&e.Attach_Request_Repeat==null && e.ID_REQ ==null).Select(e =>e.Attach_Complete_delayFirst).Average ()
	};
	ps.Dump ();