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

原书P56-1代码

bool SeqSearch(int[] arr, int sValue)
{
	for (int index = 0; index < arr.Length ; index++) //小bug
		if (arr[index] == sValue)
			return true;
	return false;
}


原书P56-P57 代码

using System;
using System.IO;
public class  Chapter4
{
	static void Main()
	{
		int[] numbers = new int[100];
		StreamReader numFile = File.OpenText(@"c:\numbers.txt");
		for (int i = 0; i < numbers.Length ; i++) //小bug
			numbers[i] = Convert.ToInt32(numFile.ReadLine(), 10);
		int searchNumber;
		Console.Write("Enter a number to search for: ");
		searchNumber = Convert.ToInt32(Console.ReadLine(),10);
		bool found;
		found = SeqSearch(numbers, searchNumber);
		if (found)
			Console.WriteLine(searchNumber + " is in the array.");
		else
			Console.WriteLine(searchNumber + " is not in the array.");
	}
	static bool SeqSearch(int[] arr, int sValue)
	{
		for (int index = 0; index < arr.Length ; index++) //小bug
			if (arr[index] == sValue)
				return true;
		return false;
	}
}


原书P57-1代码

static int SeqSearch(int[] arr, int sValue)
{
	for (int index = 0; index < arr.Length ; index++) //小bug
		if (arr[index] == sValue)
			return index;
	return -1;
}


原书P57-P58 代码

using System;
using System.IO;
public class Chapter4
{
	static void Main()
	{
		int[] numbers = new int[100];
		StreamReader numFile = File.OpenText(@"c:\numbers.txt"); 
		for (int i = 0; i < numbers.Length; i++) 
			numbers[i] = Convert.ToInt32(numFile.ReadLine(), 10);
		int searchNumber;
		Console.Write("Enter a number to search for: ");
		searchNumber = Convert.ToInt32(Console.ReadLine(), 10);
		int foundAt;
		foundAt = SeqSearch(numbers, searchNumber);
		if (foundAt >= 0)
			Console.WriteLine(searchNumber + " is in the array at position " + foundAt); 
		else
			Console.WriteLine(searchNumber + " is not in the array.");
	}
	static int SeqSearch(int[] arr, int sValue)
	{
		for (int index = 0; index < arr.Length ; index++) //小bug
			if (arr[index] == sValue)
				return index;
		return -1;
	}
}


原书P59-1代码

static int FindMin(int[] arr) 
{
int min = arr[0];
for(int i = 0; i < arr.Length; i++)
if (arr[i] < min)
min = arr[i];
return min;
}


原书P59-2代码

static int FindMax(int[] arr) 
{
int max = arr[0];
for(int i = 0; i < arr.Length; i++)
if (arr[i] > max)
max = arr[i];
return max;
}


原书P60-1代码

static bool SeqSearch(int sValue) 
{
for(int index = 0; index < arr.Length; index++)
if (arr[index] == sValue) 
{
swap(index, index-1);
return true;
}
return false;
}


原书P60-2代码

static void swap( int item1,  int item2) 
{
int temp = arr[item1];
arr[item1] = arr[item2];
arr[item2] = temp;
}


原书P61-1代码

static int SeqSearch(int sValue) 
{
for(int index = 0; index < arr.Length; index++)
if (arr[index] == sValue && index > (arr.Length * 0.2)) 
{
swap(index, index-1);
return index;
} 
else
if (arr[index] == sValue)
return index;
return -1;
}


原书P61-2代码

static int SeqSearch(int sValue) 
{
for(int index = 0; index < arr.Length; index++)
if (arr[index] == sValue) 
{
swap(index, index-1);
return index;
}
return -1;
}


原书P63-P64代码

public int binSearch(int value)
{
	int upperBound, lowerBound, mid;
	upperBound = arr.Length - 1;
	lowerBound = 0;
	while (lowerBound <= upperBound)
	{
		mid = (upperBound + lowerBound) / 2;
		if (arr[mid] == value)
			return mid;
		else
			if (value < arr[mid])
				upperBound = mid - 1;
			else
				lowerBound = mid + 1;
	}
	return -1;
}


原书P64-2代码

static void Main(string[] args)
{
Random random = new Random();
CArray mynums = new CArray(10);
for(int i = 0; i <= 9; i++)
mynums.Insert(random.Next(100));
mynums.BubbleSort();
mynums.DisplayElements();
int position = mynums.binSearch(77);
if (position >= -1)
{
Console.WriteLine("found item");
mynums.DisplayElements();
}
else
Console.WriteLine("Not in the array");
Console.Read();
}


原书P65 代码

public int RbinSearch(int value, int lower, int upper) 
{
	if (lower > upper)
		return -1;
	else 
	{
		int mid;
		mid = (int)(upper+lower) / 2;
		if (value < arr[mid])
			return RbinSearch(value, lower, mid - 1); 
		else if (value == arr[mid]) 
			return mid;
		else
			return RbinSearch(value, mid + 1, upper);
	}
}


原书P66代码

public int Bsearh(int value) 
{
return Array.BinarySearch(arr, value);
}
