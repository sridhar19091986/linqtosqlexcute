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
        where p.信道类型 >3
		group p by p.信道类型>3 into tt
		select new 
		{
		tt.Key,
		SDCCH切换失败次数=tt.Sum(e=>e.跨小区切换失败)+tt.Sum(e=>e.小区内切换失败),
SDCCH切换失败时掉话次数=tt.Sum(e=>e.SD掉话)+tt.Sum(e=>e.TCH掉话),
SDCCH切换失败时掉话占比=(tt.Sum(e=>e.SD掉话)+tt.Sum(e=>e.TCH掉话))*1.00/(tt.Sum(e=>e.跨小区切换失败)+tt.Sum(e=>e.小区内切换失败)),
SDCCH切换失败时未接通次数=tt.Sum(e=>e.未接通),
SDCCH切换失败时未接通占比=tt.Sum(e=>e.未接通)*1.00/(tt.Sum(e=>e.跨小区切换失败)+tt.Sum(e=>e.小区内切换失败))

		//cc=
		//zb=(tt.Sum(e=>e.跨小区切换失败)+tt.Sum(e=>e.小区内切换失败))*1.00/(Hos.Sum(e=>e.跨小区切换失败)+Hos.Sum(e=>e.小区内切换失败))
		};

		
msg.Dump();