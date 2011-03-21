<Query Kind="Statements">
  <Connection>
    <ID>a227fb82-8e77-4297-a037-c08cd4b366e6</ID>
    <Persist>true</Persist>
    <Server>.\SQLEXPRESS</Server>
    <Database>14A_0624</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//var link=from p in Link_ip_lengths
TimeSpan tt=(Link_ip_lengths.Max(e=>e.TTime).Value-Link_ip_lengths.Min(e=>e.TTime).Value);
tt.Dump();

var link=from p in Link_ip_lengths
         group p by p.Link into ll
		 let timer=tt.Seconds
		 let total=Link_ip_lengths.Sum(e=>e.IpLength).Value 
		 select new {
		 
		 ll.Key,
		 ipLength=ll.Sum(e=>e.IpLength).Value,
		 link_rate=ll.Sum(e=>e.IpLength).Value/timer,
		 ipLength_link_rate=(ll.Sum(e=>e.IpLength).Value +0.0)/total
		 
		 };
		 
		 link.OrderBy(e=>e.ipLength_link_rate).Dump();