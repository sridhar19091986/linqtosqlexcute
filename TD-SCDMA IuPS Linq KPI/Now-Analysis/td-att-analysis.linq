<Query Kind="Statements">
  <Connection>
    <ID>69c6882b-052d-4d33-8482-fc44aaa595ea</ID>
    <Server>192.168.1.16</Server>
    <Persist>true</Persist>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAN96yok7v7Eyv6CwMzOjNtQAAAAACAAAAAAADZgAAqAAAABAAAABPq8wPbMN4vRjVWN5aY3GzAAAAAASAAACgAAAAEAAAAPUA4cdtoD3riRYDMCFP6wIIAAAAfTDM16k7waAUAAAAKpDjl8vZLqArCW3VTCUIjGdOqPE=</Password>
    <Database>RNC681_20100720_11_28_06</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var aaa=IuPS_ATTs.Where(e=>e.FileNum==90)
//.Where (e=>e.Att_imsi .IndexOf ("460077119875")!=-1)
//.Where (e=>e.Authentication_Request !=null)
//.Where (e=>e._2G_LAC=="9787")
//.Where (e=>e.Attach_Complete ==null)
//.Where (e=>e.Attach_Reject ==null)
//.Where (e=>e.FileNum ==1)
;

aaa.Sum (e=>e.Attach_Request ).Dump ();
aaa.Take (500).Dump ();