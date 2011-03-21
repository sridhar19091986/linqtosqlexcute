<Query Kind="Expression">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

new（C# 参考）

在 C# 中，new 关键字可用作运算符、修饰符或约束。

new 运算符
用于创建对象和调用构造函数。

new 修饰符
用于向基类成员隐藏继承成员。

new 约束
用于在泛型声明中约束可能用作类型参数的参数的类型。


int i = new int();
在上一个语句中，i 初始化为 0，它是 int 类型的默认值。该语句的效果等同于：int i = 0;

// cs_modifier_new.cs
// The new modifier.
using System;
public class BaseC 
{
	public static int x = 55;
	public static int y = 22;
}

public class DerivedC : BaseC 
{
	// Hide field 'x'
	new public static int x = 100;

	static void Main() 
	{
		// Display the new value of x:
		Console.WriteLine(x);
		// Display the hidden value of x:
		Console.WriteLine(BaseC.x);
		// Display the unhidden member y:
		Console.WriteLine(y);
	}
}


new 约束指定泛型类声明中的任何类型参数都必须有公共的无参数构造函数。当泛型类创建类型的新实例时，将此约束应用于类型参数，如下面的示例所示：
class ItemFactory<T> where T : new()
{
	public T GetNewItem()
	{
		return new T();
	}
}
当与其他约束一起使用时，new() 约束必须最后指定：

复制using System;
public class ItemFactory<T>
	where T : IComparable, new()
{
}


