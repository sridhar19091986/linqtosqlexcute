<Query Kind="Program">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

void Main()
{
	string typeName = typeof(ConcreteProduct).AssemblyQualifiedName;
	Console.WriteLine(typeName);
	IProduct product = new RawGenericFactory<IProduct>().Create(typeName);
	Console.WriteLine(product);
	Debug.Assert(product!=null);
	Debug.Assert(typeName==product.GetType().AssemblyQualifiedName);	
}
interface IProduct { }
class ConcreteProduct : IProduct {public ConcreteProduct(){} }
public class RawGenericFactory<T>// where T:new()
{
    public T Create(string typeName)
	{
	    return (T)Activator.CreateInstance(Type.GetType(typeName));
	}
}
// Define other methods and classes here
public interface ITarget { void Request();}
public interface IAdaptee { void SpecifiedRequest();}	
public abstract class GenericAdapterBase<T> : ITarget where T : IAdaptee, new()
{
	public virtual void Request()
	{
	  new T().SpecifiedRequest();
	}
}