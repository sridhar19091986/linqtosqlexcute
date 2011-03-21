<Query Kind="Expression">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

原书P221 代码

public class Node
{
	public int Data;
	public Node left;
	public Node right;
	public void DisplayNode()
	{
		Console.Write(Data);
	}
}


原书P222-P223 代码

public class Node
{
	public int Data;
	public Node Left;
	public Node Right;
	public void DisplayNode()
	{
		Console.Write(Data + " ");
	}
}
public class BinarySearchTree
{
	public Node root;
	public BinarySearchTree()
	{
		root = null;
	}
	public void Insert(int i)
	{
		Node newNode = new Node();
		newNode.Data = i;
		if (root == null)
			root = newNode;
		else
		{
			Node current = root;
			Node parent;
			while (true)
			{
				parent = current;
				if (i < current.Data)
				{
					current = current.Left;
					if (current == null)
					{
						parent.Left = newNode;
						break;
					}
				}
				else
				{
					current = current.Right;
					if (current == null)
					{
						parent.Right = newNode;
						break;
					}
				}
			}
		}
}
}


原书P224 代码

public void InOrder(Node theRoot)
{
	if (!(theRoot == null))
	{
		InOrder(theRoot.Left);
		theRoot.DisplayNode();
		InOrder(theRoot.Right);
	}
}


原书P225 代码

static void Main()
{
	BinarySearchTree nums = new BinarySearchTree();
	nums.Insert(23);
	nums.Insert(45);
	nums.Insert(16);
	nums.Insert(37);
	nums.Insert(3);
	nums.Insert(99);
	nums.Insert(22);
	Console.WriteLine("Inorder traversal: ");
	nums.InOrder(nums.root);
}


原书P226 代码1

public void PreOrder(Node theRoot) 
{
if (!(theRoot == null)) 
{
theRoot.DisplayNode();
PreOrder(theRoot.Left);
PreOrder(theRoot.Right);
}
}


原书P226 代码2

public void PostOrder(Node theRoot)
{
	if (!(theRoot == null))
	{
		PostOrder(theRoot.Left);
		PostOrder(theRoot.Right);
		theRoot.DisplayNode();
	}
}


原书P227 代码1

public int FindMin()
{
	Node current = root;
	while (!(current.Left == null))
	current = current.Left;
return current.Data;
}


原书P227 代码2

public int FindMax()
{
	Node current = root;
	while (!(current.Right == null))
		current = current.Right;
	return current.Data;
}


原书P228 代码

public Node Find(int key)
{
	Node current = root;
	while (current.Data != key)
	{
		if (key < current.Data)
			current = current.Left;
		else
			current = current.Right;
		if (current == null)
		return null;
	}
	return current;
}


原书P229 代码

public bool Delete(int key)
{
	Node current = root;
	Node parent = root;
	bool isLeftChild = true;
	while (current.Data != key)
	{
		parent = current;
		if (key < current.Data)
		{
			isLeftChild = true;
			current = current.Right;
		}
		else
		{
			isLeftChild = false;
			current = current.Right;
		}
		if (current == null)
			return false;
	}
	if ((current.Left == null) & (current.Right == null))
		if (current == root)
			root = null;
		else if (isLeftChild)
			parent.Left = null;
		else
			parent.Right = null;
	return true;
}


原书P230 代码

else if (current.Right == null)
if (current == root)
root = current.Left;
else if (isLeftChild)
parent.Left = current.Left;
else
parent.Right = current.Right;
else if (current.Left == null)
if (current == root)
root = current.Right;
else if (isLeftChild)
parent.Left = parent.Right;
else
parent.Right = current.Right;


原书P232 代码1

public Node GetSuccessor(Node delNode)
{
Node successorParent = delNode;
Node successor = delNode;
Node current = delNode.Right;
while (!(current == null))
{
successorParent = current;
successor = current;
current = current.Left;
}
if (!(successor == delNode.Right))
{
successorParent.Left = successor.Right;
successor.Right = delNode.Right;
}
return successor;
}


原书P232 代码2

else
{
Node successor = GetSuccessor(current);
if (current == root)
root = successor;
else if (isLeftChild)
parent.Left = successor;
else
parent.Right = successor;
successor.Left = current.Left;
}


原书P233 代码1

if (!(successor == delNode.Right))
{
successorParent.Left = successor.Right;
successor.Right = delNode.Right;
}


原书P233 代码2

if (current == root)
root = successor;
else if (isLeftChild)
parent.Left = successor;
else
parent.Right = successor;
successor.Left = current.Left;


原书P233-P235 代码

public bool Delete(int key)
{
Node current = root;
Node parent = root;
bool isLeftChild = true;
while (current.Data != key)
{
parent = current;
	 if (key < current.Data)
	 {
		 isLeftChild = true;
		 current = current.Right;
	 }
	 else
	 {
		 isLeftChild = false;
		 current = current.Right;
	 }
	 if (current == null)
		 return false;
}
if ((current.Left == null) & (current.Right == null))
if (current == root)
		 root = null;
	 else if (isLeftChild)
		 parent.Left = null;
	 else if (current.Right == null)
		 if (current == root)
			 root = current.Left;
		 else if (isLeftChild)
			 parent.Left = current.Left;
		 else
			 parent.Right = current.Right;
	 else if (current.Left == null)
		 if (current == root)
			 root = current.Right;
		 else if (isLeftChild)
			 parent.Left = parent.Right;
		 else
			 parent.Right = current.Right;
		 else
		 {
			 Node successor = GetSuccessor(current);
			 if (current == root)
				 root = successor;
			 else if (isLeftChild)
				 parent.Left = successor;
			 else
				 parent.Right = successor;
			 successor.Left = current.Left;
}
return true;
}
