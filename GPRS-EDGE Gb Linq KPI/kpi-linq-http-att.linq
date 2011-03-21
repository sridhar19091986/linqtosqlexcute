<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>23A</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var ps= from p in Gb_ATTACHes
	group p by p.FileNum into psbyfilenum
	orderby psbyfilenum.Key 
	select new {
	mFile=psbyfilenum.Key ,
	//	mTime=psbyfilenum.Where (e=>e.FileNum ==psbyfilenum.Key).Select(e=>e.PacketTime.Value.AddHours(8)).Min (),
		mTime=psbyfilenum.Where (e=>e.FileNum ==psbyfilenum.Key).Select(e=>e.PacketTime.Value.AddHours(-8)).Min (),
	mName="Attach_Complete",
	mMessage= psbyfilenum.Where (e=>e.FileNum ==psbyfilenum.Key).Where (e=>e.Attach_Complete!=null).Select(e=>e.Attach_Complete).Sum (),
	mSuccess= (psbyfilenum.Where (e=>e.FileNum ==psbyfilenum.Key).Where (e=>e.Attach_Complete!=null).Select(e=>e.Attach_Complete).Sum ()+0.0)/psbyfilenum.Where (e=>e.FileNum ==psbyfilenum.Key).Select(e=>e.Attach_Request).Sum (),
	mDelay= psbyfilenum.Where (e=>e.FileNum ==psbyfilenum.Key).Where (e=>e.Attach_Complete!=null).Select(e =>e.Attach_Complete_delayFirst).Average ()
	};
	ps.Dump ();