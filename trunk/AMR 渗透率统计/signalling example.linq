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

var msg=from p in WAMRs
        where p.Amr_MsgType=="DTAP CC.Setup"
		where p.Amr !=null
		select p;
		
		
msg.Take(10).Dump();
