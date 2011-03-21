<Query Kind="Program">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

// keywords_base.cs
// Accessing base class members
//using System;
public class Person
{
	protected string ssn = "444-55-6666";
	protected string name = "John L. Malgraine";

	public virtual void GetInfo()
	{
		Console.WriteLine("Name: {0}", name);
		Console.WriteLine("SSN: {0}", ssn);
	}
}
class Employee : Person
{
	public string id = "ABC567EFG";
	public override void GetInfo()
	{
		// Calling the base class GetInfo method:
		base.GetInfo();
		Console.WriteLine("Employee ID: {0}", id);
	}
}

//class TestClass
//{
	static void Main()
	{
		Employee E = new Employee();
		E.GetInfo();
	}
//}