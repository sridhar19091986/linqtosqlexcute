<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>RNC681_2008</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var aaa=Iu_RABs
//.Where(e=>e.FileNum ==0)
//.Where(e=>e.RAB_AssignmentRequest_delayFirst>500)
//.Where(e=>e.RAB_AssignmentResponse !=null)
//.Where(e=>e.PDP_Act_Accept ==null)
//.Where(e=>e.UE_ID=="197010");
//.Where(e=>e.SAC=="21583");
.Where(e=>e.RadioNetwork==null)
;

//aaa.Sum (e=>e.PDP_Act_Request ).Dump ();
//aaa.Take (500).Dump ();

aaa.OrderByDescending(e=>e.RAB_AssignmentResponse_delayFirst).Take(10).Dump();