<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>mc_sz04a</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

 var tsn=WSLS.OrderBy(e=>e.Opc).ThenBy(e=>e.Dpc).ThenBy(e=>e.Tsn);
 tsn.Dump();