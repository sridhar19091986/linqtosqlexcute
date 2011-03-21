<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>fs_abis</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

var msg=from p in Hos
        //where p.信道类型 >3
		group p by p.信道类型>3 into tt
		select new 
		{
		tt.Key,
		cc=tt.Sum(e=>e.跨小区切换失败)+tt.Sum(e=>e.小区内切换失败),
		zb=(tt.Sum(e=>e.跨小区切换失败)+tt.Sum(e=>e.小区内切换失败))*1.00/(Hos.Sum(e=>e.跨小区切换失败)+Hos.Sum(e=>e.小区内切换失败))
		};

		
msg.Dump();