<Query Kind="Program">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

//// cs_namespace_keyword_2.cs
//using System;
//namespace SomeNameSpace
////{
//	public class MyClass 
//	{
		static void Main() 
		{
			Nested.NestedNameSpaceClass.SayHello();
		}
//	}

	// a nested namespace
	namespace Nested   
	{
		public class NestedNameSpaceClass 
		{
			public static void SayHello() 
			{
				Console.WriteLine("Hello");
			}
		}
	}
//}

namespace N1     // N1
{
	class C1      // N1.C1
	{
		class C2   // N1.C1.C2
		{
		}
	}
	namespace N2  // N1.N2
	{
		class C2   // N1.N2.C2
		{
		}
	}
}