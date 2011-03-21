<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>RNC681_2008</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var aaa=IuPS_ATTs
//.Where (e=>e.Att_imsi .IndexOf ("460077119875")!=-1)
//.Where (e=>e.Authentication_Request !=null)
//.Where (e=>e.Identity_Request==2)
.Where (e=>e.Attach_Complete !=null)
//.Where(e=>e.ID_REQ==null)
//.Where (e=>e.Attach_Reject ==null)
//.Where (e=>e.SAC=="58292")
//.Where(e=>e.FileNum==17)
.Where(e=>e.ID_REQ==null)
;
aaa.Count().Dump();
aaa.Average(e=>e.Attach_Complete_delayFirst).Dump();
aaa.OrderByDescending (e=>e.Attach_Complete_delayFirst ).Dump ();
//aaa.Take (500).Dump ();