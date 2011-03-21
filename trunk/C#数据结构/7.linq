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

原书P120 代码1

string name = "Jennifer Ingram";


原书P120 代码2

string name = "Mike McMillan\nInstructor, CIS\tRoom 306";


原书P121 代码

using System;
class Chapter7
{
	static void Main()
	{
		string string1 = "Hello, world!";
		int len = string1.Length;
		int pos = string1.IndexOf(" ");
		string firstWord, secondWord;
		firstWord = string1.Substring(0, pos);
		secondWord = string1.Substring(pos + 1,(len - 1) - (pos + 1));
		Console.WriteLine("First word: " + firstWord);
		Console.WriteLine("Second word: " + secondWord);
		Console.Read();
	}
}


原书P122 代码

string s = "Now is the time";
string sub = s.Substring(0, 3);


原书P123 代码1

using System;
using System.Collections; 
class Chapter7
{
	static void Main()
	{
		string astring = "Now is the time";
		int pos;
		string word;
		ArrayList words = new ArrayList();
		pos = astring.IndexOf(" ");
		while (pos > 0)
		{ 
			word = astring.Substring(0, pos);
			words.Add(word);
			astring = astring.Substring(pos + 1, astring.Length - (pos + 1));
			pos = astring.IndexOf(" ");
			if (pos == -1)
			{
				word = astring.Substring(0, astring.Length); 
				words.Add(word);
			}
			Console.Read();
		}
	}
}


原书P123-P124 代码

using System;
using System.Collections;
class Chapter7
{
	static void Main()
	{
		string astring = "now is the time for all good people ";
		ArrayList words = new ArrayList();
		words = SplitWords(astring);
		foreach (string word in words)
			Console.Write(word + " ");
		Console.Read();
	}
	static ArrayList SplitWords(string astring)
	{
		string[] ws = new string[astring.Length - 1];
		ArrayList words = new ArrayList();
		int pos;
		string word;
		pos = astring.IndexOf(" ");
		while (pos > 0)
		{
			word = astring.Substring(0, pos);
			words.Add(word);
			astring = astring.Substring(pos + 1,astring.Length - (pos + 1));
			if (pos == -1)
			{
				word = astring.Substring(0, astring.Length);
				words.Add(word);
			}
			pos = astring.IndexOf(" "); 
		}
		return words;
	}
}


原书P125 代码1

string data = "Mike,McMillan,3000 W. Scenic,North Little Rock,AR,72118";
string[] sdata;
char[] delimiter = new char[] {','};
sdata = data.Split(delimiter, data.Length);


原书P125 代码2

foreach (string word in sdata)
	Console.Write(word + " ");


原书P125 代码3

sdata = data.Split(delimiter, 2);


原书P126 代码

using System;
class Chapter7
{
	static void Main()
	{
		string data = "Mike,McMillan,3000 W. Scenic,North Little Rock,AR,72118";
		string[] sdata;
		char[] delimiter = new char[] { ',' };
		sdata = data.Split(delimiter, data.Length);
		foreach (string word in sdata)
			Console.Write(word + " ");
		string joined;
		joined = String.Join(“,”, sdata);
		Console.Write(joined);
	}
}


原书P127 代码1

int charCode;
charCode = (int)'a';


原书P127 代码2

string s1 = "foobar";
string s2 = "foobar";
if (s1.Equals(s2))
	Console.WriteLine("They are the same.");
else
	Console.WriteLine("They are not the same.");


原书P128 代码1

string s1 = "foobar";
string s2 = "foobar";
Console.WriteLine(s1.CompareTo(s2)); // returns 0
s2 = "foofoo";
Console.WriteLine(s1.CompareTo(s2)); // returns -1
s2 = "fooaar";
Console.WriteLine(s1.CompareTo(s2)); // returns 1


原书P128 代码2

static void Main()
{
string s1 = "foobar";
string s2 = "foobar";
int compVal = String.Compare(s1, s2);
switch (compVal)
{
		case 0: Console.WriteLine(s1 + " " + s2 + " are equal");
break;
	case 1: Console.WriteLine(s1 + " is less than " + s2);
break;
	case 2: Console.WriteLine(s1 + " is greater than" + s2);
break;
	default: Console.WriteLine("Can't compare");
break;
}
}


原书P129 代码1

using System;
using System.Collections;
class Chapter7
{
	static void Main()
	{
		string[] nouns = new string[] {"cat", "dog", "bird","eggs", "bones"};
		ArrayList pluralNouns = new ArrayList();
		foreach (string noun in nouns)
			if (noun.EndsWith("s"))
				pluralNouns.Add(noun);
		foreach (string noun in pluralNouns)
			Console.Write(noun + " ");
	}
}


原书P129-P130 代码

using System;
using System.Collections;
class Chapter7
{
	static void Main()
	{
		string[] words = new string[]{"triangle", "diagonal", "trimester","bifocal","triglycerides"};
		ArrayList triWords = new ArrayList();
		foreach (string word in words)
			if (word.StartsWith("tri"))
				triWords.Add(word);
		foreach (string word in triWords)
			Console.Write(word + " ");
	}
}


原书P130 代码2

String1 = String0.Insert(Position, String);


原书P130 代码3

using System;
class chapter7
{
	static void Main()
	{
		string s1 = "Hello, . Welcome to my class.";
		string name = "Clayton";
		int pos = s1.IndexOf(",");
		s1 = s1.Insert(pos + 2, name);
		Console.WriteLine(s1);
	}
}


原书P131 代码1

using System;
class chapter7
{
	static void Main()
	{
		string s1 = "Hello, . Welcome to my class.";
		string name = "Ella";
		int pos = s1.IndexOf(",");
		s1 = s1.Insert(pos + 2, name);
		Console.WriteLine(s1);
		s1 = s1.Remove(pos + 2, name.Length);
		Console.WriteLine(s1);
	}
}


原书P131 代码2

string name = "William Shakespeare";
int pos = s1.IndexOf(",");
s1 = s1.Insert(pos + 2, name);
Console.WriteLine(s1);
s1 = s1.Remove(pos + 2, name.Length);
Console.WriteLine(s1);


原书P132 代码1

using System;
class chapter7
{
	static void Main()
	{
		string[] words = new string[] { "recieve", "decieve", "reciept" };
		for (int i = 0; i <= words.GetUpperBound(0); i++)
		{
			words[i] = words[i].Replace("cie", "cei");
			Console.WriteLine(words[i]);
		}
	}
}


原书P132 代码2

words[index].Replace("cie", "cei");


原书P132 代码3

string s1 = "Hello";
Console.WriteLine(s1.PadLeft(10));
Console.WriteLine("world");


原书P133 代码1

string s1 = "Hello";
string s2 = "world";
string s3 = "Goodbye";
Console.Write(s1.PadLeft(10));
Console.WriteLine(s2.PadLeft(10));
Console.Write(s3.PadLeft(10));
Console.WriteLine(s2.PadLeft(10));


原书P133-P134 代码

using System;
class chapter7
{
	static void Main()
	{
		string[,] names = new string[,]
		{
			{"1504", "Mary", "Ella", "Steve", "Bob"},
			{"1133", "Elizabeth", "Alex", "David", "Joe"},
			{"2624", "Joel", "Chris", "Craig", "Bill"}
		};
		Console.WriteLine();
		Console.WriteLine();
		for (int outer = 0; outer <= names.GetUpperBound(0);outer++)
		{
			for (int inner = 0; inner <= names.GetUpperBound(1); inner++)
				Console.Write(names[outer, inner] + " ");
			Console.WriteLine();
		}
		Console.WriteLine();
		Console.WriteLine();
		for (int outer = 0; outer <= names.GetUpperBound(0); outer++)
		{
			for (int inner = 0; inner <= names.GetUpperBound(1); inner++)
				Console.Write(names[outer, inner].PadRight(10) + " ");
			Console.WriteLine();
		}
	}
}


原书P134-P135 代码

using System;
class chapter7
{
	static void Main()
	{
		string s1 = "hello";
		string s2 = "world";
		string s3 = "";
		s3 = String.Concat(s1, " ", s2);
		Console.WriteLine(s3);
	}
}


原书P135 代码2

string s1 = "hello";
s1 = s1.ToUpper();
Console.WriteLine(s1);
string s2 = "WORLD";
Console.WriteLine(s2.ToLower());


原书P135-P136 代码

using System;
class chapter7
{
	static void Main()
	{
		string[] names = new string[] {" David", " Raymond", "Mike ", "Bernica "};
		Console.WriteLine();
		showNames(names);
		Console.WriteLine();
		trimVals(names);
		Console.WriteLine();
		showNames(names);
	}
	static void showNames(string[] arr)
	{
		for (int i = 0; i <= arr.GetUpperBound(0); i++)
			Console.Write(arr[i]);
	}
	static void trimVals(string[] arr)
	{
		char[] charArr = new char[] { ' ' };
		for (int i = 0; i <= arr.GetUpperBound(0); i++)
		{
			arr[i] = arr[i].Trim(charArr[0]);
			arr[i] = arr[i].TrimEnd(charArr[0]);
		}
	}
}


原书P136-P137 代码

using System;
class chapter7
{
	static void Main()
	{
		string[] htmlComments = new string[]
		{
			"<!-- Start Page Number Function -->",
			"<!-- Get user name and password -->",
			"<!-- End Title page -->",
			"<!-- End script -->"
		};
		char[] commentChars = new char[] {'<', '!', '-','>'};
		for (int i = 0; i <= htmlComments.GetUpperBound(0); i++)
		{
			htmlComments[i] = htmlComments[i].
			Trim(commentChars);
			htmlComments[i] = htmlComments[i].
			TrimEnd(commentChars);
		}
		for (int i = 0; i <= htmlComments.GetUpperBound(0); i++)
			Console.WriteLine("Comment: " + htmlComments[i]);
	}
}


原书P138 代码1

StringBuilder stBuff1 = new StringBuilder();


原书P138 代码2

StringBuilder stBuff2 = new StringBuilder(25);


原书P138 代码3

StringBuilder stBuff3 = new StringBuilder("Hello,world");


原书P138-P139 代码

StringBuilder stBuff = new StringBuilder("Ken Thompson");
Console.WriteLine("Length of stBuff3: " + stBuff.Length.ToString());
Console.WriteLine("Capacity of stBuff3: " + stBuff.Capacity.ToString());
Console.WriteLine("Maximum capacity of stBuff3: " + stBuff.MaxCapacity.ToString());


原书P139 代码2

stBuff.Length = 10;
Console.Write(stBuff3);


原书P139 代码3

stBuff.EnsureCapacity(25);


原书P139 代码4

StringBuilder stBuff = new StringBuilder("Ronald Knuth");
If (stBuff[0] != ‘D’)
stBuff[0] = ‘D’;


原书P140 代码1

using System; 
using System.Text;
class chapter7
{
	static void Main() 
	{
		StringBuilder stBuff = new StringBuilder();
		String[] words = new string[] {"now ", "is ", "the ", "time ", "for ", "all ",
			"good ", "men ", "to ", "come ", "to ", "the ","aid ", "of ", "their ", "party"};
		for(int i = 0; i <= words.GetUpperBound(0); i++)
				stBuff.Append(words[i]);
		Console.WriteLine(stBuff);
	}
}


原书P140-P141 代码

using System; 
using System.Text;
class chapter7
{
	static void Main()
	{
		StringBuilder stBuff = new StringBuilder();
		Console.WriteLine();
		stBuff.AppendFormat("Your order is for {0000} widgets.", 234);
		stBuff.AppendFormat("\nWe have {0000} widgets left.", 12);
		Console.WriteLine(stBuff);
	}
}


原书P141 代码2

using System.Text;
using System;
class chapter7
{
static void Main()
{
		StringBuilder stBuff = new StringBuilder();
		stBuff.Insert(0, "Hello");
		stBuff.Append("world");
		stBuff.Insert(5, ", ");
		Console.WriteLine(stBuff);
		char [] chars  = new char[] { 't', 'h', 'e', 'r', 'e' };
		stBuff.Insert(5, " " + new string(chars));
		Console.WriteLine(stBuff);
	}
}


原书P142 代码1

StringBuilder stBuff = new StringBuilder();
stBuff.Insert(0, "and on ", 6);
Console.WriteLine(stBuff);


原书P142 代码2

StringBuilder stBuff = new StringBuilder("noise in+++++string");
stBuff.Remove(9, 5);
Console.WriteLine(stBuff);


原书P142 代码3

StringBuilder stBuff = new StringBuilder("recieve decieve reciept");
stBuff.Replace("cie", "cei");
Console.WriteLine(stBuff);


原书P143代码

using System; 
using System.Text;
class chapter7
{
	static void Main()
	{
		StringBuilder stBuff = new StringBuilder("HELLO WORLD");
		string st = stBuff.ToString();
		st = st.ToLower();
		st = st.Replace(st.Substring(0, 1),
		st.Substring(0, 1).ToUpper());
		stBuff.Replace(stBuff.ToString(), st);
		Console.WriteLine(stBuff);
	}
}


原书P144-P145代码

using System;
using System.Text;

class chapter7
{
	static void Main()
	{
		int size = 100;
		Timing timeSB = new Timing();
		Timing timeST = new Timing();

		Console.WriteLine();
		for (int i = 0; i <= 3; i++)
		{
			timeSB.startTime();
			BuildSB(size);
			timeSB.stopTime();
			timeST.startTime();
			BuildString(size);
			timeST.stopTime();
			Console.WriteLine("Time (in milliseconds) to build StringBuilder " + "object for " + size + " elements: " + timeSB.Result().TotalMilliseconds);
			Console.WriteLine("Time (in milliseconds) to build String object " + "for " + size + " elements: " + timeST.Result().TotalMilliseconds);
			Console.WriteLine();
			size *= 10;
		}
	}
	static void BuildSB(int size)
	{
		StringBuilder sbObject = new StringBuilder();
		for (int i = 0; i <= size; i++)
			sbObject.Append("a");
	}
	static void BuildString(int size)
	{
		string stringObject = "";
		for (int i = 0; i <= size; i++)
			stringObject += "a";
	}
}
