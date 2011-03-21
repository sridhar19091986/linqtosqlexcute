<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>mc_sz04a</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

//var opcdpc=WSLS.Select(e=>new {e.Opc,e.Dpc,e.Ni,e.Sls}).Distinct();
//opcdpc.OrderBy(e=>e.Opc).ThenBy(e=>e.Dpc).ThenBy(e=>e.Sls).Dump();

//foreach
		   var bb=
		   from p in WSLS
		   group p by new {p.Opc,p.Dpc,p.Ni} into ttt
		   		   select new 
		   {
		   
		   Opc=ttt.Key.Opc,
		   Dpc= ttt.Key.Dpc,
			Ni=  ttt.Key.Ni,
		   sum=ttt.Count()
		   
		   };
		   
var paging=from p in WSLS
           group p by new {p.Opc,p.Dpc,p.Ni,p.Sls} into tt
		   select new 
		   {
		   
		   Opc=tt.Key.Opc,
		   Dpc= tt.Key.Dpc,
			Ni=  tt.Key.Ni,
			Sls=    tt.Key.Sls,
			Count=tt.Count(),
		   Rate=(tt.Count()+0.0)/bb.Where(e=>e.Opc==tt.Key.Opc).Where(e=>e.Dpc==tt.Key.Dpc).Where(e=>e.Ni==tt.Key.Ni).Select(e=>e.sum).FirstOrDefault()
		   
		   };
           

paging.OrderBy(e=>e.Opc).ThenBy(e=>e.Dpc).ThenBy(e=>e.Sls).Dump();

var sls=from p in paging
        group p by new {p.Opc,p.Dpc,p.Ni} into tttt
		select new 
		{
		  //tttt.Key,
		  tttt.Key.Opc,
		  tttt.Key.Dpc,
		  tttt.Key.Ni,
		  c=tttt.Count()
		};
sls.Dump();

//WSLS.Where(e=>e.Ni=="2").Dump();

