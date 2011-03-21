<Query Kind="Program">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

//// statements_unchecked.cs
//using System;
//
//class TestClass 
//{
	const int x = 2147483647;   // Max int 
	const int y = 2;

	static void Main() 
	{
		int z;
		unchecked 
		{
			z = x * y;
		}
		Console.WriteLine("Unchecked output value: {0}", z);
	}
//}