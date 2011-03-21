<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>23A</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var ps= from p in Gb_Authentications
    //where p.Authentication_Req_Repeat ==null
	group p by p.CI into psbyfilenum
	orderby psbyfilenum.Key 
	select new {
	  mFile=psbyfilenum.Key ,
	  mLac=psbyfilenum.Select(e=>e.LAC).FirstOrDefault(),
      //mTime=psbyfilenum.Where (e=>e.FileNum ==psbyfilenum.Key).Select(e=>e.PacketTime.Value.AddHours(8)).Min (),
	  mTime=psbyfilenum.Min(e=>e.PacketTime.Value.AddHours(-8)),
	  mName="Authentication_and_Ciphering",
	  mMessage= psbyfilenum.Where (e=>e.Authentication_and_Ciphering_Resp!=null).Sum(e=>e.Authentication_and_Ciphering_Resp),
	  mSuccess= (psbyfilenum.Where (e=>e.Authentication_and_Ciphering_Resp!=null).Sum(e=>e.Authentication_and_Ciphering_Resp)+0.0)/psbyfilenum.Sum(e=>e.Authentication_and_Ciphering_Req),
	  mDelay= psbyfilenum.Where (e=>e.Authentication_and_Ciphering_Resp!=null).Average(e =>e.Authentication_and_Ciphering_Resp_delayFirst)
      //mStatus=Gb_LLC_Discardeds.Where(e=>e.BVCI==psbyfilenum.Key).Count()
	};
	ps.OrderByDescending(e=>e.mDelay).Dump ();