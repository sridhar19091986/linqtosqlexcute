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
		where p.Bcap_speech_ver_0_0 !=null
		select new
		{
		OccurAtMessage=p.Amr_MsgType,
		SpeechVerCollection=(p.Bcap_speech_ver_0_0!=null?p.Bcap_speech_ver_0_0+";":";")
	+	(p.Bcap_speech_ver_0_1!=null?p.Bcap_speech_ver_0_1+";":";")
	+	(p.Bcap_speech_ver_0_2!=null?p.Bcap_speech_ver_0_2+";":";")
	+	(p.Bcap_speech_ver_0_3!=null?p.Bcap_speech_ver_0_3+";":";")
	+	(p.Bcap_speech_ver_0_4!=null?p.Bcap_speech_ver_0_4+";":";")
	+	(p.Bcap_speech_ver_0_5!=null?p.Bcap_speech_ver_0_5+";":";")
	+	(p.Bcap_speech_ver_0_6!=null?p.Bcap_speech_ver_0_6+";":";")		
		};
var messages=msg.Select(e=>e.OccurAtMessage).Distinct();

foreach( var m in messages)
{
        var arm=from p in msg
        where p.OccurAtMessage==m
        group p by p.SpeechVerCollection.IndexOf("speech version 3")!=-1 into tt
		select new 
		{
		SpeechVer3=tt.Key,
		OccurAtMessage=m,
		SpeechVerCount=tt.Count(),
		SpeechVerRate=(tt.Count()+0.0)/msg.Where(e=>e.OccurAtMessage==m).Count()
		};
        arm.OrderByDescending(e=>e.SpeechVerCount).Dump();
}