<Query Kind="Statements">
  <Connection>
    <ID>a227fb82-8e77-4297-a037-c08cd4b366e6</ID>
    <Persist>true</Persist>
    <Server>.\SQLEXPRESS</Server>
    <Database>mc_sz04a</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

var paging=from p in WHandoverRequireds
           where p.Handover_Cause=="Directed Retry" 
		   where  p.Cell_lac=="10144" || p.Cell_lac_target =="10144" 
           group p by p.PacketTime.Value.Date.ToString()+" "+p.PacketTime.Value.Hour.ToString() into tt
		   select new {
		   tt.Key,	  
		   TCH请求次数DirectedRetry=tt.Sum(e=>e.Handover_Required),
           TCH占用次数DirectedRetry=tt.Sum(e=>e.Handover_Command),
           TCH占用DirectedRetry成功率=(tt.Sum(e=>e.Handover_Command)+0.0)/tt.Sum(e=>e.Handover_Required)
		   };

paging.Dump();