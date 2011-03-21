<Query Kind="Expression">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

原书P264-P266 代码

public class Node : IComparable
{
	public Object element;
	public Node left;
	public Node right;
	public int height;
	public Node(Object data, Node lt, Node rt)
	{
		element = data;
		left = lt;
		right = rt;
		height = 0;
	}
	public Node(Object data)
	{
		element = data;
		left = null;
		right = null;
	}
	public int CompareTo(Object obj)
{
return (((int)element).CompareTo((int)obj));
	}
	public int GetHeight()
	{
		if (this == null)
			return -1;
		else
			return this.height;
}
}


原书P266-P267 代码

private Node Insert(Object item, Node n)
{
if (n == null)
		n = new Node(item, null, null);
	else if (((int)item).CompareTo((int)n.element) < 0)
	{
		n.left = Insert(item, n.left);
		if (n.left.GetHeight() - n.right.GetHeight() == 2)
			n = n.RotateWithLeftChild(n);
		else
			n = n.DoubleWithLeftChild(n);
	}
	else if (((int)item).CompareTo((int)n.element) > 0)
	{
		n.right = Insert(item, n.right);
		if (n.right.GetHeight() - n.left.GetHeight() == 2)
			if (((int)item).CompareTo((int)n.right.element) > 0)
				n = RotateWithRightChild(n);
			else
				n = DoubleWithRightChild(n);
	}
	else
	{ ;}// do nothing, duplicate value
	n.height = Math.Max(n.left.GetHeight(), n.right.GetHeight()) + 1;
	return n;
}


原书P267-P268 代码

private Node RotateWithLeftChild(Node n2)
{
	Node n1 = n2.left;
	n2.left = n1.right;
	n1.right = n2;
	n2.height = Math.Max(n2.left.GetHeight(), n2.right.GetHeight()) + 1;
	n1.height = Math.Max(n1.left.GetHeight(), n2.height) + 1;
	return n1;
}
private Node RotateWithRightChild(Node n1)
{
	Node n2 = n1.right;
	n1.right = n2.left;
	n2.left = n1;
	n1.height = Math.Max(n1.left.GetHeight(), n1.right.GetHeight() + 1);
	n2.height = Math.Max(n2.right.GetHeight(), n1.height) + 1;
	return n2;
}
private Node DoubleWithLeftChild(Node n3)
{
	n3.left = RotateWithRightChild(n3.left);
	return RotateWithLeftChild(n3);
}
private Node DoubleWithRightChild(Node n1)
{
	n1.right = RotateWithLeftChild(n1.right);
	return RotateWithRightChild(n1);
}


原书P270-P275 代码

using System;
public class Node
{
	public string element;
	public Node left;
	public Node right;
	public int color;
	const int RED = 0;
	const int BLACK = 1;
	public Node(string element, Node left, Node right)
	{
		this.element = element;
		this.left = left;
		this.right = right;
		this.color = BLACK;
	}
	public Node(string element)
	{
		this.element = element;
		this.color = BLACK;
	}
}
public class RBTree
{
	const int RED = 0;
	const int BLACK = 1;
	private Node current;
	private Node parent;
	private Node grandParent;
	private Node greatParent;
	private Node header;
	private Node nullNode;
	public RBTree(string element)
	{
		current = new Node("");
		parent = new Node("");
		grandParent = new Node("");
		greatParent = new Node("");
		nullNode = new Node("");
		nullNode.left = nullNode;
		nullNode.right = nullNode;
		header = new Node(element);
		header.left = nullNode;
		header.right = nullNode;
	}
	public void Insert(string item)
	{
		grandParent = header;
		parent = grandParent;
		current = parent;
		nullNode.element = item;
		while (current.element.CompareTo(item) != 0)
		{
			Node greatParent = grandParent;
			grandParent = parent;
			parent = current;
			if (item.CompareTo(current.element) < 0)
				current = current.left;
			else
				current = current.right;
			if ((current.left.color) == RED && current.right.color == RED)
				HandleReorient(item);
		}
		if (!(current == nullNode))
			//return
			current = new Node(item, nullNode, nullNode);
		if (item.CompareTo(parent.element) < 0)
			parent.left = current;
		else
			parent.right = current;
		HandleReorient(item);
	}
	public string FindMin()
	{
		if (this.IsEmpty())
			return null;
		Node itrNode = header.right;
		while (!(itrNode.left == nullNode))
			itrNode = itrNode.left;
		return itrNode.element;
	}
	public string FindMax()
	{
		if (this.IsEmpty())
			return null;
		Node itrNode = header.right;
		while (!(itrNode.right == nullNode))
			itrNode = itrNode.right;
		return itrNode.element;
	}
	public string Find(string e)
	{
		nullNode.element = e;
		Node current = header.right;
		while (true)
			if (e.CompareTo(current.element) < 0)
				current = current.left;
			else if (e.CompareTo(current.element) > 0)
				current = current.right;
			else if (!(current == nullNode))
				return current.element;
			else
				return null;
	}
	public void MakeEmpty()
	{
		header.right = nullNode;
	}
	public bool IsEmpty()
	{
		return (header.right == nullNode);
	}
	public void PrintRBTree()
	{
		if (this.IsEmpty())
			Console.WriteLine("Empty");
		else
			PrintRB(header.right);
	}
	public void PrintRB(Node n)
	{
		if (!(n == nullNode))
		{
			PrintRB(n.left);
			Console.WriteLine(n.element);
			PrintRB(n.right);
		}
	}
	public void HandleReorient(string item)
	{
		current.color = RED;
		current.left.color = BLACK;
		current.right.color = BLACK;
		if (parent.color == RED)
		{
			grandParent.color = RED;
			if ((item.CompareTo(grandParent.element) < 0) != (item.CompareTo(parent.element) < 0))
			{
				current = Rotate(item, grandParent);
				current.color = BLACK;
			}
			header.right.color = BLACK;
		}
	}
	public Node Rotate(string item, Node parent)
	{
		if (item.CompareTo(parent.element) < 0)
		{
			if (item.CompareTo(parent.left.element) < 0)
				parent.left = RotateWithLeftChild(parent.left);
			else
				parent.left = RotateWithRightChild(parent.left);
			return parent.left;
		}
		else
		{
			if (item.CompareTo(parent.right.element) < 0)
				parent.right = RotateWithLeftChild(parent.right);
			else
				parent.right = RotateWithRightChild(parent.right);
			return parent.right;
		}
	}
	public Node RotateWithLeftChild(Node k2)
	{
		Node k1 = k2.left;
		k2.left = k1.right;
		k1.right = k2;
		return k1;
	}
	public Node RotateWithRightChild(Node k1)
	{
		Node k2 = k1.right;
		k1.right = k2.left;
		k2.left = k1;
		return k2;
	}
}


原书P277 代码1

(int)(Math.Ceiling(Math.Log(maxNodes) / Math.Log(1/PROB)) - 1);


原书P277-P278 代码

public class SkipNode
{
	public int key;
	public Object value;
	public SkipNode[] link;
	public SkipNode(int level, int key, Object value)
	{
		this.key = key;
		this.value = value;
		link = new SkipNode[level];
	}
}


原书P278	代码2

public class SkipList
{
private int maxLevel;
private int level;
private SkipNode header;
private float probability;
private const int NIL = Int32.MaxValue;
private const int PROB = 0.5F;
}


原书P278-P279	代码

private void SkipList2(float probable, int maxLevel)
{
this.probability = probable;
	this.maxLevel = maxLevel;
	level = 0;
	header = new SkipNode(maxLevel, 0, null);
	SkipNode nilElement = new SkipNode(maxLevel, NIL, null);
	for (int i = 0; i <= maxLevel - 1; i++)
		header.link[i] = nilElement;
}
public SkipList(long maxNodes)
{
	this.SkipList2(PROB, (int)(Math.Ceiling(Math.Log(maxNodes) / Math.Log(1 / PROB) - 1)));
}


原书P279-P280	代码

public void Insert(int key, Object value)
{
	SkipNode[] update = new SkipNode[maxLevel];
	SkipNode cursor = header;
	for (int i = level; i >= level; i--)
	{
		while (cursor.link[i].key < key)
			cursor = cursor.link[i];
		update[i] = cursor;
	}
	cursor = cursor.link[0];
	if (cursor.key == key)
		cursor.value = value;
	else
	{
		int newLevel = GenRandomLevel();
		if (newLevel > level)
		{
			for (int i = level + 1; i <= newLevel - 1; i++)
			   update[i] = header;
			level = newLevel;
		}
		cursor = new SkipNode(newLevel, key, value);
		for (int i = 0; i <= newLevel - 1; i++)
		{
			cursor.link[i] = update[i].link[i];
			update[i].link[i] = cursor;
		}
	}
}


原书P280	代码2

private int GenRandomLevel()
{
	int newLevel = 0;
	Random r = new Random();
int ran = r.Next(0);

	while ((newLevel < maxLevel) && (ran < probability))
		newLevel++;
	return newLevel;
}


原书P280-P281	代码

public void Delete(int key)
{
	SkipNode[] update = new SkipNode[maxLevel + 1];
  SkipNode cursor = header;
	for (int i = level; i >= level; i--)
	{
		while (cursor.link[i].key < key)
			cursor = cursor.link[i];
		update[i] = cursor;
	}
	cursor = cursor.link[0];
	if (cursor.key == key)
	{
		for (int i = 0; i < level - 1; i++)
		if (update[i].link[i] == cursor)
				update[i].link[i] = cursor.link[i];
		while ((level > 0) && (header.link[level].key == NIL))
			level--;
	}
}


原书P281	代码2

public Object Search(int key)
{
	SkipNode cursor = header;
	for (int i = level; i > 0; i--) 
	{
	   SkipNode nextElement = cursor.link[i];
		while (nextElement.key < key)
		{
		   cursor = nextElement;
			nextElement = cursor.link[i];
		}
	}
	cursor = cursor.link[0];
	if (cursor.key == key)
		return cursor.value;
	else
		return "Object not found";
}
