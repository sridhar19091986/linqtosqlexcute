<Query Kind="Statements">
  <Connection>
    <ID>a227fb82-8e77-4297-a037-c08cd4b366e6</ID>
    <Persist>true</Persist>
    <Server>.\SQLEXPRESS</Server>
    <Database>RNC681_2008</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

TimeSpan timer=IuPS_PDPs.Max(e=>e.PacketTime).Value- IuPS_PDPs.Min(e=>e.PacketTime).Value;
var mTime=timer.TotalSeconds;
var tt=IuPS_PDPs.Min(e=>e.PacketTime).Value.ToString()+"~"+IuPS_PDPs.Max(e=>e.PacketTime).Value.ToString()+"~"+mTime.ToString();
tt.Dump();

var a= from p in IuPS_PDPs.Where (e=>e.DumpFor =="EndFlowByFlowDesigner")
	group p by p.DumpFor        into ps
	orderby ps.Key
	select new {
	mKey=ps.Key ,
//	mStartEnd=ps.Min(e=>e.PacketTime.Value  )+"~"+ps.Max  (e=>e.PacketTime.Value) ,
	mName="PDP",
	mRequest=ps.Sum (e=>e.PDP_Act_Request ),
	mSuccess= ps.Sum (e=>e.PDP_Act_Accept ),
	mSuccRate=(ps.Sum (e=>e.PDP_Act_Accept )+0.0)/ps.Sum (e=>e.PDP_Act_Request ),
	mDelay=ps.Where (e=>e.PDP_Act_Accept !=null).Average (e=>e.PDP_Act_Accept_delayFirst -e.PDP_Act_Request_delayFirst )
	};
	a.OrderBy (e=>e.mSuccRate ).Dump ();