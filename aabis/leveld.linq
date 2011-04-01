<Query Kind="Statements">
  <Connection>
    <ID>a227fb82-8e77-4297-a037-c08cd4b366e6</ID>
    <Persist>true</Persist>
    <Server>.\SQLEXPRESS</Server>
    <Database>master</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

var GridLevelD=from p in Gis_mrs.Take(5000)
             
               group p by new {p.Gridleft,p.Gridbottom,p.Gridright,p.Gridtop}
			   into ttt
			     let dtxna=ttt.Where(e=>e.Updtx!=null).Count()
			   select new 
			   {
			   k=ttt.Key,
			   leveld= 
			   1.0*(ttt.Where(e=>e.Updtx==0).Sum(e=>e.Down_full_rxlev-110)+
			        ttt.Where(e=>e.Updtx==1).Sum(e=>e.Down_sub_rxlev-110))/dtxna
			   };
			   
GridLevelD.Where(e=>e.leveld !=null).Dump();