<Query Kind="Program">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

// statements_continue.cs
//using System;
//class ContinueTest 
//{
	static void Main() 
	{
		for (int i = 1; i <= 10; i++) 
		{
			if (i < 9) 
			{
				continue;
			}
			Console.WriteLine(i);
		}
	}
//}