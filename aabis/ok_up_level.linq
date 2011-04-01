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

var Grid_Level_Up=from p in Gis_mrs
.Select(e=>new {e.Downdtx,e.Up_full_rxlev,e.Up_sub_rxlev,e.Gridleft,e.Gridbottom,e.Gridright,e.Gridtop}).ToList()
             
               group p by new {p.Gridleft,p.Gridbottom,p.Gridright,p.Gridtop}
			   into ttt
			     let dtxna=ttt.Where(e=>e.Downdtx!=null).Count()
			   select new 
			   {
			   k=ttt.Key,
			   leveld= 
			   1.0*(ttt.Where(e=>e.Downdtx==0).Sum(e=>e.Up_full_rxlev -110)+
			        ttt.Where(e=>e.Downdtx==1).Sum(e=>e.Up_sub_rxlev-110))/dtxna
			   };



var tj=from p in Grid_Level_Up
	   group p by p.leveld into ttt
	   select new 
			{
			k=ttt.Key,
			s=ttt.Count(),
			r=ttt.Count()*1.0/Grid_Level_Up.Count()
			};
//tj.Dump();
			   
Grid_Level_Up.Average(e=>e.leveld).Dump();
tj.Where(e=>e.k>=-85).Sum(e=>e.r).Dump();
tj.Where(e=>e.k<-85).Where(e=>e.k>=-95).Sum(e=>e.r).Dump();
tj.Where(e=>e.k<-95).Where(e=>e.k>=-104).Sum(e=>e.r).Dump();
tj.Where(e=>e.k<-104).Sum(e=>e.r).Dump();

//Grid_Level_Up.Where(e=>e.leveld !=null).Dump();