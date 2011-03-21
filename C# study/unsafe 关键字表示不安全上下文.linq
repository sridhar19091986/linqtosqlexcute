<Query Kind="Program">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

// cs_unsafe_keyword.cs
// compile with: /unsafe
//using System;
//class UnsafeTest
//{
   // Unsafe method: takes pointer to int:
   unsafe static void SquarePtrParam(int* p)
   {
	  *p *= *p;
   }

   unsafe static void Main()
   {
	  int i = 5;
	  // Unsafe method: uses address-of operator (&):
	  SquarePtrParam(&i);
	  Console.WriteLine(i);
   }
//}