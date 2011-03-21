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
        where p.Attach_Reject !=null
		group p by p.Reject_Cause into sp
		select new 
		{
	      Reject_Cause=sp.Key,
		  count=sp.Count(),
		  rate=(sp.Count()+0.0)/Gb_Attachs.Where(e=>e.Attach_Reject !=null).Count()
		 };
		
		ps.OrderByDescending(e=>e.rate).Dump();