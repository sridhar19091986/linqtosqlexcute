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

原书P43-P44代码

using System;
class CArray
{
	private int[] arr;
	private int upper;
	private int numElements;
	public CArray(int size)
	{
		arr = new int[size];
		upper = size - 1;
		numElements = 0;
	}
	public void Insert(int item)
	{
		arr[numElements] = item;
		numElements++;
	}
	public void DisplayElements()
	{
		for (int i = 0; i <= upper; i++)
			Console.Write(arr[i] + " ");
	}
	public void Clear()
	{
		for (int i = 0; i <= upper; i++)
			arr[i] = 0;
		numElements = 0;
	}
	static void Main()
	{
		CArray nums = new CArray(50); 
		for (int i = 0; i <= 49; i++)
			nums.Insert(i);
		nums.DisplayElements();
		Console.ReadKey();
	}
}


原书P44-2 代码

static void Main()
{
	CArray nums = new CArray(10);
	Random rnd = new Random(100);
	for (int i = 0; i < 10; i++)
	{
		nums.Insert(rnd.Next(0, 100));
	}
	nums.DisplayElements();
}


原书P46 代码

public void BubbleSort()
{
	int temp;
	for (int outer = upper; outer >= 1; outer--)
	{
		for (int inner = 0; inner <= outer - 1; inner++)
		{
			if ((int)arr[inner] > arr[inner + 1])
			{
				temp = arr[inner];
				arr[inner] = arr[inner + 1];
				arr[inner + 1] = temp;
			}
		}
		this.DisplayElements();
	}
}


原书P47-1 代码

public void BubbleSort()
{
	int temp;
	for (int outer = upper; outer >= 1; outer--)
	{
		for (int inner = 0; inner <= outer - 1; inner++)
		{
			if ((int)arr[inner] > arr[inner + 1])
			{
				temp = arr[inner];
				arr[inner] = arr[inner + 1];
				arr[inner + 1] = temp;
			}
		}
		this.DisplayElements();
	}
}


原书P47-2 代码

static void Main()
{
	CArray nums = new CArray(10);
	Random rnd = new Random(100);
	for (int i = 0; i < 10; i++)
	{
		nums.Insert(rnd.Next(0, 100));
	}
	Console.WriteLine("Before sorting: ");
	nums.DisplayElements();
	Console.WriteLine("During sorting: ");
	nums.BubbleSort();
	Console.WriteLine("After sorting: ");
	nums.DisplayElements();
}


原书P48 代码

public void SelectionSort()
{
	int min, temp;
	for (int outer = 0; outer <= upper; outer++)
	{
		min = outer;
		for (int inner = outer + 1; inner <= upper; inner++)
		{
			if (arr[inner] < arr[min]) min = inner;
		}
		temp = arr[outer];
		arr[outer] = arr[min];
		arr[min] = temp;
		this.DisplayElements();
	}
}


原书P50 代码

public void InsertionSort()
{
	int inner, temp;
	for (int outer = 1; outer <= upper; outer++)
	{
		temp = arr[outer];
		inner = outer;
		while (inner > 0 && arr[inner - 1] >= temp)
		{
			arr[inner] = arr[inner - 1];
			inner -= 1;
		}
		arr[inner] = temp;
		this.DisplayElements();
	}
}


原书P51-P52代码

static void Main()
{
	Timing sortTime = new Timing();
	Random rnd = new Random(100);
	int numItems = 1000;
	CArray theArray = new CArray(numItems);
	for (int i = 0; i < numItems; i++)
		theArray.Insert(rnd.NextDouble() * 100);
	sortTime.startTime();
	theArray.SelectionSort();
	sortTime.stopTime();
	Console.WriteLine("Time for Selection sort: " + sortTime.Result().TotalMilliseconds); 
	theArray.Clear();
	for (int i = 0; i < numItems; i++)
		theArray.Insert(rnd.NextDouble() * 100);
	sortTime.startTime();
	theArray.BubbleSort();
	sortTime.stopTime();
	Console.WriteLine("Time for Bubble sort: " + sortTime.Result().TotalMilliseconds);
	theArray.Clear();
	for (int i = 0; i < numItems; i++)
		theArray.Insert(rnd.NextDouble() * 100);
	sortTime.startTime();
	theArray.InsertionSort();
	sortTime.stopTime();
	Console.WriteLine("Time for Insertion sort: " + sortTime.Result().TotalMilliseconds);
}
