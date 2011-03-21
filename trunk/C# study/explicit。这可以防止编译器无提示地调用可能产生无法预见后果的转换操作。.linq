<Query Kind="Program">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

// cs_keyword_explicit_2.cs
//using System;
struct Digit
{
	byte value;
	public Digit(byte value)
	{
		if (value > 9)
		{
			throw new ArgumentException();
		}
		this.value = value;
	}

	// Define explicit byte-to-Digit conversion operator:
	public static explicit operator Digit(byte b)
	{
		Digit d = new Digit(b);
		Console.WriteLine("conversion occurred");
		return d;
	}
}

//class MainClass
//{
	static void Main()
	{
		try
		{
			byte b = 3;
			Digit d = (Digit)b; // explicit conversion
		}
		catch (Exception e)
		{
			Console.WriteLine("{0} Exception caught.", e);
		}
//	}
}