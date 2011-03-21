<Query Kind="Program">
  <Connection>
    <ID>f2f6e2b4-0f40-48df-b2c7-82584a77c393</ID>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Nutshell.mdf</AttachFileName>
  </Connection>
</Query>

   
	  static void Main()
		{
			// Say we have a list of strings
			IList<string> arrNames = new List<string>();
			// Now we can covert it into an Enumerable collection
			IEnumerable<object> objects = arrNames;
		}