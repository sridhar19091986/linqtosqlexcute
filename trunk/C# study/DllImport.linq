<Query Kind="Program">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Namespace>System.Runtime.InteropServices.DllImportAttribute;</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

// cm.cs
//using System;
//using System.Runtime.InteropServices;
//public class MainClass 
//{
   [DllImport("Cmdll.dll")]
   public static extern int SampleMethod(int x);

   static void Main() 
   {
	  Console.WriteLine("SampleMethod() returns {0}.", SampleMethod(5));
   }
//}