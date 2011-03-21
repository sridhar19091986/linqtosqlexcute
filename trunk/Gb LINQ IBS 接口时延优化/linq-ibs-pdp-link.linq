<Query Kind="Statements">
  <Connection>
    <ID>a227fb82-8e77-4297-a037-c08cd4b366e6</ID>
    <Persist>true</Persist>
    <Server>.\SQLEXPRESS</Server>
    <Database>14A_0624</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

TimeSpan timer=NsLengths.Max(e=>e.TTime).Value- NsLengths.Min(e=>e.TTime).Value;
var mTime=timer.TotalSeconds;
var tt=NsLengths.Min(e=>e.TTime).Value.ToString()+"~"+NsLengths.Max(e=>e.TTime).Value.ToString()+"~"+mTime.ToString();
tt.Dump();
var ps= from p in Gb_PDP_Activations
    where p.Repeated_Request==null
	group p by p.Link into psbyfilenum
	orderby psbyfilenum.Key 
	select new {
	mLink=psbyfilenum.Key ,
	mName=this.Connection.Database,
	mRequest=psbyfilenum.Sum(e=>e.PDP_Request),
    mSuccess= psbyfilenum.Sum(e=>e.PDP_Accept),
	mSuccRate= (psbyfilenum.Sum(e=>e.PDP_Accept)+0.0)/psbyfilenum.Sum(e=>e.PDP_Request),
	mDelay_ms= psbyfilenum.Average(e =>e.Accept_Time),
	mTotalLength_byte=NsLengths.Where(e=>e.Link==psbyfilenum.Key).Sum(e=>e.Lengths),
	mLinkRate_kbps=(8*(NsLengths.Where(e=>e.Link==psbyfilenum.Key).Sum(e=>e.Lengths)/1024))/mTime
	};
	ps.OrderByDescending(e=>e.mDelay_ms).Dump ();