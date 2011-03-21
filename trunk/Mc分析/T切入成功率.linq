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
WHandoverRequests.Select(e=>e.Bssmap_cause).Distinct().Dump();
var paging=from p in WHandoverRequests
		   //where p.Handover_Cause=="Directed Retry" 
		   where  p.Target_LAC=="10144" 
		   group p by p.PacketTime.Value.Date.ToString()+" "+p.PacketTime.Value.Hour.ToString() into tt
		   select new {
		   tt.Key,	  
		   TCH请求次数切入=tt.Sum(e=>e.Handover_Request),
		   TCH占用次数切入=tt.Where(e=>e.Bssmap_cause=="Handover successful").Sum(e=>e.Handover_Request),
		   TCH占用成功率切入=(tt.Where(e=>e.Bssmap_cause=="Handover successful").Sum(e=>e.Handover_Request)+0.0)/tt.Sum(e=>e.Handover_Request)
		   };

paging.Dump();