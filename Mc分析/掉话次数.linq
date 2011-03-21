<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Database>mc_sz04a</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

//WClearRequests.Where(e=>e.Clear_Request !=null).Take(50).Dump();
//WClearRequests.Where(e=>e.Clear_Request !=null).Count().Dump();

//WClearRequests.Select(e=>e.Clear_Command_Cause).Distinct().Dump();

var paging=from p in WClearRequests.Where(e=>e.Released !=null).Where(e=>e.Clear_Request !=null 
||(e.Clear_Command_Cause !=null 
   && e.Clear_Command_Cause!="Handover successful" 
   && e.Clear_Command_Cause!="Call control"
   && e.Clear_Command_Cause!="Radio interface failure, reversion to old channel"))
           //where p.Clear_Request !=null
		   where  p.Lac=="10144" || p.Lac_target =="10144" 
           group p by p.PacketTime.Value.Date.ToString()+" "+p.PacketTime.Value.Hour.ToString() into tt
		   select new {
		   tt.Key,	  
		   SDCCH掉话次数=tt.Where(e=>e.Assignment_Complete ==null && e.Lac_target ==null).Sum(e=>e.Msg),
		   SDCCH掉话率=(tt.Where(e=>e.Assignment_Complete ==null && e.Lac_target ==null).Sum(e=>e.Msg)+0.0)/WClearRequests.Where(e=>e.Released !=null).Where(e=>e.Assignment_Complete ==null && e.Lac_target ==null).Sum(e=>e.Msg),
           TCH掉话次数=tt.Where(e=>e.Assignment_Complete !=null || e.Lac_target !=null).Sum(e=>e.Msg),
		   TCH掉话率=(tt.Where(e=>e.Assignment_Complete !=null || e.Lac_target !=null).Sum(e=>e.Msg)+0.0)/WClearRequests.Where(e=>e.Released !=null).Where(e=>e.Assignment_Complete!=null || e.Lac_target !=null).Sum(e=>e.Msg)
		   };

paging.Dump();