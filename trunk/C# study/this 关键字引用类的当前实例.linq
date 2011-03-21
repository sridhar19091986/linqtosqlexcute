<Query Kind="Program">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

// keywords_this.cs
// this example
//using System;
class Employee
{
	private string name;
	private string alias;
	private decimal salary = 3000.00m;

	// Constructor:
	public Employee(string name, string alias)
	{
		// Use this to qualify the fields, name and alias:
		this.name = name;
		this.alias = alias;
	}

	// Printing method:
	public void printEmployee()
	{
		Console.WriteLine("Name: {0}\nAlias: {1}", name, alias);
		// Passing the object to the CalcTax method by using this:
		Console.WriteLine("Taxes: {0:C}", Tax.CalcTax(this));
	}

	public decimal Salary
	{
		get { return salary; }
	}
}
class Tax
{
	public static decimal CalcTax(Employee E)
	{
		return 0.08m * E.Salary;
	}
}

//class MainClass
//{
	static void Main()
	{
		// Create objects:
		Employee E1 = new Employee("John M. Trainer", "jtrainer");

		// Display results:
		E1.printEmployee();
	}
//}