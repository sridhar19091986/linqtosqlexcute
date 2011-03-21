<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>RNC681_2008</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

TimeSpan timer=Iu_RABs.Max(e=>e.PacketTime).Value- Iu_RABs.Min(e=>e.PacketTime).Value;
var mTime=timer.TotalSeconds;
var tt=Iu_RABs.Min(e=>e.PacketTime).Value.ToString()+"~"+Iu_RABs.Max(e=>e.PacketTime).Value.ToString()+"~"+mTime.ToString();
tt.Dump();
var totalATTRequest=Iu_RABs.Where (e=>e.DumpFor =="EndFlowByFlowDesigner").Sum(e=>e.RAB_AssignmentRequest);
var a= from p in Iu_RABs.Where (e=>e.DumpFor =="EndFlowByFlowDesigner").Where(e=>e.Iu_ReleaseComplete ==null)
	group p by  p.TrafficHandlingPriority into ps
	select new {
	mKey=ps.Key ,
	mRABRequest=ps.Sum (e=>e.RAB_AssignmentRequest),
	mRABcomplete=ps.Sum(e=>e.RAB_AssignmentResponse),
	mRABRequestRate=(ps.Sum(e=>e.RAB_AssignmentRequest)+0.0)/totalATTRequest,
	mRABSuccRate= (ps.Sum (e=>e.RAB_AssignmentResponse)+0.0)/ps.Sum (e=>e.RAB_AssignmentRequest),
    mRABDelay= ps.Average(e =>e.RAB_AssignmentResponse_delayFirst)
	};
	a.OrderByDescending(e=>e.mRABDelay).Dump();