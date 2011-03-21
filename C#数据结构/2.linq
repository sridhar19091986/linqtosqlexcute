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

原书P27-1 代码

type[] array-name;


原书P27-2 代码

string[] names;


原书P27-3 代码

names = new string[10];


原书P27-4 代码

string[] names = new string[10];


原书P27-5 代码

int[] numbers = new int[] { 1, 2, 3, 4, 5 };


原书P28-1 代码

names[2] = "Raymond";
sales[19] = 23123;


原书P28-2 代码

names.SetValue("Raymond", 2);
sales.SetValue(23123, 9);


原书P28-3 代码

myName = names[2];
monthSales = sales.GetValue(19);


原书P28-4 代码
for( int i = 0; i <= sales.GetUpperBound(0); i++)
totalSales = totalSales + sales[i];


原书P29 代码

int[] numbers;
numbers = new int[] { 0, 1, 2, 3, 4 };
Type arrayType = numbers.GetType();
if (arrayType.IsArray)
Console.WriteLine("The array type is: {0}", arrayType);
else
	Console.WriteLine("Not an array");
	Console.Read();


原书P30-1 代码

int[,] grades = new int[4,5];


原书P30-2 代码

double[,] Sales;


原书P30-3 代码

double[,,] sales;


原书P30-4 代码

sales = new double[4,5];


原书P30-5 代码

int[,] grades = new int[,] 
{
{1, 82, 74, 89, 100},
{2, 93, 96, 85, 86},
{3, 83, 72, 95, 89},
{4, 91, 98, 79, 88}
};


原书P31-1 代码

grade = grades[2,2];
grades[2,2] = 99


原书P31-2 代码

grade = Grades.GetValue(0,2);


原书P31-3 代码

int[,] grades = new int[,] 
{
{1, 82, 74, 89, 100},
{2, 93, 96, 85, 86},
{3, 83, 72, 95, 89},
{4, 91, 98, 79, 88}
};
int last_grade = grades.GetUpperBound(1);
double average = 0.0;
int total;
int last_student = grades.GetUpperBound(0);
for(int row = 0; row <= last_student; row++) 
{
total = 0;
for (int col = 0; col <= last_grade; col++)
total += grades[row, col];
average = total / last_grade;
Console.WriteLine("Average: " + average);
}


原书P32-1 代码

static int sumNums(params int[] nums)
{
int sum = 0;
for (int i = 0; i <= nums.GetUpperBound(0); i++)
	sum += nums[i];
return sum;
}


原书P32-2 代码

total = sumNums(1, 2, 3);
total = sumNums(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);


原书P32-3 代码

int sales[,] = new int[12,30]; //Sales for each day of each month


原书P33-1 代码

int[][] jagged = new int[12][];


原书P33-2 代码

jagged[0][0] = 23;
jagged[0][1] = 13;
. . .
jagged[7][5] = 45;


原书P34 代码

using System;
class class1
{
	static void Main()
	{
		int[] Jan = new int[31];
		int[] Feb = new int[29];
		int[][] sales = new int[][] { Jan, Feb };
		int month, day, total;
		double average = 0.0;
		sales[0][0] = 41;
		sales[0][1] = 30;
		sales[0][0] = 41;
		sales[0][1] = 30;
		sales[0][2] = 23;
		sales[0][3] = 34;
		sales[0][4] = 28;
		sales[0][5] = 35;
		sales[0][6] = 45;
		sales[1][0] = 35;
		sales[1][1] = 37;
		sales[1][2] = 32;
		sales[1][3] = 26;
		sales[1][4] = 45;
		sales[1][5] = 38;
		sales[1][6] = 42;
		for(month = 0; month <= 1; month++) 
		{
			total = 0;
			for(day = 0; day <= 6; day++)
			{
				total += sales[month][day];
			}
			average = total / 7;
			Console.WriteLine("Average sales for month: " +month + ": " + average);
		}
	}
}


原书P36-1 代码

ArrayList grades = new ArrayList();


原书P36-2 代码

grades.Add(100);
grades.Add(84);
int position;
position = grades.Add(77);
Console.WriteLine("The grade 77 was added at position:" + position);


原书P37-1 代码

int total = 0;
double average = 0.0;
foreach (Object grade in grades)
total += (int)grade;
average = total / grades.Count;
Console.WriteLine("The average grade is: " + average);


原书P37-2 代码

grades.Insert(1, 99);
grades.Insert(3, 80);


原书P37-3 代码

Console.WriteLine("The current capacity of grades is:" + grades.Capacity);
Console.WriteLine("The number of grades in grades is:" + grades.Count);


原书P37-4 代码

if (grades.Contains(54))
grades.Remove(54)
else
Console.Write("Object not in ArrayList.");


原书P38-1 代码

grades.RemoveAt(2)


原书P38-2 代码

int pos;
pos = grades.IndexOf(70);
grades.RemoveAt(pos);


原书P38-P39 代码

using System;
using System.Collections;
class class1
{
	static void Main()
	{
		ArrayList names = new ArrayList();
		names.Add("Mike");
		names.Add("Beata");
		names.Add("Raymond");
		names.Add("Bernica");
		names.Add("Jennifer");
		Console.WriteLine("The original list of names: ");
		foreach (Object name in names)
			Console.WriteLine(name);
		Console.WriteLine();
		string[] newNames = new string[] { "David", "Michael" };
		ArrayList moreNames = new ArrayList();
		moreNames.Add("Terrill");
		moreNames.Add("Donnie");
		moreNames.Add("Mayo");
		moreNames.Add("Clayton");
		moreNames.Add("Alisa");
		names.InsertRange(0, newNames);
		names.AddRange(moreNames);
		Console.WriteLine("The new list of names: ");
		foreach (Object name in names)
			Console.WriteLine(name);
	}
}


原书P40-1 代码

ArrayList someNames = new ArrayList();
someNames = names.GetRange(2, 4);
Console.WriteLine("someNames sub-ArrayList: ");
foreach (Object name in someNames)
Console.WriteLine(name);


原书P40-2 代码

Object[] arrNames;
arrNames = names.ToArray();
Console.WriteLine("Names from an array: ");
for(int i = 0; i <= arrNames.GetUpperBound(0); i++)
Console.WriteLine(arrNames[i]);
