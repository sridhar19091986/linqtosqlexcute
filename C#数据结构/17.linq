<Query Kind="Expression">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

原书P315 代码1

static long recurFib(int n) 
{
if (n < 2)
		return n;
else
		return recurFib(n - 1) + recurFib(n - 2);
}


原书P315 代码2

static void Main()
{
int num = 5;
long fibNumber = recurFib(num);
	Console.Write(fibNumber);
}


原书P316 代码

	static long iterFib(int n)
	{
		int[] val = new int[n];
		if ((n == 1) || (n == 2))
			return 1;
		else
		{
			val[1] = 1;
			val[2] = 2;
			for (int i = 3; i <= n - 1; i++)
				val[i] = val[i - 1] + val[i - 2];
		}
		return val[n - 1];
	}


原书P317 代码

static void Main()
{
Timing tObj = new Timing();
	Timing tObj1 = new Timing();
	int num = 35;
	long fibNumber;
	tObj.startTime();
	fibNumber = recurFib(num);
	tObj.stopTime();
	Console.WriteLine("Calculating Fibonacci number: " + num);
	Console.WriteLine(fibNumber + " in: " + tObj.Result().TotalMilliseconds);
	tObj1.startTime();
	fibNumber = iterFib(num);
	tObj1.stopTime();
	Console.WriteLine(fibNumber + " in: " + tObj1.Result().TotalMilliseconds);
}


原书P318 代码

static long iterFib1(int n)
{
	long last, nextLast, result;
	last = 1;
nextLast = 1;
	result = 1;
	for (int i = 2; i <= n - 1; i++)
	{
		result = last + nextLast;
		nextLast = last;
		last = result;
	}
	return result;
}


原书P319-P320 代码

using System;
class chapter17
{
	static void LCSubstring(string word1, string word2, string[] warr1, string[] warr2, int[,] arr)
	{
		int len1, len2;
		len1 = word1.Length;
		len2 = word2.Length;
		for (int k = 0; k <= word1.Length - 1; k++)
		{
			warr1[k] = word1.Substring(k, 1);
			warr2[k] = word2.Substring(k, 1);
		}
		for (int i = len1 - 1; i >= 0; i--)
		{
			for (int j = len2 - 1; j >= 0; j--)
				if (warr1[i] == warr2[j])
					arr[i, j] = 1 + arr[i + 1, j + 1];
				else
					arr[i, j] = 0;
		}
	}
	static string ShowString(int[,] arr, string[] wordArr)
	{
		string substr = "";
		for (int i = 0; i <= arr.GetUpperBound(0); i++)
			for (int j = 0; j <= arr.GetUpperBound(1); j++)
				if (arr[i, j] > 0)
					substr += wordArr[j];
		return substr;
	}
	static void DispArray(int[,] arr)
	{
		for (int row = 0; row <= arr.GetUpperBound(0); row++)
		{
			for (int col = 0; col <= arr.GetUpperBound(1); col++)
				Console.Write(arr[row, col]);
			Console.WriteLine();
		}
	}
	static void Main()
	{
		string word1 = "mavens";
		string word2 = "hpavoc";
		string[] warray1 = new string[word1.Length];
		string[] warray2 = new string[word2.Length];
		string substr;
		int[,] larray = new int[word1.Length, word2.Length];
		LCSubstring(word1, word2, warray1, warray2, larray);
		Console.WriteLine();
		DispArray(larray);
		substr = ShowString(larray, warray1);
		Console.WriteLine();
		Console.WriteLine("The strings are: " + word1 + " " + word2);
		if (substr.Length > 0)
			Console.WriteLine("The longest common substring is: " + substr);
		else
			Console.WriteLine("There is no common substring");
	}
}


原书P322-P323 代码

using System;
class chapter17
{
	static void Main()
	{
		int capacity = 16;
		int[] size = new int[] { 3, 4, 7, 8, 9 };
		int[] values = new int[] { 4, 5, 10, 11, 13 };
		int[] totval = new int[capacity+1];
		int[] best = new int[capacity+1];
		int n = values.Length;
		for (int j = 0; j <= n - 1; j++)
			for (int i = 0; i <= capacity; i++)
				if (i >= size[j])
					if (totval[i] < (totval[i - size[j]] + values[j]))
					{
						totval[i] = totval[i - size[j]] + values[j];
						best[i] = j;
					}
		Console.WriteLine("The maximum value is: " + totval[capacity]);
	}
}


原书P323	代码2

if (totval[i] < (totval[i - size[j]] + values[j]))
{
totval[i] = totval[i - size[j]] + values[j];
best[i] = j;
}


原书P323	代码3

Console.WriteLine("The items that generate this value are: ");
int totcap = 0;
while (totcap <= capacity)
{
	Console.WriteLine("Item with best value: " + size[best[capacity - totcap]]);
	totcap += size[best[i]];
}


原书P324-P326 代码

using System;
class chapter17
{
	static void MakeChange(double origAmount, double remainAmount, int[] coins)
	{
		if ((origAmount % 0.25) < origAmount)
		{
			coins[3] = (int)(origAmount / 0.25);
			remainAmount = origAmount % 0.25;
			origAmount = remainAmount;
		}
		if ((origAmount % 0.1) < origAmount)
		{
			coins[2] = (int)(origAmount / 0.1);
			remainAmount = origAmount % 0.1;
			origAmount = remainAmount;
		}
		if ((origAmount % 0.05) < origAmount)
		{
			coins[1] = (int)(origAmount / 0.05);
			remainAmount = origAmount % 0.05;
			origAmount = remainAmount;
		}
		if ((origAmount % 0.01) < origAmount)
		{
			coins[0] = (int)(origAmount / 0.01);
			remainAmount = origAmount % 0.01;
		}
	}
	static void ShowChange(int[] arr)
	{
		if (arr[3] > 0)
			Console.WriteLine("Number of quarters: " + arr[3]);
		if (arr[2] > 0)
			Console.WriteLine("Number of dimes: " + arr[2]);
		if (arr[1] > 0)
			Console.WriteLine("Number of nickels: " + arr[1]);
		if (arr[0] > 0)
			Console.WriteLine("Number of pennies: " + arr[0]);
	}
	static void Main()
	{
		double origAmount = 0.63;
		double toChange = origAmount;
		double remainAmount = 0.0;
		int[] coins = new int[4];
		MakeChange(origAmount, remainAmount, coins);
		Console.WriteLine("The best way to change " + toChange + " cents is: ");
		ShowChange(coins);
	}
}


原书P327 代码

public class Node
{
	public HuffmanTree data;
	public Node link;
	public Node(HuffmanTree newData)
	{
		data = newData;
	}
}


原书P329-P330 代码

public class TreeList 
{
	private int count = 0;
	private Node first = null;
	private static string[] signTable = null;
	private static string[] keyTable = null;

	public TreeList(string input)
	{
		List<char> list = new List<char>();
		for (int i = 0; i < input.Length;i++ )
		{
			if (!list.Contains(input[i]))
				list.Add(input[i]);
		}
		signTable = new string[list.Count];
		keyTable = new string[list.Count];
	}
	public string[] GetSignTable()
	{
		return signTable;
	}
	public string[] GetKeyTable()
	{
		return keyTable;
	}
	public void AddLetter(string letter)
	{
		HuffmanTree hTemp = new HuffmanTree(letter);
		Node eTemp = new Node(hTemp);
		if (first == null)
			first = eTemp;
		else
		{
			eTemp.link = first;
			first = eTemp;
		}
		count++;
	}
	public void SortTree()
	{
		if (first != null && first.link != null)
		{
			Node tmp1;
			Node tmp2;
			for(tmp1 = first;tmp1!=null;tmp1 = tmp1.link)
				for (tmp2 = tmp1.link; tmp2 != null; tmp2 = tmp2.link)
				{
					if (tmp1.data.GetFreq() > tmp2.data.GetFreq())
					{
						HuffmanTree tmpHT = tmp1.data;
						tmp1.data = tmp2.data;
						tmp2.data = tmpHT;
					}
				}
		}
	}
	public void MergeTree()
	{
		if (!(first == null))
			if (!(first.link == null))
			{
				HuffmanTree aTemp = RemoveTree();
				HuffmanTree bTemp = RemoveTree();
				HuffmanTree sumTemp = new HuffmanTree("x");
				sumTemp.SetLeftChild(aTemp);
				sumTemp.SetRightChild(bTemp);
				sumTemp.SetFreq(aTemp.GetFreq() + bTemp.GetFreq());
				InsertTree(sumTemp);
			}
	}
	public HuffmanTree RemoveTree()
	{
		if (!(first == null))
		{
			HuffmanTree hTemp;
			hTemp = first.data;
			first = first.link;
			count--;
			return hTemp;
		}
		return null;
	}
	public void InsertTree(HuffmanTree hTemp)
	{
		Node eTemp = new Node(hTemp);
		if (first == null)
			first = eTemp;
		else
		{
			Node p = first;
			while (!(p.link == null))
			{
				if ((p.data.GetFreq() <= hTemp.GetFreq()) && (p.link.data.GetFreq() >= hTemp.GetFreq()))
					break;
				p = p.link;
			}
			eTemp.link = p.link;
			p.link = eTemp;
		}
		count++;
	}
	public int Length()
	{
		return count;
	}
	public void AddSign(string str)
	{
		if (first == null)
		{
			AddLetter(str);
			return;
		}
		Node tmp = first;
		while (tmp != null)
		{
			if (tmp.data.GetSign() == str)
			{
				tmp.data.IncFreq();
				return;
			}
			tmp = tmp.link;
		}
		AddLetter(str);
	}
	static string translate(string original)
	{
		string newStr = "";
		for (int i = 0; i <= original.Length - 1; i++)
			for (int j = 0; j <= signTable.Length - 1; j++)
				if (original[i].ToString() == signTable[j])
					newStr += keyTable[j];
		return newStr;
	}
	static int pos = 0;
	static void MakeKey(HuffmanTree tree, string code)
	{
		if (tree.GetLeftChild() == null)
		{
			signTable[pos] = tree.GetSign();
			keyTable[pos] = code;
			pos++;
		}
		else
		{
			MakeKey(tree.GetLeftChild(), code + "0");
			MakeKey(tree.GetRightChild(), code + "1");
		}
	}
}


原书P331 代码

public class HuffmanTree
{
	private HuffmanTree leftChild;
	private HuffmanTree rightChild;
	private string letter;
	private int freq ;
	public HuffmanTree(string letter)
	{
		this.letter = letter;
		this.freq = 1;
	}
	public void SetLeftChild(HuffmanTree newChild)
	{
		leftChild = newChild;
	}
	public void SetRightChild(HuffmanTree newChild)
	{
		rightChild = newChild;
	}
	public void SetLetter(string newLetter)
	{
		letter = newLetter;
	}
	public void IncFreq()
	{
		freq++;
	}
	public void SetFreq(int newFreq)
	{
		freq = newFreq;
	}
	public HuffmanTree GetLeftChild()
	{
		return leftChild;
	}
	public HuffmanTree GetRightChild()
	{
		return rightChild;
	}
	public int GetFreq()
	{
		return freq;
	}
	public string GetSign()
	{
		return letter;
	}
}


原书P332-P333 代码

static void Main()
{
	string input;
	Console.Write("Enter a string to encode: ");
	input = Console.ReadLine();
	TreeList treeList = new TreeList(input);
	for (int i = 0; i < input.Length; i++)
		treeList.AddSign(input[i].ToString());
	treeList.SortTree();
	while (treeList.Length() > 1)
		treeList.MergeTree();
	MakeKey(treeList.RemoveTree(), "");
	string newStr = translate(input);
	string[] signTable = treeList.GetSignTable();
	string[] keyTable = treeList.GetKeyTable();
	for (int i = 0; i <= signTable.Length - 1; i++)
		Console.WriteLine(signTable[i] + ": " + keyTable[i]);
	Console.WriteLine("The original string is " + input.Length * 16 + " bits long.");
	Console.WriteLine("The new string is " + newStr.Length + " bits long.");
	Console.WriteLine("The coded string looks like this:" + newStr);
}


原书P334-P336 代码

using System;
using System.Collections;
public class Carpet : IComparable
{
	private string item;
	private float val;
	private int unit;
	public Carpet(string i, float v, int u)
	{
		item = i;
		val = v;
		unit = u;
	}
	public int CompareTo(Object c)
	{
		return (this.val.CompareTo(((Carpet)c).val));
	}
	public int GetUnit()
	{
		return unit;
	}
	public string GetItem()
	{
		return item;
	}
	public float GetVal()
	{
		return val * unit;
	}
	public float ItemVal()
	{
		return val;
	}
}
public class Knapsack
{
	private float quantity;
	SortedList items = new SortedList();
	string itemList;
	public Knapsack(float max)
	{
		quantity = max;
	}
	public void FillSack(ArrayList objects)
	{
		int pos = objects.Count - 1;
		int totalUnits = 0;
		float totalVal = 0.0F;
		int tempTot = 0;
		while (totalUnits < quantity)
		{
			tempTot += ((Carpet)objects[pos]).GetUnit();
			if (tempTot <= quantity)
			{
				totalUnits += ((Carpet)objects[pos]).GetUnit();
				totalVal += ((Carpet)objects[pos]).GetVal();
				items.Add(((Carpet)objects[pos]).GetItem(), ((Carpet)objects[pos]).GetUnit());
			}
			else
			{
				float tempUnit = quantity - totalUnits;
				float tempVal = ((Carpet)objects[pos]).ItemVal() * tempUnit;
				totalVal += tempVal;
				totalUnits += (int)tempUnit;
				items.Add(((Carpet)objects[pos]).GetItem(), tempUnit);
			}
			pos--;
		}
	}
	public string GetItems()
	{
		foreach (Object k in items.GetKeyList())
			itemList += k.ToString() + ": " + items[k].
			ToString() + " ";
		return itemList;
	}
	static void Main()
	{
		Carpet c1 = new Carpet("Frieze", 1.75F, 12);
		Carpet c2 = new Carpet("Saxony", 1.82F, 9);
		Carpet c3 = new Carpet("Shag", 1.5F, 13);
		Carpet c4 = new Carpet("Loop", 1.77F, 10);
		ArrayList rugs = new ArrayList();
		rugs.Add(c1);
		rugs.Add(c2);
		rugs.Add(c3);
		rugs.Add(c4);
		rugs.Sort();
		Knapsack k = new Knapsack(25);
		k.FillSack(rugs);
		Console.WriteLine(k.GetItems());
	}
}
