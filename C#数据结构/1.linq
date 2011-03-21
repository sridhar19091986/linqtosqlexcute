<Query Kind="Expression">
  <Connection>
    <ID>e5f5449b-aa54-4234-bda6-c0296770953c</ID>
    <Server>.\SQLEXPRESS</Server>
    <Persist>true</Persist>
    <Database>master</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

原书P4-P5 代码

using System;
public struct Name
{
	private string fname, mname, lname;
	public Name(string first, string middle, string last)
	{
		fname = first;
		mname = middle;
		lname = last;
	}
	public string firstName
	{
		get
		{
			return fname;
		}
		set
		{
			fname = firstName;
		}
	}
	public string middleName
	{
		get
		{
			return mname;
		}
		set
		{
			mname = middleName;
		}
	}
	public string lastName
	{
		get
		{
			return lname;
		}
		set
		{
			lname = lastName;
		}
	}
	public override string ToString()
	{
		return (String.Format("{0} {1} {2}", fname, mname,lname));
	}
	public string Initials()
	{
		return (String.Format("{0}{1}{2}", fname.Substring(0, 1),mname.Substring(0, 1), lname.Substring(0, 1)));
	}
}
public class NameTest
{
	static void Main()
	{
		Name myName = new Name("Michael", "Mason", "McMillan");
		string fullName, inits;
		fullName = myName.ToString();
		inits = myName.Initials();
		Console.WriteLine("My name is {0}.", fullName);
		Console.WriteLine("My initials are {0}.", inits);
	}
}


原书P5-P6 代码

using System;
public class IntStruct
{
	static void Main()
	{
		int num;
		string snum;
		Console.Write("Enter a number: ");
		snum = Console.ReadLine();
		num = Int32.Parse(snum);
		Console.WriteLine(num);
	}
}


原书P12 代码1

public void Add(Object item)
{
	InnerList.Add(item);
}


原书P12 代码2

public void Remove(Object item) 
{
	InnerList.Remove(item);
}


原书P13 代码1

public new int Count() 
{
	return InnerList.Count;
}


原书P13 代码2

public new void Clear() 
{
	InnerList.Clear();
}


原书P13-P14 代码

using System;
using System.Collections;
public class Collection : CollectionBase
{
	public void Add(Object item) 
	{
		InnerList.Add(item);
	}
	public void Remove(Object item) 
	{
		InnerList.Remove(item);
	}
	public new void Clear() 
	{
		InnerList.Clear();
	}
	public new int Count() 
	{
		return InnerList.Count;
	}
}
class chapter1
{
	static void Main()
	{
		Collection names = new Collection();
		names.Add("David");
		names.Add("Bernica");
		names.Add("Raymond");
		names.Add("Clayton");
		foreach (Object name in names)
		{
			Console.WriteLine(name);
		}
		Console.WriteLine("Number of names: " + names.Count());
		names.Remove("Raymond");
		Console.WriteLine("Number of names: " + names.Count());
		names.Clear();
		Console.WriteLine("Number of names: " + names.Count());
	}
}


原书P15 代码1

static void Swap<T>(ref T val1, ref T val2)
{
	T temp;
	temp = val1;
	val1 = val2;
	val2 = temp;
}


原书P15 代码2

using System;
class chapter1
{
	static void Main()
	{
		int num1 = 100;
		int num2 = 200;
		Console.WriteLine("num1: " + num1);
		Console.WriteLine("num2: " + num2);
		Swap<int>(ref num1, ref num2);
		Console.WriteLine("num1: " + num1);
		Console.WriteLine("num2: " + num2);
		string str1 = "Sam";
		string str2 = "Tom";
		Console.WriteLine("String 1: " + str1);
		Console.WriteLine("String 2: " + str2);
		Swap<string>(ref str1, ref str2);
		Console.WriteLine("String 1: " + str1);
		Console.WriteLine("String 2: " + str2);
	}
	static void Swap<T>(ref T val1, ref T val2)
	{
		T temp;
		temp = val1;
		val1 = val2;
		val2 = temp;
	}
}


原书P16 代码1

public class Node<T>
{
	T data;
	Node<T> link;
	public Node(T data, Node<T> link)
	{
		this.data = data;
		this.link = link;
   }
}


原书P16 代码2

Node<string> node1 = new Node<string>("Mike", null);
Node<string> node2 = new Node<string>("Raymond", node1);


原书P17 代码1
static void DisplayNums(int[] arr)
{
for (int i = 0; i <= arr.GetUpperBound(0); i++)
	Console.Write(arr[i] + " ");
}


原书P17 代码2

DateTime startTime;
TimeSpan endTime;
startTime = DateTime.Now;
endTime = DateTime.Now.Subtract(startTime);


原书P19 代码1

GC.Collect();


原书P19 代码2

GC.WaitForPendingFinalizers();


原书P19 代码3

TimeSpan startingTime;
startingTime = Process.GetCurrentProcess().Threads[0].UserProcessorTime;


原书P20 代码1

duration = Process.GetCurrentProcess().Threads[0].UserProcessorTime.Subtract(startingTime);


原书P20 代码2

using System;
using System.Diagnostics;
class chapter1
{
	static void Main()
	{
		int[] nums = new int[100000];
		BuildArray(nums);
		TimeSpan duration;
		DisplayNums(nums);
		DisplayNums(nums);
		DisplayNums(nums);
duration = Process.GetCurrentProcess().TotalProcessorTime;
		Console.WriteLine("Time: " + duration.TotalSeconds);
	}
	static void BuildArray(int[] arr)
	{
		for (int i = 0; i <= 99999; i++)
			arr[i] = i;
	}
	static void DisplayNums(int[] arr)
	{
		for (int i = 0; i <= arr.GetUpperBound(0); i++)
			Console.Write(arr[i] + " ");
	}
}


原书P21 代码

public class Timing
{
	TimeSpan startingTime;
	TimeSpan duration;
	public Timing()
	{
		startingTime = new TimeSpan(0);
		duration = new TimeSpan(0);
	}
	public void StopTime()
	{
		duration =
		Process.GetCurrentProcess().Threads[0].
		UserProcessorTime.Subtract(startingTime);

	}
	public void startTime()
	{
		GC.Collect();
		GC.WaitForPendingFinalizers();
		startingTime =
		Process.GetCurrentProcess().Threads[0].
		UserProcessorTime;
	}
	public TimeSpan Result()
	{
		return duration;
	}
}


原书P22-P23 代码

using System;
using System.Diagnostics;
using System.Threading;
public class Timing 
{
	TimeSpan duration;
	public Timing() 
	{
		duration = new TimeSpan(0);
	}
	public void stopTime() 
	{
		duration = Process.GetCurrentProcess().TotalProcessorTime;
	}
	public void startTime() 
	{
		GC.Collect();
		GC.WaitForPendingFinalizers();
	}
	public TimeSpan Result()
	{
		return duration;
	}
}
class chapter1
{
	static void Main()
	{
		int[] nums = new int[100000];
		BuildArray(nums);
		Timing tObj = new Timing();
		tObj.startTime();
		DisplayNums(nums);
		tObj.stopTime();
		Console.WriteLine("time (.NET): " + tObj.Result().TotalSeconds);
	}
	static void BuildArray(int[] arr)
	{
		for (int i = 0; i < 100000; i++)
			arr[i] = i;
	}
	static void DisplayNums(int[] arr)
	{
		for (int i = 0; i <= arr.GetUpperBound(0); i++)
			Console.Write(arr[i] + " ");
	}
}


原书P23-2 代码

startTime = Process.GetCurrentProcess().Threads[0].UserProcessorTime;


原书P23-3 代码

tObj.startTime();
