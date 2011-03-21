<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>CDR_Database</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//var link=from p in Link_ip_lengths
DateTime? maxt=NsLengths.Max(e=>e.TTime);
DateTime? mint=NsLengths.Min(e=>e.TTime);
TimeSpan tt=maxt.Value-mint.Value;
mint.Value.Dump();
maxt.Value.Dump();
tt.Dump();
double timer=tt.TotalSeconds;
var link=from p in NsLengths
         group p by p.Link into ll
		 let total=NsLengths.Sum(e=>e.Lengths).Value 
		 select new {
		 link=ll.Key,
		 nsLength=ll.Sum(e=>e.Lengths).Value,
		 link_rate_Mbps=(8*(ll.Sum(e=>e.Lengths).Value/timer)+0.0)/(1024*1024),
		 link_rate=((8*(ll.Sum(e=>e.Lengths).Value/timer)+0.0)/(1024*1024))/2.048,
		 link_total_rate=(ll.Sum(e=>e.Lengths).Value +0.0)/total
		 };
link.OrderBy(e=>e.link_rate).Dump();