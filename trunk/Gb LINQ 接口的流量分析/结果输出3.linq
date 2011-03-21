<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>23A</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Namespace></Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

var a=from p in MLocatingTypes
      group p by p.TrafficType   into tt
	  let allLen= MLocatingTypes.Sum (e=>e.MLen )
	  select new
	  {
	        mKey=tt.Key ,
			室内终端=(tt.Where (e=>e.CiConverClass =="是" ).Where (e=>e.MsimeiType==null||e.MsimeiType=="1"||e.MsimeiType=="2").Sum (e=>e.MLen )+0.0)/allLen,
			室内上网卡=(tt.Where (e=>e.CiConverClass =="是" ).Where (e=>e.MsimeiType!=null&&e.MsimeiType!="1"&&e.MsimeiType!="2").Sum (e=>e.MLen )+0.0)/allLen,
			室外终端=(tt.Where (e=>e.CiConverClass !="是" ).Where (e=>e.MsimeiType==null||e.MsimeiType=="1"||e.MsimeiType=="2").Sum (e=>e.MLen )+0.0)/allLen,
			室外上网卡=(tt.Where (e=>e.CiConverClass !="是" ).Where (e=>e.MsimeiType!=null&&e.MsimeiType!="1"&&e.MsimeiType!="2").Sum (e=>e.MLen )+0.0)/allLen
	  };
	  
	  a.OrderByDescending (e=>e.mKey).Dump ();