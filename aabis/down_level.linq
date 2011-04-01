<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
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