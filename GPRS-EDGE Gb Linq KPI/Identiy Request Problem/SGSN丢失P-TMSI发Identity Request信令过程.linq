<Query Kind="Statements">
  <Connection>
    <ID>c3e164c7-9cf2-4b0d-aa90-72b91ec7a949</ID>
    <Persist>true</Persist>
    <Server>192.168.1.16</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAN96yok7v7Eyv6CwMzOjNtQAAAAACAAAAAAADZgAAqAAAABAAAACVwTFsk9dhMIn5i8ZFS9d4AAAAAASAAACgAAAAEAAAANmSpZffgSRmeLtQTcaKcpwIAAAAOuxdiYNQsLIUAAAAUdmDuQYJvN2uV/Lw/N9gMoxASHY=</Password>
    <Database>kpi_14A_20100820_12</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var totalAttatchRequest=Gb_Attachs.Where(e=>e.IMSI=="460028895030324");


var lacci=from p in totalAttatchRequest
          group p by p.LAC+"~"+p.CI 
		  into tt
		  select new 
		  {
		 mkey=tt.Key,
		  mCount=tt.Count()
		  };
		  
		  
		  lacci.OrderByDescending(e=>e.mCount).Dump();