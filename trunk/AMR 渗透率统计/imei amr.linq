<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>IP_Stream</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

 HashSet<string> hs = new HashSet<string>();
 string srLine=null;

					   StreamReader sr = new StreamReader(@"C:\log.txt");
						while (!sr.EndOfStream)
						{
							srLine = sr.ReadLine();
							hs.Add(srLine);
						}

	  StreamWriter objWriter = new StreamWriter(@"C:\NO_AMR_IMEI_LIST.txt");


foreach(string s in hs)
{
objWriter.Write(s);objWriter.Write(",");
var p=ImeiTypes.Where(e=>e.Imei==s.Substring(0,8)).Where(e=>e.ImeiFactory!="未知").FirstOrDefault();
if(p !=null)
{
//Console.Write(p);Console.WriteLine(p.ImeiClass);
objWriter.Write(p.ImeiClass);objWriter.Write(",");
objWriter.Write(p.ImeiFactory);objWriter.Write(",");
objWriter.Write(p.ImeiModel);
}
objWriter.Write("\n");
}
			objWriter.Flush();
			objWriter.Close();


//
//		var imeis=from p in ImeiTypes
//		         join q in  hs on p.Imei equals q.ToString()
//				 select new
//		{
//		p.Imei,
//		q,
//		p.ImeiClass,
//		p.ImeiFactory,
//		p.ImeiModel,
//		
//		//p.SpeechVerCollection,
//		
//		};
//		foreach(var i in imeis)
//		i.Dump();
//		//imeis.Dump();
////ImeiTypes.Where(e=>hs.Contains(e.Imei)).Dump();