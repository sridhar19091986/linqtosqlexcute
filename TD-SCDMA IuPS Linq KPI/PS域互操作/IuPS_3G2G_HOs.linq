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
	  let ss=IuPS_3G2G_HOs.Sum(e=>e.SRNS_ContextTransferRequest)
	  select new {
	    mKey=ps.Key,
	  	mSRNSContextRequest=ps.Sum (e=>e.SRNS_ContextTransferRequest),
		mSRNSContextRequestPercent=(ps.Sum(e=>e.SRNS_ContextTransferRequest)+0.0)/ss,
	    mIuReleaseCommand=ps.Sum(e=>e.Iu_ReleaseComplete),
	    mSRNSContextRequestSuccRate=(ps.Sum(e=>e.Iu_ReleaseComplete)+0.0)/ps.Sum (e=>e.SRNS_ContextTransferRequest),
	    mSRNSContextRequestDelay= ps.Average(e =>e.Iu_ReleaseComplete_delayFirst)
	  };
	  
	  a.Dump();