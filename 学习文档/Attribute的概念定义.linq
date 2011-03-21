<Query Kind="Program">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
</Query>

//1.自定义Attribute类：VersionAttribute
//“[AttributeUsage(AttributeTargets.Class)] ”说明了Attribute提供附加信息的元素是Class
//Attribute可以关联的元素包括：
//程序集(assembly)、模块(module)、类型(type)、属性(property)、事件(event)、字段(field)、方法(method)、参数(param)、返回值(return)

[AttributeUsage(AttributeTargets.Class)] 
public class VersionAttribute : Attribute  
{     
public string Name { get; set; }      
public string Date { get; set; }      
public string Describtion { get; set; }  
}

//2.使用自定义Attribute的Class：
//Attribute一般翻译为”特性”，而Property称为“属性”

[Version(Name = "hyddd", Date = "ok,2009-07-20", Describtion = "hyddd's class")] 
public class MyCode  {  
//...  
} 

//3.上面这个Class中的Attribute一般会被如何使用呢？


static void Main()      
{     
var info = typeof(MyCode);        
var classAttribute = (VersionAttribute)Attribute.GetCustomAttribute(info, typeof(VersionAttribute));    
Console.WriteLine(classAttribute.Name);         
Console.WriteLine(classAttribute.Date);         
Console.WriteLine(classAttribute.Describtion);     
}  