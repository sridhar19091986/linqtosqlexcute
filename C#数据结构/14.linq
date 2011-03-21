<Query Kind="Expression">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

原书P250 代码1

while (h <= numElements / 3)
h = h * 3 + 1;


原书P250 代码2

h = (h - 1) / 3;


原书P250 代码3

public void ShellSort()
{
	int inner, temp;
	int h = 3;
	while (h > 0)
	{
		for (int outer = h; outer <= numElements - 1; outer++)
		{
			temp = arr[outer];
			inner = outer;
			while ((inner > h - 1) && arr[inner - h] >= temp)
			{
				arr[inner] = arr[inner - h];
				inner -= h;
			}
			arr[inner] = temp;
		}
		h = (h - 1) % 3;
	}
}


原书P251 代码

static void Main()
{
	const int SIZE = 19;
	CArray theArray = new CArray(SIZE);
	Random random = new Random();
	for (int index = 0; index < SIZE; index++)
		theArray.Insert(random.Next(100) + 1);
	Console.WriteLine();
	theArray.DisplayElements();
	Console.WriteLine();
	theArray.ShellSort();
	theArray.DisplayElements();
}


原书P252 代码

public void MergeSort()
{
	int[] tempArray = new int[numElements];
	RecMergeSort(tempArray, 0, numElements - 1);
}
public void RecMergeSort(int[] tempArray, int lbound, int ubound)
{
	if (lbound == ubound)
		return;
	else
	{
		int mid = (int)(lbound + ubound) / 2;
		RecMergeSort(tempArray, lbound, mid);
		RecMergeSort(tempArray, mid + 1, ubound);
		Merge(tempArray, lbound, mid + 1, ubound);
	}
}


原书P253 代码1

public void Merge(int[] tempArray, int lowp, int highp, int ubound)
{
	int lbound = lowp;
	int mid = highp - 1;
	int n = (ubound - lbound) + 1;
	int j = 0;
	while ((lowp <= mid) && (highp <= ubound))
	{
		if (arr[lowp] < arr[highp])
		{
			tempArray[j] = arr[lowp];
			j++;
			lowp++;
		}
		else
		{
			tempArray[j] = arr[highp];
			j++;
			highp++;
		}
}
	while (lowp <= mid)
	{
		tempArray[j] = arr[lowp];
		j++;
		lowp++;
	}
	while (highp <= ubound)
	{
		tempArray[j] = arr[highp];
		j++;
		highp++;
	}
	for (j = 0; j <= n - 1; j++)
		arr[lbound + j] = tempArray[j];
}


原书P253 代码2

this. DisplayElements();


原书P255 代码1

public class Node
{
	public int data;
	public Node(int key)
	{
		data = key;
	}
}


原书P255-P256 代码

public void ShiftUp(int index)
{
	int parent = (index - 1) / 2;
	Node bottom = heapArray[index];
	while ((index > 0) && (heapArray[parent].data < bottom.data))
	{
		heapArray[index] = heapArray[parent];
		index = parent;
		parent = (parent - 1) / 2;
	}
	heapArray[index] = bottom;
}


原书P256 代码2

public bool Insert(int key)
{
	if (currSize == maxSize)
		return false;
	heapArray[currSize] = new Node(key);
	currSize++;
	return true;
}


原书P257 代码1

public Node Remove()
{
	Node root = heapArray[0];
	currSize--;
	heapArray[0] = heapArray[currSize];
	ShiftDown(0);
	return root;
}
public void ShiftDown(int index)
{
	int largerChild;
	Node top = heapArray[index];
	while (index < (int)(currSize / 2))
	{
		int leftChild = 2 * index + 1;
		int rightChild = leftChild + 1;
		if ((rightChild < currSize) && heapArray[leftChild].data < heapArray[rightChild].data)
			largerChild = rightChild;
		else
			largerChild = leftChild;
		if (top.data >= heapArray[largerChild].data)
			break;
		heapArray[index] = heapArray[largerChild];
		index = largerChild;
	}
	heapArray[index] = top;
}


原书P257-P258 代码

using System;

public class Heap
{
	Node[] heapArray = null;
	private int maxSize = 0;
	private int currSize = 0;
	public Heap(int maxSize)
	{
		this.maxSize = maxSize;
		heapArray = new Node[maxSize];
	}
public bool InsertAt(int pos, Node nd)
{
		heapArray[pos] = nd;
		return true;
}
	public void ShowArray()
	{
		for (int i = 0; i < maxSize; i++)
		{
			if (heapArray[i] != null)
				System.Console.Write(heapArray[i].data + "  ");
		}
	}
	static void Main()
	{
		const int SIZE = 9;
		Heap aHeap = new Heap(SIZE);
		Random RandomClass = new Random();
		for (int i = 0; i < SIZE; i++)
		{

			int rn = RandomClass.Next(1, 100);
			aHeap.Insert(rn);
		}
		Console.Write("Random: ");
		aHeap.ShowArray();
		Console.WriteLine();
		Console.Write("Heap: ");
		for (int i = (int)SIZE / 2 - 1; i >= 0; i--)
			aHeap.ShiftDown(i);
		aHeap.ShowArray();
		for (int i = SIZE - 1; i >= 0; i--)
		{
			Node bigNode = aHeap.Remove();
			aHeap.InsertAt(i, bigNode);
		}
		Console.WriteLine();
		Console.Write("Sorted: ");
		aHeap.ShowArray();
	}
}


原书P259 代码

mv = arr[first];


原书P260-P262 代码

public void QSort()
{
	RecQSort(0, numElements - 1);
}
public void RecQSort(int first, int last)
{
	if ((last - first) <= 0)
		return;
	else
	{
		int part = this.Partition(first, last);
		RecQSort(first, part - 1);
		RecQSort(part + 1, last);
	}
}
public int Partition(int first, int last)
{
	int pivotVal = arr[first];
	int theFirst = first;
	bool okSide;
	first++;
	do
	{
		okSide = true;
		while (okSide)
			if (arr[first] > pivotVal)
				okSide = false;
			else
			{
				first++;
				okSide = (first <= last);
			}
		okSide = true;
		while (okSide)
			if (arr[last] <= pivotVal)
				okSide = false;
			else
			{
				last--;
				okSide = (first <= last);
			}
		if (first < last)
		{
			Swap(first, last);
			this.DisplayElements();
			first++;
			last--;
		}
	} while (first <= last);
	Swap(theFirst, last);
	this.DisplayElements();
	return last;
}
public void Swap(int item1, int item2)
{
	int temp = arr[item1];
	arr[item1] = arr[item2];
	arr[item2] = temp;
}


原书P262 代码2

theFirst = arr[(int)arr.GetUpperBound(0) / 2]
