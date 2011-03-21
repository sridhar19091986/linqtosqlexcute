<Query Kind="Program">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

// keywords_base2.cs
//using System;
public class BaseClass
{
	int num;

	public BaseClass()
	{
		Console.WriteLine("in BaseClass()");
	}

	public BaseClass(int i)
	{
		num = i;
		Console.WriteLine("in BaseClass(int i)");
	}

	public int GetNum()
	{
		return num;
	}
}

public class DerivedClass : BaseClass
{
	// This constructor will call BaseClass.BaseClass()
	public DerivedClass() : base()
	{
	}

	// This constructor will call BaseClass.BaseClass(int i)
	public DerivedClass(int i) : base(i)
	{
	}


}
	static void Main()
	{
		DerivedClass md = new DerivedClass();
		DerivedClass md1 = new DerivedClass(1);
	}