<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.dll</Reference>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.Dialog.dll</Reference>
  <Reference>F:\参考代码\frmConnectionConfig\frmConnectionConfig\bin\Debug\Microsoft.Data.ConnectionUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Reference>E:\linq to sql\HtmlAgilityPack\HtmlAgilityPack.1.4.0\HtmlAgilityPack.dll</Reference>
  <GACReference>Microsoft.SqlServer.Management.MultiServerConnection, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <GACReference>Microsoft.SqlServer.Management.CollectorEnum, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91</GACReference>
  <Namespace>System.Data</Namespace>
  <Namespace>System.Data.Common</Namespace>
  <Namespace>Microsoft.Data.ConnectionUI</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>HtmlAgilityPack</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Security</Namespace>
  <Namespace>System.Security.Permissions</Namespace>
  <Namespace>System.Security.Principal</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

public class MyClass
{
  public int Value;
}
//如果不重新生成对象(new)，ref加不加，都是指向同样的一个对象
public static void TestRef(ref MyClass m)
{
  m.Value = 10;
}

//m是对象指针
public static void TestNoRef(MyClass m)
{
  m.Value = 20;
}

//m实际是，类型指针，相当于 MyClass *m
//新建了一个对象时，m是指向这个新的对象(new)
public static void TestCreateRef(ref MyClass m)
{
  m = new MyClass();  //这个m地址没有发生改变，ref 把m地址传递给新对象
  m.Value = 100;
}

//新建一个对象时(new)，此时仍然是指向m原来的对象
public static void TestCreateNoRef(MyClass m)
{
  m = new MyClass();  //这个m指向了新的地址，调用时，原因地址上的值不会发生改变
  m.Value = 200;
}

public static void Main()
{
  MyClass m = new MyClass();
  m.Value = 1;

  TestRef(ref m);
  Console.WriteLine(m.Value);


  TestNoRef(m);
  Console.WriteLine(m.Value);

  TestCreateRef(ref m);
  Console.WriteLine(m.Value);

  TestCreateNoRef(m);
  Console.WriteLine(m.Value);
}