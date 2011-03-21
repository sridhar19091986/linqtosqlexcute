<Query Kind="SQL">
  <Connection>
    <ID>337b631f-296e-4a3b-aa36-09f23a9a9a38</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>msqq</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

CREATE TABLE msqqBulk2 
( 
 frame_number  int,
 frame_time  VARCHAR(32),
 frame_time_relative  VARCHAR(32),
 ip_src VARCHAR(32),
 ip_dst VARCHAR(32),
 tcp_srcport INT,
 tcp_dstport INT,
 msqq_qqNumber VARCHAR(32) null,
 msqq_Length INT null,
 msqq_Version  INT null,
 msqq_Command  VARCHAR(32),
 msqq_Sequence  INT  null,
 message_type VARCHAR(32),
)