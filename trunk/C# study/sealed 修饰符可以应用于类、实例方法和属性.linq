<Query Kind="Program">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

// cs_sealed_keyword.cs
//using System;
sealed class SealedClass
{
	public int x;
	public int y;
}
//class MyDerivedC: SealedClass {} 
//class MainClass
//{
	static void Main()
	{
		SealedClass sc = new SealedClass();
		sc.x = 110;
		sc.y = 150;
		Console.WriteLine("x = {0}, y = {1}", sc.x, sc.y);
//	}
}

//sealed（C# 参考）
//sealed 修饰符可以应用于类、实例方法和属性。
//密封类不能被继承。密封方法会重写基类中的方法
//，但其本身不能在任何派生类中进一步重写。
//当应用于方法或属性时，sealed 修饰符必须始终与 override（C# 参考） 一起使用。
