<Query Kind="Statements">
  <Connection>
    <ID>69c6882b-052d-4d33-8482-fc44aaa595ea</ID>
    <Server>192.168.1.6</Server>
    <Persist>true</Persist>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAN96yok7v7Eyv6CwMzOjNtQAAAAACAAAAAAADZgAAqAAAABAAAABPq8wPbMN4vRjVWN5aY3GzAAAAAASAAACgAAAAEAAAAPUA4cdtoD3riRYDMCFP6wIIAAAAfTDM16k7waAUAAAAKpDjl8vZLqArCW3VTCUIjGdOqPE=</Password>
    <Database>sz_47B_20101013</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var ps= from p in Gb_Attachs
//        where p.Reject_Cause =="Congestion" 
//        where  p.Attach_Reject !=null &&
where p.FileNum==0
//		where p.PTMSI==null
 where p.CI =="3880"
 where p.Attach_Complete==1
//		where p.TLLI==p.PTMSI
//		where p.Attach_Request_Repeat==null
		select p;
		
		ps.Dump();