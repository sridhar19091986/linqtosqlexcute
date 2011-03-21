<Query Kind="Program">
  <Connection>
    <ID>f2f6e2b4-0f40-48df-b2c7-82584a77c393</ID>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Nutshell.mdf</AttachFileName>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
  </Connection>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
</Query>

static void Main()
			{
				string path = "";
				XmlDocument doc = new XmlDocument();
				doc.Load(@"F:\LenovoHP\ICDR_EDGE\Temp\Asn1OutXml.xml");
				XmlElement root = doc.DocumentElement;
				var q=from c in doc.ChildNodes .Cast <XmlNode>()
				      select c.InnerText;
					  q.Dump ();
					  
				
				
				
				
				
				
				
				
				
				Get(root,path);
			}
			XmlDocument doc = new XmlDocument();
			.....

			var q = from c in doc.DocumentElement.ChildNodes.Cast<XmlNode>()
					where Convert.ToInt32(c.SelectSingleNode("ID").InnerText) == 10
					select  c;
			public static  void Get(XmlNode node,string path)
			{
				if (!node.HasChildNodes)
				{
					Console.WriteLine(path);
					Console.WriteLine(node.InnerText);
				}
				else
				{
					path += node.Name + "/";
					foreach (XmlNode n in node.ChildNodes)
					{
						Get(n, path);
					}
				}
			}