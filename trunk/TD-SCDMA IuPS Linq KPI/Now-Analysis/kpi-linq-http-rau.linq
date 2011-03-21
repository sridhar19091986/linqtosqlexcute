<Query Kind="Statements">
  <Connection>
    <ID>a227fb82-8e77-4297-a037-c08cd4b366e6</ID>
    <Persist>true</Persist>
    <Server>.\SQLEXPRESS</Server>
    <Database>RNC681_2008</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

TimeSpan timer=IuPS_RAUs.Max(e=>e.PacketTime).Value- IuPS_RAUs.Min(e=>e.PacketTime).Value;
var mTime=timer.TotalSeconds;
var tt=IuPS_RAUs.Min(e=>e.PacketTime).Value.ToString()+"~"+IuPS_RAUs.Max(e=>e.PacketTime).Value.ToString()+"~"+mTime.ToString();
tt.Dump();
var a= from p in IuPS_RAUs.Where (e=>e.DumpFor =="EndFlowByFlowDesigner")
	group p by p.DumpFor into ps
	orderby ps.Key 
	select new {
	mFile=ps.Key ,
	mName="RAU",
	mRequest=ps.Sum (e=>e.Routing_Area_Update_Request ),
	mSuccess= ps.Sum (e=>e.Routing_Area_Update_Complete ),
	mSuccRate=(ps.Sum (e=>e.Routing_Area_Update_Complete  )+0.0)/ps.Sum (e=>e.Routing_Area_Update_Request  ),
	mDelay= ps.Average  (e=>e.Routing_Area_Update_Complete_delayFirst)
	};
	a.OrderBy (e=>e.mSuccRate ).Dump ();
	