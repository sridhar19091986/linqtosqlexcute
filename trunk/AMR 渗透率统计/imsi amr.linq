<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>Amr</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

var msg=from p in WImsiImeiAmrs
		where p.Bcap_speech_ver_0_0 !=null && p.Imsi !=null
		select new
		{
		imsi=p.Imsi,
		SpeechVerCollection=(p.Bcap_speech_ver_0_0!=null?p.Bcap_speech_ver_0_0+";":";")
	+	(p.Bcap_speech_ver_0_1!=null?p.Bcap_speech_ver_0_1+";":";")
	+	(p.Bcap_speech_ver_0_2!=null?p.Bcap_speech_ver_0_2+";":";")
	+	(p.Bcap_speech_ver_0_3!=null?p.Bcap_speech_ver_0_3+";":";")
	+	(p.Bcap_speech_ver_0_4!=null?p.Bcap_speech_ver_0_4+";":";")
	+	(p.Bcap_speech_ver_0_5!=null?p.Bcap_speech_ver_0_5+";":";")
	+	(p.Bcap_speech_ver_0_6!=null?p.Bcap_speech_ver_0_6+";":";")		
		};
var messages=msg.Distinct().ToList();

        var arm=from p in messages
        //where p.OccurAtMessage==m
        group p by p.SpeechVerCollection.IndexOf("speech version 3")!=-1 into tt
		select new 
		{
		SpeechVer3=tt.Key,
		OccurAtIMSI="IMSI Distinct",
		SpeechVerCount=tt.Count(),
		SpeechVerRate=(tt.Count()+0.0)/messages.Count()
		};
        arm.OrderByDescending(e=>e.SpeechVerCount).Dump();
		
		
		var imei=from p in messages 
		         join q in  WImsiImeiAmrs.Where(e=>e.Imei !=null) on p.imsi equals q.Imsi
				 select new
		{
		q.Imei,
		p.SpeechVerCollection,
		
		};
		
		var imeis=imei.Distinct().ToList();

		var imeiarm=from p in imeis
		//where p.OccurAtMessage==m
		group p by p.SpeechVerCollection.IndexOf("speech version 3")!=-1 into tt
		select new 
		{
		SpeechVer3=tt.Key,
		OccurAtIMEI="IMEI Distinct",
		SpeechVerCount=tt.Count(),
		SpeechVerRate=(tt.Count()+0.0)/imeis.Count()
		};
		imeiarm.OrderByDescending(e=>e.SpeechVerCount).Dump();
		
	 var hs=	imeis.Where(p=>p.SpeechVerCollection.IndexOf("speech version 3")==-1);
	  StreamWriter objWriter = new StreamWriter(@"C:\log.txt");
			foreach (var s in hs)
				objWriter.WriteLine(s.Imei);
			objWriter.Flush();
			objWriter.Close();
	 