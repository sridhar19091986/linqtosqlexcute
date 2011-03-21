<Query Kind="Statements">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>fs_abis</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>



var message=from p in WMessages
            where p.Rrcause !=null
            group p by new {p.Msg_MsgType,p.Rrcause} into tt
			select new 
			{
			rr=tt.Key,
			count=tt.Count()
			};
message.Dump();