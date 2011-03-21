<Query Kind="Program">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

// cs_operator_sizeof.cs
// compile with: /unsafe

	unsafe static void Main()
	{
		Console.WriteLine("The size of short is {0}.", sizeof(short));
		Console.WriteLine("The size of int is {0}.", sizeof(int));
		Console.WriteLine("The size of long is {0}.", sizeof(long));
	}
