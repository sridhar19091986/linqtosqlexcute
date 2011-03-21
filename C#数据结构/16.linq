<Query Kind="Expression">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

原书P285-P286 代码

public class Vertex
{
	public bool wasVisited;
	public string label;
	public Vertex(string label)
	{
		this.label = label;
		wasVisited = false;
	}
}


原书P287 代码1

int nVertices = 0;
vertices[nVertices] = new Vertex("A");
nVertices++;
vertices[nVertices] = new Vertex("B");
nVertices++;
vertices[nVertices] = new Vertex("C");
nVertices++;
vertices[nVertices] = new Vertex("D");


原书P287 代码2

adjMatrix[0,1] = 1;
adjMatrix[1,0] = 1;
adjMatrix[1,3] = 1;
adjMatrix[3,1] = 1;


原书P287-P288 代码

public class Vertex
{
	public bool wasVisited;
	public string label;
	public Vertex(string label)
	{
		this.label = label;
		wasVisited = false;
	}
}
public class Graph
{
	private int NUM_VERTICES = 6;
	private Vertex[] vertices;
	private int[,] adjMatrix;
	int numVerts;
	public Graph(int numvertices)
	{
		NUM_VERTICES = numvertices;
		vertices = new Vertex[NUM_VERTICES];
		adjMatrix = new int[NUM_VERTICES, NUM_VERTICES];
		numVerts = 0;
		for (int j = 0; j <= NUM_VERTICES -1; j++)
			for (int k = 0; k <= NUM_VERTICES - 1; k++)
				adjMatrix[j, k] = 0;
	}
	public void AddVertex(string label)
	{
		vertices[numVerts] = new Vertex(label);
		numVerts++;
	}
	public void AddEdge(int start, int eend)
	{
		adjMatrix[start, eend] = 1;
	}
	public void ShowVertex(int v)
	{
		Console.Write(vertices[v].label + " ");
}
}


原书P290 代码

public int NoSuccessors()
{
	bool isEdge;
	for (int row = 0; row <= NUM_VERTICES - 1; row++)
	{
		isEdge = false;
		for (int col = 0; col <= NUM_VERTICES - 1; col++)
		{
			if (adjMatrix[row, col] > 0)
			{
				isEdge = true;
				break;
			}
		}
		if (!isEdge)
			return row;
	}
	return -1;
}


原书P291 代码1

	public void DelVertex(int vert)
	{
		if (vert != NUM_VERTICES - 1)
		{
			for (int j = vert; j < NUM_VERTICES - 1; j++)
				vertices[j] = vertices[j + 1];
			for (int row = vert; row < NUM_VERTICES - 1; row++)
				MoveRow(row, NUM_VERTICES);
			for (int col = vert; col < NUM_VERTICES - 1; col++)
				MoveCol(col, NUM_VERTICES);
		}
		NUM_VERTICES--;
	}
	private void MoveRow(int row, int length)
	{
		for (int col = 0; col <= length - 1; col++)
			adjMatrix[row, col] = adjMatrix[row + 1, col];
	}
	private void MoveCol(int col, int length)
	{
		for (int row = 0; row <= length - 1; row++)
			adjMatrix[row, col] = adjMatrix[row, col + 1];
}


原书P291-P292 代码

public void TopSort()
{
	Stack<string> gStack = new Stack<string>();
	while (NUM_VERTICES > 0)
	{
		int currVertex = NoSuccessors();
		if (currVertex == -1)
		{
			Console.WriteLine("Error: graph has cycles.");
			return;
		}
		gStack.Push(vertices[currVertex].label);
		DelVertex(currVertex);
	}
	Console.Write("Topological sorting order: ");
	while (gStack.Count > 0)
		Console.Write(gStack.Pop() + " ");
}


原书P292 代码2

static void Main(string[] args)
{
	Graph theGraph = new Graph(4);
	theGraph.AddVertex("A");
	theGraph.AddVertex("B");
	theGraph.AddVertex("C");
	theGraph.AddVertex("D");
	theGraph.AddEdge(0, 1);
	theGraph.AddEdge(1, 2);
	theGraph.AddEdge(2, 3);
	theGraph.TopSort();
	Console.WriteLine();
Console.WriteLine("Finished.");
}


原书P293 代码

static void Main(string[] args)
{
Graph theGraph = new Graph(6);
theGraph.AddVertex("CS1");
	theGraph.AddVertex("CS2");
	theGraph.AddVertex("DS");
	theGraph.AddVertex("OS");
	theGraph.AddVertex("ALG");
	theGraph.AddVertex("AL");
	theGraph.AddEdge(0, 1);
	theGraph.AddEdge(1, 2);
	theGraph.AddEdge(1, 5);
	theGraph.AddEdge(2, 3);
	theGraph.AddEdge(2, 4);
	theGraph.TopSort();
	Console.WriteLine();
	Console.WriteLine("Finished.");
}


原书P294 代码

private int GetAdjUnvisitedVertex(int v)
{
	for (int j = 0; j <= NUM_VERTICES - 1; j++)
		if ( (adjMatrix[v,j] == 1) && (vertices[j].wasVisited  == false))
		return j;
	return -1;
}


原书P295 代码1

public void DepthFirstSearch()
{
	Stack<int> gStack = new Stack<int>();
	vertices[0].wasVisited = true;
	ShowVertex(0);
	gStack.Push(0);
	int v;
	while (gStack.Count > 0)
	{
		v = GetAdjUnvisitedVertex(gStack.Peek());
		if (v == -1)
		   gStack.Pop();
		else
		{
			vertices[v].wasVisited = true;
			ShowVertex(v);
			gStack.Push(v);
		}
	}
	for (int j = 0; j <= NUM_VERTICES - 1; j++)
		vertices[j].wasVisited = false;
}


原书P295-P296 代码

static void Main(string[] args)
{
	Graph aGraph = new Graph(13);
	aGraph.AddVertex("A");
	aGraph.AddVertex("B");
	aGraph.AddVertex("C");
	aGraph.AddVertex("D");
	aGraph.AddVertex("E");
	aGraph.AddVertex("F");
	aGraph.AddVertex("G");
	aGraph.AddVertex("H");
	aGraph.AddVertex("I");
	aGraph.AddVertex("J");
	aGraph.AddVertex("K");
	aGraph.AddVertex("L");
	aGraph.AddVertex("M");
	aGraph.AddEdge(0, 1);
	aGraph.AddEdge(1, 2);
	aGraph.AddEdge(2, 3);
	aGraph.AddEdge(0, 4);
	aGraph.AddEdge(4, 5);
	aGraph.AddEdge(5, 6);
	aGraph.AddEdge(0, 7);
	aGraph.AddEdge(7, 8);
	aGraph.AddEdge(8, 9);
	aGraph.AddEdge(0, 10);
	aGraph.AddEdge(10, 11);
	aGraph.AddEdge(11, 12);
	aGraph.DepthFirstSearch();
	Console.WriteLine();
}


原书P297 代码

public void BreadthFirstSearch()
{
	Queue<int> gQueue = new Queue<int>();
	vertices[0].wasVisited = true;
	ShowVertex(0);
	gQueue.Enqueue(0);
	int vert1, vert2;
	while (gQueue.Count > 0)
	{
		vert1 = gQueue.Dequeue();
		vert2 = GetAdjUnvisitedVertex(vert1);
		while (vert2 != -1)
		{
			vertices[vert2].wasVisited = true;
			ShowVertex(vert2);
			gQueue.Enqueue(vert2);
			vert2 = GetAdjUnvisitedVertex(vert1);
		}
	}
	for (int i = 0; i <= NUM_VERTICES - 1; i++)
		vertices[i].wasVisited = false;
}


原书P298 代码

static void Main(string[] args)
{
	Graph aGraph = new Graph(13);
	aGraph.AddVertex("A");
	aGraph.AddVertex("B");
	aGraph.AddVertex("C");
	aGraph.AddVertex("D");
	aGraph.AddVertex("E");
	aGraph.AddVertex("F");
	aGraph.AddVertex("G");
	aGraph.AddVertex("H");
	aGraph.AddVertex("I");
	aGraph.AddVertex("J");
	aGraph.AddVertex("K");
	aGraph.AddVertex("L");
	aGraph.AddVertex("M");
	aGraph.AddEdge(0, 1);
	aGraph.AddEdge(1, 2);
	aGraph.AddEdge(2, 3);
	aGraph.AddEdge(0, 4);
	aGraph.AddEdge(4, 5);
	aGraph.AddEdge(5, 6);
	aGraph.AddEdge(0, 7);
	aGraph.AddEdge(7, 8);
	aGraph.AddEdge(8, 9);
	aGraph.AddEdge(0, 10);
	aGraph.AddEdge(10, 11);
	aGraph.AddEdge(11, 12);
	Console.WriteLine();
	aGraph.BreadthFirstSearch();
}


原书P299-P300 代码

public void Mst()
{
	Stack<int> gStack = new Stack<int>();
	vertices[0].wasVisited = true;
	gStack.Push(0);
	int currVertex, ver;
	while (gStack.Count > 0)
	{
		currVertex = gStack.Peek();
		ver = GetAdjUnvisitedVertex(currVertex);
		if (ver == -1)
			gStack.Pop();
		else
		{
			vertices[ver].wasVisited = true;
			gStack.Push(ver);
			ShowVertex(currVertex);
			ShowVertex(ver);
			Console.Write(" ");
		}
	}
	for (int j = 0; j <= NUM_VERTICES - 1; j++)
		vertices[j].wasVisited = false;
}


原书P301 代码

static void Main(string[] args)
{
	Graph aGraph = new Graph(7);
	aGraph.AddVertex("A");
	aGraph.AddVertex("B");
	aGraph.AddVertex("C");
	aGraph.AddVertex("D");
	aGraph.AddVertex("E");
	aGraph.AddVertex("F");
	aGraph.AddVertex("G");
	aGraph.AddEdge(0, 1);
	aGraph.AddEdge(0, 2);
	aGraph.AddEdge(0, 3);
	aGraph.AddEdge(1, 2);
	aGraph.AddEdge(1, 3);
	aGraph.AddEdge(1, 4);
	aGraph.AddEdge(2, 3);
	aGraph.AddEdge(2, 5);
	aGraph.AddEdge(3, 5);
	aGraph.AddEdge(3, 4);
	aGraph.AddEdge(3, 6);
	aGraph.AddEdge(4, 5);
	aGraph.AddEdge(4, 6);
	aGraph.AddEdge(5, 6);
	Console.WriteLine();
	aGraph.Mst();
}


原书P305-P306 代码

public class Vertex
{
	public string label;
	public bool isInTree;
	public Vertex(string lab)
	{
		label = lab;
		isInTree = false;
	}
}


原书P306 代码2

public class DistOriginal
{
	public int distance;
	public int parentVert;
	public DistOriginal(int pv, int d)
	{
		distance = d;
		parentVert = pv;
	}
}


原书P306-P307 代码

public void Path()
{
	int startTree = 0;
	vertexList[startTree].isInTree = true;
	nTree = 1;
	for (int j = 0; j <= nVerts; j++)
	{
		int tempDist = adjMat[startTree, j];
		sPath[j] = new DistOriginal(startTree, tempDist);
	}
	while (nTree < nVerts)
	{
		int indexMin = GetMin();
		int minDist = sPath[indexMin].distance;
		currentVert = indexMin;
		startToCurrent = sPath[indexMin].distance;
		vertexList[currentVert].isInTree = true;
		nTree++;
		AdjustShortPath();
	}
	DisplayPaths();
	nTree = 0;
	for (int j = 0; j <= nVerts - 1; j++)
		vertexList[j].isInTree = false;
}


原书P307-P308 代码

public int GetMin()
{
	int minDist = infinity;
	int indexMin = 0;
	for (int j = 1; j <= nVerts - 1; j++)
		if (!(vertexList[j].isInTree) && sPath[j].distance < minDist)
		{
			minDist = sPath[j].distance;
			indexMin = j;
		}
	return indexMin;
}
public void AdjustShortPath()
{
	int column = 1;
	while (column < nVerts)
		if (vertexList[column].isInTree)
			column++;
		else
		{
			int currentToFring = adjMat[currentVert, column];
			int startToFringe = startToCurrent + currentToFring;
			int sPathDist = sPath[column].distance;
			if (startToFringe < sPathDist)
			{
				sPath[column].parentVert = currentVert;
				sPath[column].distance = startToFringe;
			}
			column++;
		}
}


原书P308-P312 代码

using System;
using System.Collections.Generic;
public class DistOriginal
{
	public int distance;
	public int parentVert;
	public DistOriginal(int pv, int d)
	{
		distance = d;
		parentVert = pv;
	}
}
public class Vertex
{
	public string label;
	public bool isInTree;
	public Vertex(string lab)
	{
		label = lab;
		isInTree = false;
	}
}
public class Graph
{
	private const int max_verts = 20;
	int infinity = 1000000;
	Vertex[] vertexList;
	int[,] adjMat;
	int nVerts;
	int nTree;
	DistOriginal[] sPath;
	int currentVert;
	int startToCurrent;
	public Graph()
	{
		vertexList = new Vertex[max_verts];
		adjMat = new int[max_verts, max_verts];
		nVerts = 0;
		nTree = 0;
		for (int j = 0; j <= max_verts - 1; j++)
			for (int k = 0; k <= max_verts - 1; k++)
				adjMat[j, k] = infinity;
		sPath = new DistOriginal[max_verts];
	}
	public void AddVertex(string lab)
	{
		vertexList[nVerts] = new Vertex(lab);
		nVerts++;
	}
	public void AddEdge(int start, int theEnd, int weight)
	{
		adjMat[start, theEnd] = weight;
	}
	public void Path()
	{
		int startTree = 0;
		vertexList[startTree].isInTree = true;
		nTree = 1;
		for (int j = 0; j <= nVerts; j++)
		{
			int tempDist = adjMat[startTree, j];
			sPath[j] = new DistOriginal(startTree, tempDist);
		}
		while (nTree < nVerts)
		{
			int indexMin = GetMin();
			int minDist = sPath[indexMin].distance;
			currentVert = indexMin;
			startToCurrent = sPath[indexMin].distance;
			vertexList[currentVert].isInTree = true;
			nTree++;
			AdjustShortPath();
		}
		DisplayPaths();
		nTree = 0;
		for (int j = 0; j <= nVerts - 1; j++)
			vertexList[j].isInTree = false;
	}
	public int GetMin()
	{
		int minDist = infinity;
		int indexMin = 0;
		for (int j = 1; j <= nVerts - 1; j++)
			if (!(vertexList[j].isInTree) && sPath[j].distance < minDist)
			{
				minDist = sPath[j].distance;
				indexMin = j;
			}
		return indexMin;
	}
	public void AdjustShortPath()
	{
		int column = 1;
		while (column < nVerts)
			if (vertexList[column].isInTree)
				column++;
			else
			{
				int currentToFring = adjMat[currentVert, column];
				int startToFringe = startToCurrent + currentToFring;
				int sPathDist = sPath[column].distance;
				if (startToFringe < sPathDist)
				{
					sPath[column].parentVert = currentVert;
					sPath[column].distance = startToFringe;
				}
				column++;
			}
	}
	public void DisplayPaths()
	{
		for (int j = 0; j <= nVerts - 1; j++)
		{
			Console.Write(vertexList[j].label + "=");
			if (sPath[j].distance == infinity)
				Console.Write("inf");
			else
				Console.Write(sPath[j].distance);
			string parent = vertexList[sPath[j].parentVert].
			label;
			Console.Write("(" + parent + ") ");
		}
	}
	class chapter16
	{
		static void Main()
		{
			Graph theGraph = new Graph();
			theGraph.AddVertex("A");
			theGraph.AddVertex("B");
			theGraph.AddVertex("C");
			theGraph.AddVertex("D");
			theGraph.AddVertex("E");
			theGraph.AddVertex("F");
			theGraph.AddVertex("G");
			theGraph.AddEdge(0, 1, 2);
			theGraph.AddEdge(0, 3, 1);
			theGraph.AddEdge(1, 3, 3);
			theGraph.AddEdge(1, 4, 10);
			theGraph.AddEdge(2, 5, 5);
			theGraph.AddEdge(2, 0, 4);
			theGraph.AddEdge(3, 2, 2);
			theGraph.AddEdge(3, 5, 8);
			theGraph.AddEdge(3, 4, 2);
			theGraph.AddEdge(3, 6, 4);
			theGraph.AddEdge(4, 6, 6);
			theGraph.AddEdge(6, 5, 1);
			Console.WriteLine();
			Console.WriteLine("Shortest paths:");
			Console.WriteLine();
			theGraph.Path();
			Console.WriteLine();
		}
	}
}
