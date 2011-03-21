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

原书P166-P167 代码

public class IPAddresses : DictionaryBase
{
	public IPAddresses()
	{
	}
	public void Add(string name, string ip)
	{
		base.InnerHashtable.Add(name, ip);
	}
	public string Item(string name)
	{
		return base.InnerHashtable[name].ToString();
	}
	public void Remove(string name)
	{
		base.InnerHashtable.Remove(name);
	}
}


原书P167-P168 代码

class chapter9
{
	static void Main()
	{
		IPAddresses myIPs = new IPAddresses();
		myIPs.Add("Mike", "192.155.12.1");
		myIPs.Add("David", "192.155.12.2");
		myIPs.Add("Bernica", "192.155.12.3");
		Console.WriteLine("There are " + myIPs.Count + " IP addresses");
		Console.WriteLine("David's ip address: " + myIPs.Item("David"));
		myIPs.Clear();
		Console.WriteLine("There are " + myIPs.Count + " IP addresses");
	}
}


原书P168 代码2

public IPAddresses(string txtFile)
{
	string line;
	string[] words;
	StreamReader inFile;
	inFile = File.OpenText(txtFile);
	while (inFile.Peek() != -1)
	{
		line = inFile.ReadLine();
		words = line.Split(',');
		this.InnerHashtable.Add(words[0], words[1]);
	}
	inFile.Close();
}


原书P168-P169 代码

class chapter9
{
	static void Main()
	{
		for (int i = 0; i < 4; i++)
			Console.WriteLine();
		IPAddresses myIPs = new IPAddresses(@"c:\data\ips.txt");
		Console.WriteLine("There are {0} IP addresses", myIPs.Count);
		Console.WriteLine("David's IP address: " + myIPs.Item("David"));
		Console.WriteLine("Bernica's IP address: " + myIPs.Item("Bernica"));
		Console.WriteLine("Mike's IP address: " + myIPs.Item("Mike"));
	}
}


原书P169 代码2

IPAddresses myIPs = new IPAddresses(@"c:\ips.txt");
DictionaryEntry[] ips = new DictionaryEntry[myIPs.Count-1];
myIPs.CopyTo(ips, 0);


原书P170 代码1

for (int i = 0; i <= ips.GetUpperBound(0); i++)
	Console.WriteLine(ips[i].ToString());


原书P170 代码2

Console.WriteLine(ips[index].ToString());


原书P170 代码3

for(int i = 0; i <= ips.GetUpperBound(0); i++) 
{
Console.WriteLine(ips[index].Key);
Console.WriteLine(ips[index].Value);
}


原书P171 代码1

KeyValuePair<string, int> mcmillan = new KeyValuePair<string, int>("McMillan", 99);


原书P171 代码2

Console.Write(mcmillan.Key);
Console.Write(" " + mcmillan.Value);


原书P171-P172 代码

using System;
using System.Collections.Generic;
using System.Text;
namespace Generics
{
	class Program
	{
		static void Main(string[] args)
		{

			KeyValuePair<string, int>[] gradeBook = new KeyValuePair<string, int>[10];
			gradeBook[0] = new KeyValuePair<string,int>("McMillan", 99);
			gradeBook[1] = new KeyValuePair<string,int>("Ruff", 64);
			for (int i = 0; i <= gradeBook.GetUpperBound(0); i++)
				if (gradeBook[i].Value != 0)
					Console.WriteLine(gradeBook[i].Key + ": " + gradeBook[i].Value);
			Console.Read();
		}
	}
}


原书P172 代码2

SortedList myips = new SortedList();
myips.Add("Mike", "192.155.12.1");
myips.Add("David", "192.155.12.2");
myips.Add("Bernica", "192.155.12.3");


原书P173 代码1

SortedList<Tkey, TValue>


原书P173 代码2

SortedList<string, string> myips = new SortedList<string, string>();


原书P173 代码3

SortedList<string, int> gradeBook = new SortedList<string, int>();


原书P173 代码4

foreach(Object key in myips.Keys)
Console.WriteLine("Name: " + key + "\n" + "IP: " + myips[key]);


原书P173 代码5

for(int i = 0; i < myips.Count; i++)
Console.WriteLine("Name: " + myips.GetKey(i) + "\n" + "IP: " + myips.GetByIndex(i));


原书P174 代码1

myips.Remove("David");
myips.RemoveAt(1);


原书P174 代码2

int indexDavid = myips.IndexOfKey("David");
int indexIPDavid = myips.IndexOfValue(myips["David"]);
