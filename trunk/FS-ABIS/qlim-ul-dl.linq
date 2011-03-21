<Query Kind="Program">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>fs_abis</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

void Main()
{
	var qlimdl=from p in  主要参数s
	           group p by p.Qlimdl into tt
			   select new 
			   {
			   Qlimdl_Value=tt.Key,
			   Qlimdl_Cell_Count=tt.Count(),
			   Qlimdl_Cell_Rate=ToPercent(1.00*tt.Count()/主要参数s.Count())
			   };
			   
			   qlimdl.OrderByDescending(e=>e.Qlimdl_Cell_Count).Dump();
			   
	var qlimul=from p in  主要参数s
			   group p by p.Qlimul into tt
			   select new 
			   {
			   Qlimul_Value=tt.Key,
			   Qlimul_Cell_Count=tt.Count(),
			   Qlimul_Cell_Rate=ToPercent(1.00*tt.Count()/主要参数s.Count())
			   };
//			   qlimul.Dump();
//			   var u=qlimul.ToList();
			   qlimul.OrderByDescending(e=>e.Qlimul_Cell_Count).Dump();
			   
			   
}

//static class MyDoubleExtended
//{
	 string ToPercent(double d)
	{
//	    string s=d.Substring(2,2);
//	    return s;
		return d.ToString("P02");
	}
//}

// Define other methods and classes here
