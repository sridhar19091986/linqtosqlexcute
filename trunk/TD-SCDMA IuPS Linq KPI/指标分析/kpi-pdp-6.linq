<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>RNC681_2008</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

TimeSpan timer=IuPS_PDPs.Max(e=>e.PacketTime).Value- IuPS_PDPs.Min(e=>e.PacketTime).Value;
var mTime=timer.TotalSeconds;
var tt=IuPS_PDPs.Min(e=>e.PacketTime).Value.ToString()+"~"+IuPS_PDPs.Max(e=>e.PacketTime).Value.ToString()+"~"+mTime.ToString();
tt.Dump();
var total=IuPS_PDPs.Where(e=>e.RAB_AssignmentResponse !=null).Where(e=>e.PDP_Act_Accept ==null).Count();  
var a= from p in IuPS_PDPs.Where(e=>e.RAB_AssignmentResponse !=null).Where(e=>e.PDP_Act_Accept ==null)    
	   group p by p.LAC+"~"+p.SAC into ps
	   select new 
	   {
	   mKey=ps.Key ,
	   mCount=ps.Count(),
	   mRate=(ps.Count()+0.0)/total
	   };
	 a.OrderByDescending(e=>e.mCount).Dump();