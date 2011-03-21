<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>RNC681_2008</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var aaa=IuPS_PDPs.Where(e=>e.FileNum==90)
//.Where (e=>e.Att_imsi .IndexOf ("460077119875")!=-1)
//.Where (e=>e.Authentication_Request !=null)
//.Where (e=>e.APN =="CMNET")
//.Where (e=>e.Attach_Complete ==null)
//.Where (e=>e.Attach_Reject ==null)
//.Where (e=>e.FileNum ==1)
;


aaa.Sum (e=>e.PDP_Act_Request ).Dump ();
aaa.Take (500).Dump ();