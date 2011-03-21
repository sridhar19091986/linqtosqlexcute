<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Database>mc_sz04a</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

//WHandoverRequests.Take(50).Dump();
//WHandoverPerforms.Take(50).Dump();
WHandoverPerforms.Select(e=>e.Handover_Cause).Distinct().Dump();
WHandoverPerforms.Where(e=>e.PacketTime.Value.Hour.ToString()=="20").Where(e=>e.LAC=="10144").Count().Dump();
//WHandoverPerforms.Where(e=>e.FileNum==0).Where(e=>e.Handover_Cause=="Handover successful").Take(50).Dump();
//var paging=from p in WHandoverPerforms
//		   //where p.Handover_Cause=="Directed Retry" 
//		   where  p.LAC=="10144" 
//		   group p by p.PacketTime.Value.Date.ToString()+" "+p.PacketTime.Value.Hour.ToString() into tt
//		   select new {
//		   tt.Key,	  
//		   TCH请求次数切换=tt.Sum(e=>e.Handover_Performed),
//		   TCH占用次数切换=tt.Where(e=>e.Handover_Cause!="Handover successful").Sum(e=>e.Handover_Performed),
//		   TCH占用成功率切换=(tt.Where(e=>e.Handover_Cause!="Handover successful").Sum(e=>e.Handover_Performed)+0.0)/tt.Sum(e=>e.Handover_Performed)
//		   };
//
//paging.Dump();