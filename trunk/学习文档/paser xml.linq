<Query Kind="Statements">
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

XElement config = XElement.Parse (
@"
<message>
<!-- message 1 -->
   <CONTEXT_28>
	  <CONTEXT_0>46</CONTEXT_0>
	  <CONTEXT_3>64005000642546F9</CONTEXT_3>
	  <CONTEXT_4>
		 <CONTEXT_0>DDB128A3</CONTEXT_0>
	  </CONTEXT_4>
	  <CONTEXT_5>20659DC9</CONTEXT_5>
	  <CONTEXT_6>
		 <CONTEXT_0>DDB12846</CONTEXT_0>
	  </CONTEXT_6>
	  <CONTEXT_7>636D776170</CONTEXT_7>
	  <CONTEXT_8>0121</CONTEXT_8>
	  <CONTEXT_9>
		 <CONTEXT_0>
			<CONTEXT_0>0A8A7E7E</CONTEXT_0>
		 </CONTEXT_0>
	  </CONTEXT_9>
	  <CONTEXT_11>FF</CONTEXT_11>
	  <CONTEXT_12>
		 <SEQUENCE>
			<CONTEXT_2>0423621F</CONTEXT_2>
			<CONTEXT_3>00</CONTEXT_3>
			<CONTEXT_4>00</CONTEXT_4>
			<CONTEXT_5>02</CONTEXT_5>
			<CONTEXT_6>0812261800002B0800</CONTEXT_6>
		 </SEQUENCE>
	  </CONTEXT_12>
	  <CONTEXT_13>0812261730002B0800</CONTEXT_13>
	  <CONTEXT_14>0708</CONTEXT_14>
	  <CONTEXT_15>11</CONTEXT_15>
	  <CONTEXT_17>0E41</CONTEXT_17>
	  <CONTEXT_18>4747534E53483035</CONTEXT_18>
	  <CONTEXT_21>00</CONTEXT_21>
	  <CONTEXT_22>91683169059779F7</CONTEXT_22>
	  <CONTEXT_23>0800</CONTEXT_23>
	  <CONTEXT_24>03</CONTEXT_24>
	  <CONTEXT_20>00A3FBA761</CONTEXT_20>
   </CONTEXT_28>
<!-- message 2 -->
   <CONTEXT_28>
	  <CONTEXT_0>46</CONTEXT_0>
	  <CONTEXT_3>64003212080131F0</CONTEXT_3>
	  <CONTEXT_4>
		 <CONTEXT_0>DDB128A3</CONTEXT_0>
	  </CONTEXT_4>
	  <CONTEXT_5>3542B481</CONTEXT_5>
	  <CONTEXT_6>
		 <CONTEXT_0>DDB12C8F</CONTEXT_0>
	  </CONTEXT_6>
	  <CONTEXT_7>636D776170</CONTEXT_7>
	  <CONTEXT_8>0121</CONTEXT_8>
	  <CONTEXT_9>
		 <CONTEXT_0>
			<CONTEXT_0>0A8AE5AC</CONTEXT_0>
		 </CONTEXT_0>
	  </CONTEXT_9>
	  <CONTEXT_11>FF</CONTEXT_11>
	  <CONTEXT_12>
		 <SEQUENCE>
			<CONTEXT_2>0223621F9396405874FFFFFF</CONTEXT_2>
			<CONTEXT_3>00</CONTEXT_3>
			<CONTEXT_4>00</CONTEXT_4>
			<CONTEXT_5>02</CONTEXT_5>
			<CONTEXT_6>0812261800002B0800</CONTEXT_6>
		 </SEQUENCE>
	  </CONTEXT_12>
	  <CONTEXT_13>0812261730002B0800</CONTEXT_13>
	  <CONTEXT_14>0708</CONTEXT_14>
	  <CONTEXT_15>11</CONTEXT_15>
	  <CONTEXT_17>01F0</CONTEXT_17>
	  <CONTEXT_18>4747534E53483035</CONTEXT_18>
	  <CONTEXT_21>00</CONTEXT_21>
	  <CONTEXT_22>91685120810124F8</CONTEXT_22>
	  <CONTEXT_23>0800</CONTEXT_23>
	  <CONTEXT_24>03</CONTEXT_24>
	  <CONTEXT_27>64F000</CONTEXT_27>
	  <CONTEXT_20>00A3FBA762</CONTEXT_20>
   </CONTEXT_28>
 </message>"
);

//config.Annotation <string>.Dump;
foreach (XComment child in config.DescendantNodes().OfType<XComment>())
	child.Dump ();
//	
//XElement file=XElement.Load (@"F:\LenovoHP\ICDR_EDGE\Temp\Asn1OutXml.xml");
//foreach (XComment child in file.DescendantNodes().OfType<XComment>())
//child.Dump ();
//	XNode child;
//	child.Document
foreach (XNode child in config.Nodes ())
//	child.Dump ();
	child.

XElement client = config.Element ("client");

bool enabled = (bool) client.Attribute ("enabled");   // Read attribute
enabled.Dump ("enabled attribute");

client.Attribute ("enabled").SetValue (!enabled);     // Update attribute

int timeout = (int) client.Element ("timeout");       // Read element
timeout.Dump ("timeout element");

client.Element ("timeout").SetValue (timeout * 2);    // Update element

client.Add (new XElement ("retries", 3));             // Add new elememt

config.Dump ("Updated DOM");