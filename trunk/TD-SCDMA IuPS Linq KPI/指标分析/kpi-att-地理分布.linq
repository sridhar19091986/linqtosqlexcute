<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>RNC681_2008</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

TimeSpan timer=IuPS_ATTs.Max(e=>e.PacketTime).Value- IuPS_ATTs.Min(e=>e.PacketTime).Value;
var mTime=timer.TotalSeconds;
var tt=IuPS_ATTs.Min(e=>e.PacketTime).Value.ToString()+"~"+IuPS_ATTs.Max(e=>e.PacketTime).Value.ToString()+"~"+mTime.ToString();
tt.Dump();
var totalATTRequest=IuPS_ATTs.Where (e=>e.DumpFor =="EndFlowByFlowDesigner").Where(e=>e.ID_REQ!=null).Sum(e=>e.Attach_Request);
var a= from p in IuPS_ATTs
.Where (e=>e.DumpFor =="EndFlowByFlowDesigner")
.Where(e=>e.ID_REQ!=null)
	group p by p._3G_LAC+"~"+p.SAC into ps
	select new {
	mKey=ps.Key ,
	mATTRequest=ps.Sum (e=>e.Attach_Request),
	mATTRequestRate=(ps.Sum(e=>e.Attach_Request)+0.0)/totalATTRequest,
	mATTSuccRate= (ps.Sum (e=>e.Attach_Complete)+0.0)/ps.Sum (e=>e.Attach_Request),
	mATTDelay= ps.Average(e =>e.Attach_Complete_delayFirst)
	};
	a.OrderByDescending(e=>e.mATTRequestRate).Dump();