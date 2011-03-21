<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>RNC681_2008</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

var a=from p in IuPS_3G2G_HOs
      group p by p.DumpFor into ps 
	  let ss=IuPS_3G2G_HOs.Where(e=>e.SRNS_ContextTransferRequest!=null).Count()
	  select new {
	    mKey=ps.Key,
	  	mSRNSContextRequest=ps.Where(e=>e.SRNS_ContextTransferRequest!=null).Count(),
		mSRNSContextRequestPercent=(ps.Where(e=>e.SRNS_ContextTransferRequest!=null).Count()+0.0)/ss,
	    mIuReleaseCommand=ps.Where(e=>e.Iu_ReleaseComplete!=null).Count(),
	    mSRNSContextRequestSuccRate=(ps.Where(e=>e.Iu_ReleaseComplete!=null).Count()+0.0)/ps.Where(e=>e.SRNS_ContextTransferRequest!=null).Count(),
	    mSRNSContextRequestDelay= ps.Average(e =>e.Iu_ReleaseComplete_delayFirst)
	  };
	  
	  a.Dump();