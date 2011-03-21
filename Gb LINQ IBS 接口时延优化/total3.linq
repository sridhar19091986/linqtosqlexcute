<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>CDR_Database</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

         var link=from p in Gb_Attaches
                  where p.Attach_Complete !=null
                  group p by p.Link into ll
                  //let total=Link_ip_lengths.Sum(e=>e.IpLength).Value 
		 select new { 
		 ll.Key,
		 Gb_Attach_Complete_Time=ll.Average(e=>e.Complete_Time).Value ,
		 PDP_Accept_Time=Gb_PDP_Activations.Where(e=>e.PDP_Accept!=null).Where(e=>e.Link==ll.Key).Average(e=>e.Accept_Time),
		 HTTP_Get_Response_time=HTTP_Gets.Where(e=>e.Response!=null).Where(e=>e.Link==ll.Key).Average(e=>e.Response_time),
		 HTTP_Post_Response_time=HTTP_Posts.Where(e=>e.Response!=null).Where(e=>e.Link==ll.Key).Average(e=>e.Response_time),
		 WAP_Connect_Acknowledge_Time=WAP_Connects.Where(e=>e.Acknowledge!=null).Where(e=>e.Link==ll.Key).Average(e=>e.Acknowledge_Time),
		 WAP_Get_Acknowledge_time=WAP_Gets.Where(e=>e.Acknowledge!=null).Where(e=>e.Link==ll.Key).Average(e=>e.Acknowledge_time),
		 WAP_Post_Acknowledge_time=WAP_Posts.Where(e=>e.Acknowledge!=null).Where(e=>e.Link==ll.Key).Average(e=>e.Acknowledge_time) 
		 };
		 link.Dump();
		 
		