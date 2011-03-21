<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>mc_sz04a</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

var paging=from p in WTchAssignments
           where p.Lac=="10144"
           group p by p.PacketTime.Value.Date.ToString()+" "+p.PacketTime.Value.Hour.ToString() into tt
		   select new {
		   tt.Key,	  
		   TCH请求次数=tt.Sum(e=>e.Assignment_Request),
           TCH占用次数=tt.Sum(e=>e.Assignment_Complete),
           TCH占用成功率=(tt.Sum(e=>e.Assignment_Complete)+0.0)/tt.Sum(e=>e.Assignment_Request)
		   };

paging.Dump();