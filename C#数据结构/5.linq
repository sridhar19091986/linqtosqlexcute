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

原书P70-P71代码

class CStack
{
	private int p_index;
	private ArrayList list;
	public CStack()
	{
		list = new ArrayList();
		p_index = -1;
	}
	public int count
	{
		get
		{
			return list.Count;
		}
	}
	public void push(object item)
	{
		list.Add(item);
		p_index++;
	}
	public object pop()
	{
		object obj = list[p_index];
		list.RemoveAt(p_index);
		p_index--;
		return obj;
	}
	public void clear()
	{
		list.Clear();
		p_index = -1;
	}
	public object peek()
	{
		return list[p_index];
	}
}


原书P72

static void Main(string[] args)
{
	CStack alist = new CStack();
	string ch;
	string word = "sees";
	bool isPalindrome = true;
	for (int x = 0; x < word.Length; x++)
		alist.push(word.Substring(x, 1));
	int pos = 0;
	while (alist.count > 0)
	{
		ch = alist.pop().ToString();
		if (ch != word.Substring(pos, 1))
		{
		isPalindrome = false;
		break;
		}
		pos++;
	}
	if (isPalindrome)
		Console.WriteLine(word + " is a palindrome.");
	else
		Console.WriteLine(word + " is not a palindrome.");
	Console.Read();
}


原书P73-1代码

Stack myStack = new Stack();


原书P73-2代码

Stack<string> myStack = new Stack<string>();


原书P73-3代码

string[] names = new string[] { "Raymond", "David", "Mike" };
Stack nameStack = new Stack(names);


原书P73-4代码

Stack myStack = new Stack(25);


原书P74-P76代码

using System;
using System.Collections;
using System.Text.RegularExpressions;
namespace csstack
{
	class Class1
	{
		static void Main(string[] args)
		{
			Stack nums = new Stack();
			Stack ops = new Stack();
			string expression = "5 + 10 + 15 + 20";
			Calculate(nums, ops, expression);
			Console.WriteLine(nums.Pop());
			Console.Read();
		}
		// IsNumeric isn't built into C# so we must define it
		static bool IsNumeric(string input)
		{
			bool flag = true;
			string pattern = (@"^\d+$");
			Regex validate = new Regex(pattern);
			if (!validate.IsMatch(input))
			{
				flag = false;
			}
			return flag;
		}
		static void Calculate(Stack N, Stack O, string exp)
		{
			string ch, token = "";
			for (int p = 0; p < exp.Length; p++)
			{
				ch = exp.Substring(p, 1);
				if (IsNumeric(ch))
					token += ch; //+=
				if (ch == " " || p == (exp.Length - 1))
				{
					if (IsNumeric(token))
					{
						N.Push(token);
						token = "";
					}
				}
				else if (ch == "+" || ch == "-" || ch == "*" || ch == "/")
					O.Push(ch);
				if (N.Count == 2)
					Compute(N, O);
			}
		}
		static void Compute(Stack N, Stack O)
		{
			int oper1, oper2;
			string oper;
			oper1 = Convert.ToInt32(N.Pop());
			oper2 = Convert.ToInt32(N.Pop());
			oper = Convert.ToString(O.Pop());
			switch (oper)
			{
				case "+":
					N.Push(oper1 + oper2);
					break;
				case "-":
					N.Push(oper1 - oper2);
					break;
				case "*":
					N.Push(oper1 * oper2);
					break;
				case "/":
					N.Push(oper1 / oper2);
					break;
			}
		}
	}
}


原书P76-2 代码

if (IsNumeric(Nums.Peek()))
num = Nums.Pop();


原书P77-1 代码

if (oper2 == 0)
Nums.Clear();


原书P77-2 代码

if (myStack.Contains(" "))
StopProcessing();
else
ContinueProcessing();


原书P77-3 代码

Stack myStack = new Stack();
for (int i = 20; i > 0; i--)
myStack.Push(i);
object[] myArray = new object[myStack.Count];
myStack.CopyTo(myArray, 0);


原书P78-1 代码

Stack myStack = new Stack();
for (int i = 0; i > 0; i++)
myStack.Push(i);
object[] myArray = new object[myStack.Count];
myArray = myStack.ToArray();


原书P78-P79 代码

using System;
using System.Collections;
namespace csstack
{
	class Class1
	{
		static void Main(string[] args)
		{
			int num, baseNum;
			Console.Write("Enter a decimal number: ");
			num = Convert.ToInt32(Console.ReadLine());
			Console.Write("Enter a base: ");
			baseNum = Convert.ToInt32(Console.ReadLine());
			Console.Write(num + " converts to ");
			MulBase(num, baseNum);
			Console.WriteLine(" Base " + baseNum);
			Console.Read();
		}
		static void MulBase(int n, int b)
		{
			Stack Digits = new Stack();
			do
			{
				Digits.Push(n % b);
				n /= b;
			} while (n != 0);
			while (Digits.Count > 0)
				Console.Write(Digits.Pop());
		}
	}
}


原书P81-P82 代码

public class CQueue
{
	private ArrayList pqueue;
	public CQueue()
	{
		pqueue = new ArrayList();
	}
	public void EnQueue(object item)
	{
		pqueue.Add(item);
	}
	public void DeQueue()
	{
		pqueue.RemoveAt(0);
	}
	public object Peek()
	{
		return pqueue[0];
	}
	public void ClearQueue()
	{
		pqueue.Clear();
}
	public int Count()
	{
		return pqueue.Count;
}
}


原书P82-2 代码

Queue myQueue = new Queue(100);


原书P82-3 代码

Queue myQueue = new Queue(32, 3);


原书P82-4 代码

Queue<int> numbers = new Queue<int>();


原书P83-P86 代码

using System;
using System.Collections;
using System.IO;

namespace csqueue
{
	public struct Dancer
	{
		public string name;
		public string sex;
		public void GetName(string n)
		{
			name = n;
		}
		public override string ToString()
		{
			return name;
		}
	}
	class Class1
	{
		static void newDancers(Queue male, Queue female)
		{
			Dancer m, w;
			m = new Dancer();
			w = new Dancer();
			if (male.Count > 0 && female.Count > 0)
			{
				m.GetName(male.Dequeue().ToString());
				w.GetName(female.Dequeue().ToString());
			}
			else if ((male.Count > 0) && (female.Count == 0))
				Console.WriteLine("Waiting on a female dancer.");
			else if ((female.Count > 0) && (male.Count == 0))
				Console.WriteLine("Waiting on a male dancer.");
		}
		static void headOfLine(Queue male, Queue female)
		{
			Dancer w, m;
			m = new Dancer();
			w = new Dancer();
			if (male.Count > 0)
				m.GetName(male.Peek().ToString());
			if (female.Count > 0)
				w.GetName(female.Peek().ToString());
			if (m.name != " " && w.name != "") 
				Console.WriteLine("Next in line are: " + m.name + "\t" + w.name);
			else
				if (m.name != "") //!=
					Console.WriteLine("Next in line is: " + m.name);
				else
					Console.WriteLine("Next in line is: " + w.name);
		}
		static void startDancing(Queue male, Queue female)
		{
			Dancer m, w;
			m = new Dancer();
			w = new Dancer();
			Console.WriteLine("Dance partners are: ");
			Console.WriteLine();
			for (int count = 0; count <= 3; count++)
			{
				m.GetName(male.Dequeue().ToString());
				w.GetName(female.Dequeue().ToString());
				Console.WriteLine(w.name + "\t" + m.name);
			}
		}
		static void formLines(Queue male, Queue female)
		{
			Dancer d = new Dancer();
			StreamReader inFile;
			inFile = File.OpenText(@"c:\dancers.dat");
			string line;
			while (inFile.Peek() != -1) 
			{
				line = inFile.ReadLine();
				d.sex = line.Substring(0, 1);
				d.name = line.Substring(2, line.Length - 2);
				if (d.sex == "M")
					male.Enqueue(d);
				else
					female.Enqueue(d);
			}
		}
		static void Main(string[] args)
		{
			Queue males = new Queue();
			Queue females = new Queue();
			formLines(males, females);
			startDancing(males, females);
			if (males.Count > 0 || females.Count > 0)
				headOfLine(males, females);
			newDancers(males, females);
			if (males.Count > 0 || females.Count > 0)
				headOfLine(males, females);
			newDancers(males, females);
			Console.Write("press enter");
			Console.Read();
		}
	}
}


原书P88-P90 代码

using System;
using System.Collections;
using System.IO;
namespace csqueue
{
	class Class1
	{
		enum DigitType { ones = 1, tens = 10 }
		static void DisplayArray(int[] n)
		{
			for (int x = 0; x <= n.GetUpperBound(0); x++)
				Console.Write(n[x] + " ");
		}
		static void RSort(Queue[] que, int[] n, DigitType digit)
		{
			int snum;
			for (int x = 0; x <= n.GetUpperBound(0); x++)
			{
				if (digit == DigitType.ones)
					snum = n[x] % 10;
				else
					snum = n[x] / 10;
				que[snum].Enqueue(n[x]);
			}
		}
		static void BuildArray(Queue[] que, int[] n)
		{
			int y = 0;
			for (int x = 0; x >= 9; x++)
				while (que[x].Count > 0)
				{
					n[y] =
					Int32.Parse(que[x].Dequeue().ToString());
					y++;
				}
		}
		static void Main(string[] args)
		{
			Queue[] numQueue = new Queue[10];
			int[] nums = new int[] { 91, 46, 85, 15, 92, 35, 31, 22 };
			int[] random = new Int32[99];
			// Display original list
			for (int i = 0; i < 10; i++)
				numQueue[i] = new Queue();
			RSort(numQueue, nums, DigitType.ones);
			//numQueue, nums, 1
			BuildArray(numQueue, nums);
			Console.WriteLine();
			Console.WriteLine("First pass results: ");
			DisplayArray(nums);
			// Second pass sort
			RSort(numQueue, nums, DigitType.tens);
			BuildArray(numQueue, nums);
			Console.WriteLine();
			Console.WriteLine("Second pass results: ");
			// Display final results
			DisplayArray(nums);
			Console.WriteLine();
			Console.Write("Press enter to quit");
			Console.Read();
		}
	}
}


原书P90-2 代码

struct Process
{
	int priority;
	string name;
}


原书P91 代码

public struct pqItem 
{
	public int priority;
	public string name;
}
public class PQueue : Queue 
{
	public PQueue()
	{
	}
	public override object Dequeue() 
	{
		object [] items;
		int min;
		items = this.ToArray();
		min = ((pqItem)items[0]).priority;
		for(int x = 1; x <= items.GetUpperBound(0); x++)
			if (((pqItem)items[x]).priority < min) 
			{
				min = ((pqItem)items[x]).priority;
			}
		this.Clear();
		int x2 ;
		for(x2 = 0; x2 <= items.GetUpperBound(0); x2++)
			if (((pqItem)items[x2]).priority == min && ((pqItem)items[x2]).name != "")
				this.Enqueue(items[x2]);
		return base.Dequeue();
	}
}


原书P92 代码

static void Main()
{
	PQueue erwait = new PQueue();
	pqItem[] erPatient = new pqItem[3];
	pqItem nextPatient;
	erPatient[0].name = "Joe Smith";
	erPatient[0].priority = 1;
	erPatient[1].name = "Mary Brown";
	erPatient[1].priority = 0;
	erPatient[2].name = "Sam Jones";
	erPatient[2].priority = 3;
	for (int x = 0; x <= erPatient.GetUpperBound(0); x++)
		erwait.Enqueue(erPatient[x]);
	nextPatient = (pqItem)erwait.Dequeue();
	Console.WriteLine(nextPatient.name);
}
