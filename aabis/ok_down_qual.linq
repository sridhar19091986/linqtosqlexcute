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

var Grid_Qual_Down=from p in Gis_mrs
.Select(e=>new {e.Updtx,e.Down_full_rxqual,e.Down_sub_rxqual,e.Gridleft,e.Gridbottom,e.Gridright,e.Gridtop}).ToList()     
               group p by new {p.Gridleft,p.Gridbottom,p.Gridright,p.Gridtop}
			   into ttt
			   let dtxna=ttt.Where(e=>e.Updtx!=null).Count()
			   select new 
			   {
			   k=ttt.Key,
			   leveld= 
			   (ttt.Where(e=>e.Updtx==0).Where(e=>e.Down_full_rxqual<4).Count()*1.0
			   +ttt.Where(e=>e.Updtx==0).Where(e=>e.Down_full_rxqual>=4).Where(e=>e.Down_full_rxqual<7).Count()*0.7
			   +ttt.Where(e=>e.Updtx==1).Where(e=>e.Down_sub_rxqual<4).Count()*1.0
			   +ttt.Where(e=>e.Updtx==1).Where(e=>e.Down_sub_rxqual>=4).Where(e=>e.Down_sub_rxqual<7).Count()*0.7
			   )/dtxna
			   };
var tj=from p in Grid_Qual_Down
       group p by p.leveld into ttt
	   select new 
			{
			k=ttt.Key,
			s=ttt.Count(),
			r=ttt.Count()*1.0/Grid_Qual_Down.Count()
			};
//tj.OrderByDescending(e=>e.r).Dump();
Grid_Qual_Down.Average(e=>e.leveld).Dump();
tj.Where(e=>e.k>=0.95).Sum(e=>e.r).Dump();
tj.Where(e=>e.k>=0.90).Where(e=>e.k<0.95).Sum(e=>e.r).Dump();
tj.Where(e=>e.k>=0.80).Where(e=>e.k<0.90).Sum(e=>e.r).Dump();
tj.Where(e=>e.k<0.80).Sum(e=>e.r).Dump();		   
//Grid_Qual_Down.Where(e=>e.leveld !=null).Dump();