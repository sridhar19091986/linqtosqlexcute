<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Database>master</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

var sdcch=Gis_mrs.Where(e=>e.Weight !=null).Where(e=>e.Chan>=4 && e.Chan <=15).Count()*0.2354/3600;
var tch=Gis_mrs.Where(e=>e.Weight !=null).Where(e=>e.Chan<4 && e.Chan>15).Count()*0.48/3600;

sdcch.Dump();
tch.Dump();