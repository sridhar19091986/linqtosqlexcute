<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.Dialog.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;System.Windows.Forms.dll</Reference>
  <Reference>E:\linq to sql\HtmlAgilityPack\HtmlAgilityPack.1.4.0\HtmlAgilityPack.dll</Reference>
  <GACReference>Microsoft.SqlServer.Management.MultiServerConnection, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <GACReference>Microsoft.SqlServer.Management.CollectorEnum, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>HtmlAgilityPack</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Reflection</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>


		static void Main()
		{
			CW("测试");
			Person p = new Person();
			p.Name = "snoopy";
			p.Age = 5;
			p.Sex = "male";
			Person p2 = new Person();
			PropertyInfo[] infos = p.GetType().GetProperties();
			CW("打印属性");
			foreach (PropertyInfo info in infos)
			{
				CW(info.Name + ":" + info.GetValue(p, null));
			}
			infos[0].SetValue(p, "Helloketty", null);

			foreach (PropertyInfo info in infos)
			{
				CW(info.Name + ":" + info.GetValue(p, null));
			}
			CR();
		}
		static void CW(object content)
		{
			Console.WriteLine(content);
		}
		static string CR()
		{
			return Console.ReadLine();
		}
	

	class Person
	{
		private string _name;

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}
		private int _age;

		public int Age
		{
			get { return _age; }
			set { _age = value; }
		}
		private string _sex;

		public string Sex
		{
			get { return _sex; }
			set { _sex = value; }
		}
	}