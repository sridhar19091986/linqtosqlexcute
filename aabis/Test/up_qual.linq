<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Database>master</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

var Grid_Qual_Up=from p in Gis_mrs.Take(5000)
             
               group p by new {p.Gridleft,p.Gridbottom,p.Gridright,p.Gridtop}
			   into ttt
			     let dtxna=ttt.Where(e=>e.Updtx!=null).Count()
			   select new 
			   {
			   
			   k=ttt.Key,
			   leveld= 
			   (ttt.Where(e=>e.Downdtx==0).Where(e=>e.Up_full_rxqual<4).Count()*1.0
			   +ttt.Where(e=>e.Downdtx==0).Where(e=>e.Up_full_rxqual>=4).Where(e=>e.Up_full_rxqual<7).Count()*0.7
			   +ttt.Where(e=>e.Downdtx==1).Where(e=>e.Up_sub_rxlev<4).Count()*1.0
			   +ttt.Where(e=>e.Downdtx==1).Where(e=>e.Up_sub_rxqual>=4).Where(e=>e.Up_sub_rxqual<7).Count()*0.7
			   )/dtxna
			   
			   };


var tj=from p in Grid_Qual_Up
	   group p by p.leveld into ttt
	    let gridsum=ttt.Count()
	   select new 
			{
			ttt.Key,
			s=ttt.Count(),
			r=ttt.Count()*1.0/gridsum
			};
tj.Dump();
			   
Grid_Qual_Up.Where(e=>e.leveld !=null).Dump();