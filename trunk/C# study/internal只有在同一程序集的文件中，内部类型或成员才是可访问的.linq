<Query Kind="Program">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

// Assembly1_a.cs
// compile with: /reference:Assembly1.dll

   static void Main() 
   {
	  BaseClass myBase = new BaseClass();   // CS0122
	  myBase.GetType().Dump ();
//	  myBase.
   }

// Assembly1_a.cs
// compile with: /reference:Assembly1.dll
// Assembly1.cs
// compile with: /target:library
internal class BaseClass 
{
   public static int intM = 0;
}


// Assembly2.cs
// compile with: /target:library
public class BaseClass 
{
   internal static int intM = 0;
}
// Assembly2_a.cs
// compile with: /reference:Assembly1.dll

   static void Main() 
   {
	  BaseClass myBase = new BaseClass();   // Ok.
	  BaseClass.intM = 444;    // CS0117
	  
   }
