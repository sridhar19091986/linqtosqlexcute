<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>RNC681_2008</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

TimeSpan timer=IuPS_ATTs.Max(e=>e.PacketTime).Value- IuPS_ATTs.Min(e=>e.PacketTime).Value;
var mTime=timer.TotalSeconds;
var tt=IuPS_ATTs.Min(e=>e.PacketTime).Value.ToString()+"~"+IuPS_ATTs.Max(e=>e.PacketTime).Value.ToString()+"~"+mTime.ToString();
tt.Dump();
var a= from p in IuPS_ATTs
.Where (e=>e.DumpFor =="EndFlowByFlowDesigner")
	group p by p.ID_REQ==null into ps
	select new {
	mKey=ps.Key,
AttachRequest=0,
IdentityRequest=ps.Average(e=>e.ID_REQ_delayFirst),
IdentityResponse=ps.Average(e=>e.ID_RESP_delayFirst),
AuthenticationandCipheringReq=ps.Average(e=>e.Authentication_Request_delayFirst),
AuthenticationandCipheringResp=ps.Average(e=>e.Authentication_Response_delayFirst),
SecurityModeControlCommand=ps.Average(e=>e.SecurityModeControl_Command_delayFirst),
SecurityModeControlComplete=ps.Average(e=>e.SecurityModeControl_Complete_delayFirst),
AttachAccept=ps.Average(e=>e.Attach_Accept_delayFirst),
AttachComplete=ps.Average(e=>e.Attach_Complete_delayFirst),};
	a.OrderByDescending(e=>e.AttachComplete).Dump();