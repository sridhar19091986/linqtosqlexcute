<Query Kind="Expression">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

原书P197 代码1

public class Node
{
	public Object Element;
	public Node Link;
	public Node()
	{
		Element = null;
		Link = null;
	}
	public Node(Object theElement)
	{
		Element = theElement;
		Link = null;
	}
}


原书P197-P198 代码

public class LinkedList
{
	protected Node header;
	public LinkedList()
	{
		header = new Node("header");
	}
…
}


原书P198 代码2

private Node Find(Object item)
{
	Node current = new Node();
	current = header;
	while (current. Element != item)
		current = current.Link;
	return current;
}


原书P198-P199 代码

public void Insert(Object newItem, Object after)
{
   Node current = new Node();
	Node newNode = new Node(newItem);
	current = Find(after);
	newNode.Link = current.Link;
	current.Link = newNode;
}


原书P199 代码2

private Node FindPrevious(Object n)
{
	Node current = header;
	while (!(current.Link == null) && (current.Link.Element != n))
	current = current.Link;
	return current;
}


原书P199 代码3

public void Remove(Object n)
{
Node p = FindPrevious(n);
	if (!(p.Link == null))
		p.Link = p.Link.Link;
}


原书P199 代码4

public void PrintList()
{
	Node current = new Node();
	current = header;
	while (!(current.Link == null))
	{
		Console.WriteLine(current.Link.Element);
		current = current.Link;
	}
}


原书P200-P201 代码

public class Node
{
	public Object Element;
	public Node Flink;
	public Node Blink;
	public Node()
	{
		Element = null;
		Flink = null;
		Blink = null;
	}
	public Node(Object theElement)
	{
		Element = theElement;
		Flink = null;
		Blink = null;
}
}


原书P201 代码2

public void Insert(Object newItem, Object after) 
{
Node current = new Node();
Node newNode = new Node(newItem);
current = Find(after);
newNode.Flink = current.Flink;
newNode.Blink = current;
current.Flink = newNode;
}


原书P202 代码1

public void Remove(Object n)
{
Node p = Find(n);
if (!(p.Flink == null))
{
	p.Blink.Flink = p.Flink;
	p.Flink.Blink = p.Blink;
	p.Flink = null;
	p.Blink = null;
	}
}


原书P202 代码2

private Node FindLast()
{
	Node current = new Node();
current = header;
while (!(current.Flink == null))
current = current.Flink;
return current;
}


原书P202 代码3

public void PrintReverse()
{
Node current = new Node();
current = FindLast();
while (!(current.Blink == null))
{
   Console.WriteLine(current.Element);
   current = current.Blink;
}
}


原书P203-P205 代码

public class Node
{
	public Object Element;
	public Node Link;
	public Node()
	{
		Element = null;
		Link = null;
	}
	public Node(Object theElement)
	{
		Element = theElement;
		Link = null;
	}
}
public class LinkedList
{
	protected Node current;
	protected Node header;
	private int count;
	public LinkedList()
	{
		count = 0;
		header = new Node("header");
		header.Link = header;
	}
	public bool IsEmpty()
	{
		return (header.Link == null);
	}
	public void MakeEmpty()
	{
		header.Link = null;
	}
	public void PrintList()
	{
		Node current = new Node();
		current = header;
		while (current.Link.Element.ToString() != "header")
		{
			Console.WriteLine(current.Link.Element);
			current = current.Link;
		}
	}
	private Node FindPrevious(Object n)
	{
		Node current = header;
		while (!(current.Link == null) && current.Link.
		Element != n)
			current = current.Link;
		return current;
	}
	private Node Find(Object n)
	{
		Node current = new Node();
		current = header.Link;
		while (current.Element != n)
			current = current.Link;
		return current;
	}
	public void Remove(Object n)
	{
		Node p = FindPrevious(n);
		if (!(p.Link == null))
			p.Link = p.Link.Link;
		count--;
	}
	public void Insert(Object n1, Object n2)
	{
		Node current = new Node();
		Node newnode = new Node(n1);
		current = Find(n2);
		newnode.Link = current.Link;
		current.Link = newnode;
		count++;
	}
	public void InsertFirst(Object n)
	{
		Node current = new Node(n);
		current.Link = header;
		header.Link = current;
		count++;
	}
	public Node Move(int n)
	{
		Node current = header.Link;
		Node temp;
		for (int i = 0; i <= n; i++)
			current = current.Link;
		if (current.Element.ToString() == "header")
			current = current.Link;
		temp = current;
		return temp;
}
public Node getFirst()
{
		return header;
}
}


原书P206 代码1

public class ListIter
{
	private Node current;
	private Node previous;
	LinkedList theList;
	public ListIter(LinkedList list)
	{
		theList = list;
		current = theList.getFirst();
		previous = null;
}
}


原书P206 代码2

public void NextLink()
{
	previous = current;
	current = current.Link;
}


原书P207 代码1

public Node GetCurrent()
{
return current;
}


原书P207 代码2

public void InsertBefore(Object theElement)
{
Node newNode = new Node(theElement);
if (current == header)
throw new InsertBeforeHeaderException();
else
{
newNode.Link = previous.Link;
previous.Link = newNode;
current = newNode;
}
}


原书P207 代码3

class InsertBeforeHeaderException : Exception
{
	public InsertBeforeHeaderException()
		: base("Can't insert before the header node.") 
	{
	}
}


原书P208 代码1

public void InsertAfter(Object theElement)
{
	Node newnode = new Node(theElement);
	newnode.Link = current.Link;
	current.Link = newnode;
	NextLink();
}


原书P208 代码2

public void Remove()
{
	previous.Link = current.Link;
}


原书P208 代码3

public void Reset()
{
	current = theList.getFirst();
	previous = null;
}
public bool AtEnd()
{
	return (current.Link == null);
}


原书P208-P209 代码

public class LinkedList
{
	private Node header;
	public LinkedList()
	{
		header = new Node("header");
	}
	public bool IsEmpty()
	{
		return (header.Link == null);
	}
	public Node GetFirst()
	{
		return header;
	}
	public void ShowList()
	{
		Node current = header.Link;
		while (!(current == null))
		{
			Console.WriteLine(current.Element);
			current = current.Link;
		}
	}
}

原书P209-P214 代码

using System;
public class Node
{
	public Object Element;
	public Node Link;
	public Node()
	{
		Element = null;
		Link = null;
	}
	public Node(Object theElement)
	{
		Element = theElement;
		Link = null;
	}
}
public class InsertBeforeHeaderException : System.ApplicationException
{
	public InsertBeforeHeaderException(string message) :base(message)
	{
	}
}
public class LinkedList 
{
	private Node header;
	public LinkedList() 
	{
		header = new Node("header");
	}
	public bool IsEmpty() 
	{
		return (header.Link == null);
	}
	public Node GetFirst() 
	{
		return header;
	}
	public void ShowList() 
	{
		Node current = header.Link;
		while (!(current == null)) 
		{
			Console.WriteLine(current.Element);
			current = current.Link;
		}
	}
}
public class ListIter
{
	private Node current;
	private Node previous;
	LinkedList theList;
	public ListIter(LinkedList list)
	{
		theList = list;
		current = theList.GetFirst();
		previous = null;
	}
	public void NextLink()
	{
		previous = current;
		current = current.Link;
	}
	public Node GetCurrent()
	{
		return current;
	}
	public void InsertBefore(Object theElement)
	{
		Node newNode = new Node(theElement);
		if (previous.Link == null)
			throw new InsertBeforeHeaderException("Can't insert here.");
		else
		{
			newNode.Link = previous.Link;
			previous.Link = newNode;
			current = newNode;
		}
	}
	public void InsertAfter(Object theElement)
	{
		Node newNode = new Node(theElement);
		newNode.Link = current.Link;
		current.Link = newNode;
		NextLink();
	}
	public void Remove()
	{
		previous.Link = current.Link;
	}
	public void Reset()
	{
		current = theList.GetFirst();
		previous = null;
	}
	public bool AtEnd()
	{
		return (current.Link == null);
	}
}
class chapter11
{
	static void Main()
	{
		LinkedList MyList = new LinkedList();
		ListIter iter = new ListIter(MyList);
		string choice, value;
		try
		{
			iter.InsertAfter("David");
			iter.InsertAfter("Mike");
			iter.InsertAfter("Raymond");
			iter.InsertAfter("Bernica");
			iter.InsertAfter("Jennifer");
			iter.InsertBefore("Donnie");
			iter.InsertAfter("Michael");
			iter.InsertBefore("Terrill");
			iter.InsertBefore("Mayo");
			iter.InsertBefore("Clayton");
			while (true)
			{
				Console.WriteLine("(n) Move to next node");
				Console.WriteLine("(g)Get value in current node");
				Console.WriteLine("(r) Reset iterator");
				Console.WriteLine("(s) Show complete list");
				Console.WriteLine("(a) Insert after");
				Console.WriteLine("(b) Insert before");
				Console.WriteLine("(c) Clear the screen");
				Console.WriteLine("(x) Exit");
				Console.WriteLine();
				Console.Write("Enter your choice: ");
				choice = Console.ReadLine();
				choice = choice.ToLower();
				char[] onechar = choice.ToCharArray();
				switch (onechar[0])
				{
					case 'n':
						if (!(MyList.IsEmpty()) && (!(iter.AtEnd())))
							iter.NextLink();
						else
							Console.WriteLine("Can' move to next link.");
						break;
					case 'g':
						if (!(MyList.IsEmpty()))
							Console.WriteLine("Element: " + iter.GetCurrent().Element);
						else
							Console.WriteLine("List is empty.");
						break;
					case 'r':
						iter.Reset();
						break;
					case 's':
						if (!(MyList.IsEmpty()))
							MyList.ShowList();
						else
							Console.WriteLine("List is empty.");
						break;
					case 'a':
						Console.WriteLine();
						Console.Write("Enter value to insert:");
						value = Console.ReadLine();
						iter.InsertAfter(value);
						break;
					case 'b':
						Console.WriteLine();
						Console.Write("Enter value to insert:");
						value = Console.ReadLine();
						iter.InsertBefore(value);
						break;
					case 'c':
						// clear the screen
						break;
					case 'x':
						// end of program
						return;
				}
			}
		}
		catch (InsertBeforeHeaderException e)
		{
			Console.WriteLine(e.Message);
		}
	}
}


原书P214-P215 代码

LinkedListNode<string> node1 = new LinkedListNode<string>("Raymond");
LinkedList<string> names = new LinkedList<string>();


原书P215 代码2

using System;
using System.Collections.Generic;
using System.Text;
class Program
{
	static void Main(string[] args)
	{
		LinkedListNode<string> node = new
		LinkedListNode<string>("Mike");
		LinkedList<string> names = new LinkedList<string>();
		names.AddFirst(node);
		LinkedListNode<string> node1 = new LinkedListNode<string>("David");
		names.AddAfter(node, node1);
		LinkedListNode<string> node2 = new LinkedListNode<string>("Raymond");
		names.AddAfter(node1, node2);
		LinkedListNode<string> node3 = new LinkedListNode<string>(null);
		LinkedListNode<string> aNode = names.First;
		while (aNode != null)
		{
			Console.WriteLine(aNode.Value);
			aNode = aNode.Next;
		}
		aNode = names.Find("David");
		if (aNode != null) aNode = names.First;
		while (aNode != null)
		{
			Console.WriteLine(aNode.Value);
			aNode = aNode.Next;
		}
		Console.Read();
	}
}


原书P216 代码

while (aNode != names.Last)
{
Console.WriteLine(aNode.Value);
aNode = aNode.Next;
}
