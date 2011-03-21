<Query Kind="Statements">
  <Connection>
    <ID>a227fb82-8e77-4297-a037-c08cd4b366e6</ID>
    <Server>.\SQLEXPRESS</Server>
    <Database>RNC681_2008</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var aaa=IuPS_RAUs
//.Where (e=>e.Att_imsi .IndexOf ("460077119875")!=-1)
//.Where (e=>e.Authentication_Request !=null)
.Where (e=>e.Identity_Request==2)
//.Where (e=>e.Attach_Complete ==null)
//.Where (e=>e.Attach_Reject ==null)
//.Where (e=>e.FileNum ==1)
;


aaa.Sum (e=>e.Routing_Area_Update_Request ).Dump ();
aaa.Take (500).Dump ();