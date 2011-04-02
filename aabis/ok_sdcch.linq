<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Database>master</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

var sdcch=from p in Gis_mrs
.Select(e=>new {e.Chan,e.Weight,e.Gridleft,e.Gridbottom,e.Gridright,e.Gridtop}).ToList()  
               where p.Weight !=null
               where p.Chan>=4 && p.Chan <=15
               group p by new {p.Gridleft,p.Gridbottom,p.Gridright,p.Gridtop}
			   into ttt
			   //let dtxna=ttt.Where(e=>e.Downdtx!=null).Count()
			   select new 
			   {
			   k=ttt.Key,
			   leveld=ttt.Count()*0.2354/3600
			   };

sdcch.Sum(e=>e.leveld).Dump();