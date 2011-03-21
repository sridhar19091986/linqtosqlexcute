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
var totalATTRequest=IuPS_ATTs.Where (e=>e.DumpFor =="EndFlowByFlowDesigner").Sum(e=>e.Attach_Request);
var a= from p in IuPS_ATTs
.Where (e=>e.DumpFor =="EndFlowByFlowDesigner")
	group p by p.ID_REQ==null into ps
	select new {
	mKey=ps.Key ,
AttachRequest=ps.Sum(e=>e.Attach_Request),
IdentityRequest=ps.Sum(e=>e.ID_REQ),
IdentityResponse=ps.Sum(e=>e.ID_RESP),
AuthenticationandCipheringReq=ps.Sum(e=>e.Authentication_Request),
AuthenticationandCipheringResp=ps.Sum(e=>e.Authentication_Response),
SecurityModeControlCommand=ps.Sum(e=>e.SecurityModeControl_Command),
SecurityModeControlComplete=ps.Sum(e=>e.SecurityModeControl_Complete),
AttachAccept=ps.Sum(e=>e.Attach_Accept),
AttachComplete=ps.Sum(e=>e.Attach_Complete),
	};
	a.OrderByDescending(e=>e.AttachComplete).Dump();