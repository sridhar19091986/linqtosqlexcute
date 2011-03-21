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

var messages=WAMRs.Select(e=>e.Amr_MsgType).Distinct();

foreach( var m in messages)
{
var msg=from p in WAMRs
        where p.Speech_ver !=null
		where p.Amr_MsgType==m
		group p by p.Speech_ver into tt
		select new
		{
		  SpeechVersion=tt.Key,
		  OccurAtMessage=m,
		  SpeechVerCount=tt.Count(),
		  SpeechVerRate=(tt.Count()+0.0)/WAMRs.Where(e=>e.Speech_ver !=null).Where(e=>e.Amr_MsgType==m).Count()
		};
msg.OrderByDescending(e=>e.SpeechVerCount).Dump();
}