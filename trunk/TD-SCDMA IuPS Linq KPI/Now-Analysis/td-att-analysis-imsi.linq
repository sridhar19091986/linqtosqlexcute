<Query Kind="Statements">
  <Connection>
    <ID>c3e164c7-9cf2-4b0d-aa90-72b91ec7a949</ID>
    <Persist>true</Persist>
    <Server>192.168.1.16</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAN96yok7v7Eyv6CwMzOjNtQAAAAACAAAAAAADZgAAqAAAABAAAACVwTFsk9dhMIn5i8ZFS9d4AAAAAASAAACgAAAAEAAAANmSpZffgSRmeLtQTcaKcpwIAAAAOuxdiYNQsLIUAAAAUdmDuQYJvN2uV/Lw/N9gMoxASHY=</Password>
    <Database>RNC681_20100720_11_28_06</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var aaa=IuPS_ATTs
//.Where (e=>e.Att_imsi .IndexOf ("460077119875")!=-1)
.Where (e=>e.Authentication_Request !=null)
//.Where (e=>e._2G_LAC=="65534")
.Where (e=>e.Attach_Complete ==null)
//.Where (e=>e.Attach_Reject ==null)
//.Where (e=>e.FileNum ==1)
;

aaa.Sum (e=>e.Attach_Request ).Dump ();
aaa.Take (500).Dump ();