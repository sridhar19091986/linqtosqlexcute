<Query Kind="Statements">
  <Connection>
    <ID>a227fb82-8e77-4297-a037-c08cd4b366e6</ID>
    <Persist>true</Persist>
    <Server>.\SQLEXPRESS</Server>
    <Database>msqq</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

var cmdlist=from p in MsqqBulks
        select p.Message_type;
var cmddisc=cmdlist.Distinct();

foreach(string vars in cmddisc)
{
vars.Dump();
//MsqqBulks.Where(e=>e.Message_type==vars).Where(e=>e.Msqq_Command==113).Dump();
var query=from p in MsqqBulks
		where p.Message_type==vars && p.Ip_src.Substring(0,3)!="10."
		select p;
var maxfr=MsqqBulks.Where(e=>e.Message_type==vars).Max(e=>e.Frame_number);
var minfr=MsqqBulks.Where(e=>e.Message_type==vars).Min(e=>e.Frame_number);
var maxtime=MsqqBulks.Where(e=>e.Message_type==vars).Where(e=>e.Frame_number==maxfr).Select(e=>e.Frame_time).FirstOrDefault();
var mintime=MsqqBulks.Where(e=>e.Message_type==vars).Where(e=>e.Frame_number==minfr).Select(e=>e.Frame_time).FirstOrDefault();
var timer=MsqqBulks.Where(e=>e.Message_type==vars).Where(e=>e.Frame_number==maxfr).Select(e=>e.Frame_time_relative).FirstOrDefault();
var timeduration=mintime+"~"+maxtime+"~"+timer;
timeduration.Dump();
//timer.Substring(0,4).Dump();
int timers=Convert.ToInt32(timer.Substring(0,timer.IndexOf(".")));
timers.Dump();
var cmd=from p in query
		group p by  p.Msqq_Command into tt
		select new 
		{
			oicqCommand=tt.Key,
			count=tt.Count(),
			countRate=(tt.Count()+0.0)/query.Count(),
			countFreq=(0.0+timers/60)/tt.Count(),
			length=tt.Sum(e=>e.Msqq_Length),
			lengthRate=(tt.Sum(e=>e.Msqq_Length)+0.0)/query.Sum(e=>e.Msqq_Length)
		};
				 
cmd.OrderByDescending(e=>e.length).Dump();
}