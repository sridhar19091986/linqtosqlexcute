<Query Kind="Program">
  <Connection>
    <ID>f2f6e2b4-0f40-48df-b2c7-82584a77c393</ID>
    <Server>.\SQLEXPRESS</Server>
    <AttachFile>true</AttachFile>
    <UserInstance>true</UserInstance>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\Nutshell.mdf</AttachFileName>
  </Connection>
</Query>

   
		public static void Method(int age, String firstname="John", double salary=4000.99)
		{ }
		static void Main()
		{
			Method(30); //The same as Method(30,"John",4000.99)
			Method(30, "Mary"); //The same as Method(30,"Mary",4000.99)
			Method(30, "Mary", 2000.99);
		}
	