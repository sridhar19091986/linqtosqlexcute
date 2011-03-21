<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>14A</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

TimeSpan timer=NsLengths.Max(e=>e.TTime).Value- NsLengths.Min(e=>e.TTime).Value;
var mTime=timer.TotalSeconds;
var tt=NsLengths.Min(e=>e.TTime).Value.ToString()+"~"+NsLengths.Max(e=>e.TTime).Value.ToString()+"~"+mTime.ToString();
tt.Dump();
var ps= from p in Gb_PDP_Activations
     where p.Repeated_Request==null
	group p by p.LAC into psbyfilenum
	orderby psbyfilenum.Key 
	select new {
	mLac=psbyfilenum.Key ,
	mName=this.Connection.Database,
	mRequest=psbyfilenum.Sum(e=>e.PDP_Request),
    mSuccess= psbyfilenum.Sum(e=>e.PDP_Accept),
	mSuccRate= (psbyfilenum.Sum(e=>e.PDP_Accept)+0.0)/psbyfilenum.Sum(e=>e.PDP_Request),
	mDelay_ms= psbyfilenum.Average(e =>e.Accept_Time)
	};
	ps.OrderByDescending(e=>e.mDelay_ms).Dump ();