<Query Kind="Expression">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

原书P239 代码

public class CSet
{
	private Hashtable data;
	public CSet()
	{
		data = new Hashtable();
	}
	// More code to follow
}


原书P240 代码1

public void Add(Object item)
{
	if (!data.ContainsValue(item))
		data.Add(Hash(item), item);
}


原书P240 代码2

	private string Hash(Object item)
	{
		char[] chars;
		string s = item.ToString();
		int hashValue = 0;
		chars = s.ToCharArray();
		for (int i = 0; i <= chars.GetUpperBound(0); i++)
			hashValue += (int)chars[i];
		return hashValue.ToString();
}


原书P240 代码3

public void Remove(Object item)
{
	data.Remove(Hash(item));
}
public int Size()
{
	return data.Count;
}


原书P241 代码1

public CSet Union(CSet aSet)
{
CSet tempSet = new CSet();
	foreach (Object hashObject in data.Keys)
	   tempSet.Add(this.data[hashObject]);
	foreach (Object hashObject in aSet.data.Keys)
	   if (!(this.data.ContainsKey(hashObject)))
			tempSet.Add(aSet.data[hashObject]);
	return tempSet;
}


原书P241 代码2

public CSet Intersection(CSet aSet)
{
	CSet tempSet = new CSet();
	foreach (Object hashObject in data.Keys)
	if (aSet.data.Contains(hashObject))
			tempSet.Add(aSet.data[hashObject]);
tempSet.Add(aSet.GetValue(hashObject))
	return tempSet;
}


原书P242 代码1

public bool Subset(CSet aSet)
{
	if (this.Size() > aSet.Size())
		return false;
	else
		foreach (Object key in this.data.Keys)
			if (!(aSet.data.Contains(key)))
				return false;
	return true;
}


原书P242 代码2

	public CSet Difference(CSet aSet)
	{
		CSet tempSet = new CSet();
		foreach (Object hashObject in data.Keys)
			if (!(aSet.data.Contains(hashObject)))
				tempSet.Add(data[hashObject]);
		return tempSet;
	}
	public override string ToString()
	{
		string s = “”;
		foreach (Object key in data.Keys)
			s += data[key] + " ";
		return s;
	}


原书P243 代码

static void Main()
{
	CSet setA = new CSet();
	CSet setB = new CSet();
	setA.Add("milk");
	setA.Add("eggs");
	setA.Add("bacon");
	setA.Add("cereal");
	setB.Add("bacon");
	setB.Add("eggs");
	setB.Add("bread");
	CSet setC = new CSet();
	setC = setA.Union(setB);
	Console.WriteLine();
	Console.WriteLine("A: " + setA.ToString());
	Console.WriteLine("B: " + setB.ToString());
	Console.WriteLine("A union B: " + setC.ToString());
	setC = setA.Intersection(setB);
	Console.WriteLine("A intersect B: " + setC.ToString());
	setC = setA.Difference(setB);
	Console.WriteLine("A diff B: " + setC.ToString());
	setC = setB.Difference(setA);
	Console.WriteLine("B diff A: " + setC.ToString());
	if (setB.Subset(setA))
		Console.WriteLine("b is a subset of a");
	else
		Console.WriteLine("b is not a subset of a");
}


原书P245-P247 代码

public class CSet
{
	private BitArray data;
	public CSet()
	{
		data = new BitArray(5);
	}
	public void Add(int item)
	{
		data[item] = true;
	}
	public bool IsMember(int item)
	{
		return data[item];
	}
	public void Remove(int item)
	{
		data[item] = false;
	}
	public CSet Union(CSet aSet)
	{
		CSet tempSet = new CSet();
		for (int i = 0; i <= data.Count - 1; i++)
			tempSet.data[i] = (this.data[i] || aSet.data[i]);
		return tempSet;
	}
	public CSet Intersection(CSet aSet)
	{
		CSet tempSet = new CSet();
		for (int i = 0; i <= data.Count - 1; i++)
			tempSet.data[i] = (this.data[i] && aSet.data[i]);
		return tempSet;
	}
	public CSet Difference(CSet aSet)
	{
		CSet tempSet = new CSet();
		for (int i = 0; i <= data.Count - 1; i++)
			tempSet.data[i] = (this.data[i] &&
			(!(aSet.data[i])));
		return tempSet;
	}
	public bool IsSubset(CSet aSet)
	{
		CSet tempSet = new CSet();
		for (int i = 0; i <= data.Count - 1; i++)
			if (this.data[i] && (!(aSet.data[i])))
				return false;
		return true;
	}
	public override string ToString()
	{
		string s = "";
		for (int i = 0; i <= data.Count - 1; i++)
			if (data[i])
				s += i;
		return s;
	}
}


static void Main()
{
	CSet setA = new CSet();
	CSet setB = new CSet();
	setA.Add(1);
	setA.Add(2);
	setA.Add(3);
	setB.Add(2);
	setB.Add(3);
	CSet setC = new CSet();
	setC = setA.Union(setB);
	Console.WriteLine();
	Console.WriteLine(setA.ToString());
	Console.WriteLine(setC.ToString());
	setC = setA.Intersection(setB);
	Console.WriteLine(setC.ToString());
	setC = setA.Difference(setB);
	Console.WriteLine(setC.ToString());
	bool flag = setB.IsSubset(setA);
	if (flag)
		Console.WriteLine("b is a subset of a");
	else
		Console.WriteLine("b is not a subset of a");
}
