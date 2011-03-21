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

var paging=from p in WLocationUpdates
           where p.LAC =="10144"
		   where p.PacketTime.Value.Hour==20
		   //where p.LAC==null
           //group p by p.PacketTime.Value.Date.ToString()+" "+p.PacketTime.Value.Hour.ToString() into tt
		   group p by p.Clear_Command_Cause   into tt
		   select new {
		   tt.Key,	  
		   位置更新请求次数=tt.Sum(e=>e.LAU_Request),
           位置更新成功次数=tt.Sum(e=>e.LAU_Accept),
           位置更新成功率=(tt.Sum(e=>e.LAU_Accept)+0.0)/tt.Sum(e=>e.LAU_Request)
		   };

//paging.Dump();

paging.OrderBy(e=>e.位置更新成功率).Dump();

//WLocationUpdates.Where(e=>e.FileNum==0).Where(e=>e.LAC=="10144").Where(e=>e.Clear_Command_Cause!=null).Dump();