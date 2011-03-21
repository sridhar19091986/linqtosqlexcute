<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>RNC681_2008</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var a= from p in IuPS_ATTs.Where (e=>e.DumpFor =="EndFlowByFlowDesigner")
    let c=p.Authentication_Request==null ? "auth_req null":"auth_req not null"
	group p by c      into ps
//	orderby ps.Key 
	select new {
	mKey=ps.Key ,
//	mStartEnd=ps.Min(e=>e.PacketTime.Value  )+"~"+ps.Max  (e=>e.PacketTime.Value),
//	mTimer=ps.Max  (e=>e.PacketTime.Value)-ps.Min(e=>e.PacketTime.Value  ),
	mName="Attach",
	mRequest=ps.Sum (e=>e.Attach_Request),
	mSuccess= ps.Sum (e=>e.Attach_Complete),
	mSuccRate= (ps.Sum (e=>e.Attach_Complete)+0.0)/ps.Sum (e=>e.Attach_Request),
	mDelay= ps.Where (e=>e.Attach_Complete !=null).Average(e =>e.Attach_Complete_delayFirst)
	};
	a.Dump ();
//	a.OrderBy (e=>e.mSuccRate ).ThenBy (e=>e.mKey ).Dump ();