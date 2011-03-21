<Query Kind="Program">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

// cs_params.cs
//using System;
//public class MyClass 
//{

	public static void UseParams(params int[] list) 
	{
		for (int i = 0 ; i < list.Length; i++)
		{
			Console.WriteLine(list[i]);
		}
		Console.WriteLine();
	}

	public static void UseParams2(params object[] list) 
	{
		for (int i = 0 ; i < list.Length; i++)
		{
			Console.WriteLine(list[i]);
		}
		Console.WriteLine();
	}

	static void Main() 
	{
		UseParams(1, 2, 3);
		UseParams2(1, 'a', "test"); 

		// An array of objects can also be passed, as long as
		// the array type matches the method being called.
		int[] myarray = new int[3] {10,11,12};
		UseParams(myarray);
	}
//}