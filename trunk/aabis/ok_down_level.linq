<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>master</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

var Grid_Level_Down=from p in Gis_mrs
 .Select(e=>new {e.Updtx,e.Down_full_rxlev,e.Down_sub_rxlev,e.Gridleft,e.Gridbottom,e.Gridright,e.Gridtop}).ToList()     
               group p by new {p.Gridleft,p.Gridbottom,p.Gridright,p.Gridtop}
			   into ttt
			     let dtxna=ttt.Where(e=>e.Updtx!=null).Count()
			   select new 
			   {
			   k=ttt.Key,
			   leveld=1.0*(ttt.Where(e=>e.Updtx==0).Sum(e=>e.Down_full_rxlev-110)+
			               ttt.Where(e=>e.Updtx==1).Sum(e=>e.Down_sub_rxlev-110))/dtxna
			   };
var tj=from p in Grid_Level_Down
	   group p by p.leveld into ttt
	   select new 
			{
			k=ttt.Key,
			s=ttt.Count(),
			r=ttt.Count()*1.0/Grid_Level_Down.Count()
			};
//tj.Dump();	   
Grid_Level_Down.Average(e=>e.leveld).Dump();
tj.Where(e=>e.k>=-75).Sum(e=>e.r).Dump();
tj.Where(e=>e.k<-75).Where(e=>e.k>=-85).Sum(e=>e.r).Dump();
tj.Where(e=>e.k<-85).Where(e=>e.k>=-94).Sum(e=>e.r).Dump();
tj.Where(e=>e.k<-94).Sum(e=>e.r).Dump();
//Grid_Level_Down.Where(e=>e.leveld !=null).Dump();