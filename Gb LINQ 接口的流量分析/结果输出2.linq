<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>23A</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

//IP_streams.Where  (e=>e.Link =="down").Select (e=>e.Tcp_s).Distinct ()
//var a=from p in MLocatingTypes
//      select p.CiConverType  ;
//	  
//	  a.Distinct ().Dump ();
//	  
////	  a.OrderByDescending (e=>e.mKey).Dump ();
//
//var b=from p in MLocatingTypes
//	  select p.PortType   ;
//	  
//	  b.Distinct ().Dump ();
//	  
//	   MLocatingTypes.Where(e=>e.TrafficType=="p2p").Dump ();
	   
	    MLocatingTypes.Count().Dump ();
	   IP_streams.Count().Dump();