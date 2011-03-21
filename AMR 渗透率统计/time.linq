<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>Amr</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>



var min=WAMRs.Min(e=>e.PacketTime).Value;
var max=WAMRs.Max(e=>e.PacketTime).Value;
TimeSpan timer=max- min;
var mTime=timer.TotalSeconds;
var tt=min.ToString()+"~"+max.ToString()+"~"+mTime.ToString();
tt.Dump();