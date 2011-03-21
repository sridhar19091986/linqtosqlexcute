<Query Kind="Statements">
  <Connection>
    <ID>a227fb82-8e77-4297-a037-c08cd4b366e6</ID>
    <Persist>true</Persist>
    <Server>.\SQLEXPRESS</Server>
    <Database>14A_0624</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var ps= from p in Gb_PDP_Activations
    where p.PDP_Act_Accept!=null
	group p by p.BVCI into psbyfilenum
	orderby psbyfilenum.Key 
	select new {
	mFile=psbyfilenum.Key ,
    //mTime=psbyfilenum.Where (e=>e.FileNum ==psbyfilenum.Key).Select(e=>e.PacketTime.Value.AddHours(8)).Min (),
	mTime=psbyfilenum.Select(e=>e.PacketTime.Value.AddHours(-8)).Min (),
	mName="PDP_Act_Accept",
	mMessage= psbyfilenum.Select(e=>e.PDP_Act_Accept).Sum (),
	mSuccess= (psbyfilenum.Select(e=>e.PDP_Act_Accept).Sum ()+0.0)/psbyfilenum.Select(e=>e.PDP_Act_Request).Sum (),
	mDelay= psbyfilenum.Select(e =>e.PDP_Act_Accept_delayFirst).Average ()
//	mStatus=Gb_LLC_Discardeds.Where(e=>e.BVCI==psbyfilenum.Key).Count()
	};
	ps.OrderByDescending(e=>e.mDelay).Dump ();