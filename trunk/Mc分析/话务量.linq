<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Database>mc_sz04a</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

var total=from p in WClearRequests
          where  p.Lac=="10144" || p.Lac_target =="10144"
		  select p;

var sdcch=total.Where(e=>e.Assignment_Complete==null &&  e.Lac_target !=null).Where(e=>e.Released !=null).Sum(e=>(e.Released_delayFirst+0.0)/(1000*3600));
var tch=total.Where(e=>e.Assignment_Complete!=null || e.Lac_target !=null ).Where(e=>e.Released !=null).Sum(e=>(e.Released_delayFirst+0.0)/(1000*3600));

sdcch.Dump();
tch.Dump();

//var sdcch=total.Where(e=>e.Assignment_Complete==null &&  e.Lac_target !=null);
//var tch=total.Where(e=>e.Assignment_Complete!=null || e.Lac_target !=null );
//double? sdErl=0;
//double? tchErl=0;
//foreach(var sd in sdcch)
//  if(sd.Released !=null)
//       sdErl+=((sd.Released_delayFirst+0.0)/1000);
//foreach(var t in tch)
//{
// if(t.Released !=null && t.Assignment_Complete!=null)
//   {
//       tchErl+=(t.Released_delayFirst-t.Assignment_Complete_delayFirst+0.0)/1000;
//	   sdErl+=(t.Assignment_Complete_delayFirst+0.0)/1000;
//    }
// if(t.Released !=null && t.Lac_target !=null)
//  tchErl+=(t.Released_delayFirst+0.0)/1000;
//}
//(sdErl/3600).Dump();
//(tchErl/3600).Dump();