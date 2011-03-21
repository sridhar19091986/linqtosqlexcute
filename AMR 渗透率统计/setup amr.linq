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
		//where p.Speech_ver !=null
		where p.Bcap_speech_ver_0_0 !=null
		group p by new {p.Bcap_speech_ver_0_0,p.Amr_MsgType} into tt
		select new
		{
		tt.Key,
		Count=tt.Count()
		
		};
msg.Dump();