<Query Kind="Statements">
  <Connection>
    <ID>69c6882b-052d-4d33-8482-fc44aaa595ea</ID>
    <Server>192.168.1.16</Server>
    <Persist>true</Persist>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAN96yok7v7Eyv6CwMzOjNtQAAAAACAAAAAAADZgAAqAAAABAAAABPq8wPbMN4vRjVWN5aY3GzAAAAAASAAACgAAAAEAAAAPUA4cdtoD3riRYDMCFP6wIIAAAAfTDM16k7waAUAAAAKpDjl8vZLqArCW3VTCUIjGdOqPE=</Password>
    <Database>Attatch_id_23A_20100329</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var totalAttatchRequest=Gb_Attachs.Where(e=>e.LAC==e.Gmm_lac).
Where(e=>e.Request_id2=="IMSI").Where(e=>e.FileNum ==18).Where(e=>e.CI =="3652");

totalAttatchRequest.Dump();
var lacci=from p in totalAttatchRequest
          group p by p.LAC+"~"+p.CI 
		  into tt
		  select new 
		  {
		 mkey=tt.Key,
		  mCount=tt.Count()
		  };
		  
		  
		  lacci.OrderByDescending(e=>e.mCount).Dump();