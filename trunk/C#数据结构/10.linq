<Query Kind="Expression">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

原书P177-P178 代码

using System;
class chapter10
{
	static void Main()
	{
		string[] names = new string[99];
		string name;
		string[] someNames = new string[]{"David","Jennifer", "Donnie", "Mayo", "Raymond",
			"Bernica", "Mike", "Clayton", "Beata", "Michael"};
		int hashVal;
		for (int i = 0; i < 10; i++)
		{
			name = someNames[i];
			hashVal = SimpleHash(name, names);
			names[hashVal] = name;
		}
		ShowDistrib(names);
	}
	static int SimpleHash(string s, string[] arr)
	{
		int tot = 0;
		char[] cname;
		cname = s.ToCharArray();
		for (int i = 0; i <= cname.GetUpperBound(0); i++)
			tot += (int)cname[i];
		return tot % arr.GetUpperBound(0);
	}
	static void ShowDistrib(string[] arr)
	{
		for (int i = 0; i <= arr.GetUpperBound(0); i++)
			if (arr[i] != null)
				Console.WriteLine(i + " " + arr[i]);
	}
}


原书P179 代码

static int BetterHash(string s, string[] arr)
{
	long tot = 0;
	char[] cname;
	cname = s.ToCharArray();
	for (int i = 0; i <= cname.GetUpperBound(0); i++)
		tot += 37 * tot + (int)cname[i];
	tot = tot % arr.GetUpperBound(0);
	if (tot < 0)
		tot += arr.GetUpperBound(0);
	return (int)tot;
}


原书P180 代码

static bool InHash(string s, string[] arr)
{
	int hval = BetterHash(s, arr);
	if (arr[hval] == s)
		return true;
	else
		return false;
}


原书P181-P182 代码

public class BucketHash
{
	private const int SIZE = 101;
	ArrayList[] data;
	public BucketHash()
	{
		data = new ArrayList[SIZE];
		for (int i = 0; i <= SIZE - 1; i++)
			data[i] = new ArrayList(4);
	}
	public int Hash(string s)
	{
		long tot = 0;
		char[] charray;
		charray = s.ToCharArray();
		for (int i = 0; i <= s.Length - 1; i++)
			tot += 37 * tot + (int)charray[i];
		tot = tot % data.GetUpperBound(0);
		if (tot < 0)
			tot += data.GetUpperBound(0);
		return (int)tot;
	}
	public void Insert(string item)
	{
		int hash_value;
		hash_value = Hash(item);
		if (data[hash_value].Contains(item))
			data[hash_value].Add(item);
	}
	public void Remove(string item)
	{
		int hash_value;
		hash_value = Hash(item);
		if (data[hash_value].Contains(item))
			data[hash_value].Remove(item);
	}
}


原书P184 代码

Hashtable symbols = new Hashtable();
Hashtable symbols = new Hashtable(50);
Hashtable symbols = new Hashtable(25, 3.0F);


原书P185 代码1

Hashtable symbols = new Hashtable(25);
symbols.Add("salary", 100000);
symbols.Add("name", "David Durr");
symbols.Add("age", 43);
symbols.Add("dept", "Information Technology");


原书P185 代码2

symbols["sex"] = "Male";
symbols["age"] = 44;


原书P186 代码1

using System;
using System.Collections;
class chapter10
{
	static void Main()
	{
		Hashtable symbols = new Hashtable(25);
		symbols.Add("salary", 100000);
		symbols.Add("name", "David Durr");
		symbols.Add("age", 45);
		symbols.Add("dept", "Information Technology");
		symbols["sex"] = "Male";
		Console.WriteLine("The keys are: ");
		foreach (Object key in symbols.Keys)
			Console.WriteLine(key);
		Console.WriteLine();
		Console.WriteLine("The values are: ");
		foreach (Object value in symbols.Values)
			Console.WriteLine(value);
	}
}


原书P186 代码2

Object value = symbols["name"];
Console.WriteLine("The variable name's value is: " + value.ToString());


原书P186-P187 代码

using System;
using System.Collections;
class chapter10
{
	static void Main()
	{
		Hashtable symbols = new Hashtable(25);
		symbols.Add("salary", 100000);
		symbols.Add("name", "David Durr");
		symbols.Add("age", 45);
		symbols.Add("dept", "Information Technology");
		symbols["sex"] = "Male";
		Console.WriteLine();
		Console.WriteLine("Hash table dump - ");
		Console.WriteLine();
		foreach (Object key in symbols.Keys)
			Console.WriteLine(key.ToString() + ": " + symbols[key].ToString());
	}
}


原书P188 代码1

int numElements;
numElements = symbols.Count;


原书P188 代码2

symbols.Clear();


原书P188 代码3

symbols.Remove("sex");
foreach (Object key in symbols.Keys)
Console.WriteLine(key.ToString() + ": " + symbols[key].ToString());


原书P188 代码4

string aKey;
Console.Write("Enter a key to remove: ");
aKey = Console.ReadLine();
if (symbols.ContainsKey(aKey))
symbols.Remove(aKey);


原书P189-P191 代码

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace WindowsApplication3
{
	public partial class Form1 : Form
	{
		private Hashtable glossary = new Hashtable();
		public Form1()
		{
			InitializeComponent();
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			BuildGlossary(glossary);
			DisplayWords(glossary);
		}
		private void BuildGlossary(Hashtable g)
		{
			StreamReader inFile;
			string line;
			string[] words;
			inFile = File.OpenText(@"c:\words.txt");
			char[] delimiter = new char[] { ',' };
			while (inFile.Peek() != -1)
			{
				line = inFile.ReadLine();
				words = line.Split(delimiter);
				g.Add(words[0], words[1]);
			}
			inFile.Close();
		}
		private void DisplayWords(Hashtable g)
		{
			Object[] words = new Object[100];
			g.Keys.CopyTo(words, 0);
			for (int i = 0; i <= words.GetUpperBound(0); i++)
				if (!(words[i] == null))
					lstWords.Items.Add((words[i]));
		}
		private void lstWords_SelectedIndexChanged(object sender, EventArgs e)
		{
			Object word;
			word = lstWords.SelectedItem;
			txtDefinition.Text = glossary[word].ToString();
		}
	}
}
