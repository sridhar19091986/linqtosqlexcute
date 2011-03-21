<Query Kind="Program">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

//// statements_checked.cs
//using System;
//class OverFlowTest
//{
	static short x = 32767;   // Max short value
	static short y = 32767;

	// Using a checked expression 
	static int CheckedMethod()
	{
		int z = 0;
		try
		{
//			z = checked((short)(x + y));
			z = (short)(x + y);
		}
		catch (System.OverflowException e)
		{
			Console.WriteLine(e.ToString());
		}
		return z;
	}

	static void Main()
	{
		Console.WriteLine("Checked output value is: {0}", 
					 CheckedMethod());
	}
//}