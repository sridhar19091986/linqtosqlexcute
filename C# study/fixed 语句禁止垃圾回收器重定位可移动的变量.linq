<Query Kind="Program">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

// statements_fixed.cs
// compile with: /unsafe
//using System;

class Point
{ 
	public int x, y; 
}

//class FixedTest 
//{
	// Unsafe method: takes a pointer to an int.
	unsafe static void SquarePtrParam (int* p) 
	{
		*p *= *p;
	}

	unsafe static void Main() 
	{
		Point pt = new Point();
		pt.x = 5;
		pt.y = 6;
		// Pin pt in place:
		fixed (int* p = &pt.x) 
		{
			SquarePtrParam (p);
		}
		// pt now unpinned
		Console.WriteLine ("{0} {1}", pt.x, pt.y);
	}
//}