<Query Kind="Program">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

void Main()
{
	
	ItemFactory<int> a=new ItemFactory <int>(2);
	a.GetType().Dump();
	a.i .Dump ();
	a.GetNewItem().Dump();
	a.GetNewItem().GetType().Dump();
TypeAttributes ta=typeof(ItemFactory<int>).Attributes;
MethodAttributes ma=MethodInfo.GetCurrentMethod ().Attributes ;
Console.WriteLine(ta);Console.WriteLine(ma);
}
class ItemFactory<T> where T : new()
{
   public T i;
	public T GetNewItem()
	{
		return new T();
	}
	public ItemFactory(T i)
	{
	this.i =i;
	}
		public ItemFactory()
	{
	this.i =i;
	}
}
// Define other methods and classes here
