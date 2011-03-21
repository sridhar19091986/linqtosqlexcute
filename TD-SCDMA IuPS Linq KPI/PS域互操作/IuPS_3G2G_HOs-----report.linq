<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>RNC681_2008</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

//IuPS_3G2G_HOs.Where(e=>e.Iu_ReleaseRequest !=null).Dump();


var a=from p in IuPS_3G2G_HOs
      where p.IuRequestRadioNetwork !=null
      group p by p.IuRequestRadioNetwork into ps
	  select new 
	  {
	  IuRequestRadioNetwork=ps.Key,
	  count=ps.Count(),
	  IuRequestRadioNetworkRate=(ps.Count()+0.0)/IuPS_3G2G_HOs.Where(e=>e.IuRequestRadioNetwork !=null).Count()
	  };a.Dump();
	  