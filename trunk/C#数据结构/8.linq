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

原书P149 代码1

using System;
using System.Text.RegularExpressions;
class chapter8
{
	static void Main()
	{
		Regex reg = new Regex("the");
		string str1 = "the quick brown fox jumped over the lazy dog";
		Match matchSet;
		int matchPos;
		matchSet = reg.Match(str1);
		if (matchSet.Success)
		{ 
			matchPos = matchSet.Index;
			Console.WriteLine("found match at position:" + matchPos);
		}
	}
}


原书P149 代码2

if (Regex.IsMatch(str1, "the"))
{
	Match aMatch;
aMatch = reg.Match(str1);
}


原书P150 代码1

using System;
using System.Text.RegularExpressions;
class chapter8
{
	static void Main()
	{
		Regex reg = new Regex("the");
		string str1 = "the quick brown fox jumped over the lazy dog";
		MatchCollection matchSet;
		matchSet = reg.Matches(str1);
		if (matchSet.Count > 0)
			foreach (Match aMatch in matchSet)
				Console.WriteLine("found a match at: " + aMatch.Index);
		Console.Read();
	}
}


原书P150 代码2

string s = "the quick brown fox jumped over the brown dog";
s = Regex.Replace(s, "brown", "black");


原书P151 代码

using System;
using System.Text.RegularExpressions;
class chapter8
{
	static void Main()
	{
		string[] words = new string[] {"bad", "boy", "baaad","bear", "bend"};
		foreach (string word in words)
			if (Regex.IsMatch(word, "ba+"))
				Console.WriteLine(word);
	}
}


原书P152 代码1

using System;
using System.Text.RegularExpressions;
class chapter8
{
	static void Main()
	{
		string[] words = new string[] {"bad", "boy", "baad","baaad", "bear", "bend"};
		foreach (string word in words)
			if (Regex.IsMatch(word, "ba{2}d"))
				Console.WriteLine(word);
	}
}


原书P152-P153 代码

using System;
using System.Text.RegularExpressions;
class chapter8
{
	static void Main()
	{
		string[] words = new string[]{"Part", "of", "this","<b>string</b>", "is", "bold"};
		string regExp = "<.*>";
		MatchCollection aMatch;
		foreach (string word in words)
		{
			if (Regex.IsMatch(word, regExp))
			{
				aMatch = Regex.Matches(word, regExp);
				for (int i = 0; i < aMatch.Count; i++)
					Console.WriteLine(aMatch[i].Value);
			}
		}
	}
}


原书P153-P154 代码

using System;
using System.Text.RegularExpressions;
class chapter8
{
	static void Main()
	{
		string str1 = "the quick brown fox jumped over the lazy dog";
		MatchCollection matchSet;
		matchSet = Regex.Matches(str1, ".");
		foreach (Match aMatch in matchSet)
			Console.WriteLine("matches at: " + aMatch.Index);
	}
}


原书P154 代码2

using System;
using System.Text.RegularExpressions;
class chapter8
{
	static void Main()
	{
		string str1 = "the quick brown fox jumped over the lazy dog one time";
		MatchCollection matchSet;
		matchSet = Regex.Matches(str1, "t.e");
		foreach (Match aMatch in matchSet)
			Console.WriteLine("Matches at: " + aMatch.Index);
	}
}


原书P155 代码

using System;
using System.Text.RegularExpressions;
class chapter8
{
	static void Main()
	{
		string str1 = "THE quick BROWN fox JUMPED over THE lazy DOG";
		MatchCollection matchSet;
		matchSet = Regex.Matches(str1, "[a-z]");
		foreach (Match aMatch in matchSet)
			Console.WriteLine("Matches at: " + aMatch.Index);
	}
}


原书P156-P157 代码

using System;
using System.Text.RegularExpressions;
class chapter8
{
	static void Main()
	{
		string[] words = new string[] { "heal", "heel", "noah", "techno" };
		string regExp = "^h";
		Match aMatch;
		foreach (string word in words)
			if (Regex.IsMatch(word, regExp))
			{
				aMatch = Regex.Match(word, regExp);
				Console.WriteLine("Matched: " + word + " at position: " + aMatch.Index);
			}
	}
}


原书P157 代码2

string regExp = "h$";


原书P157 代码3

string words = "hark, what doth thou say, Harold? ";
string regExp = "\\bh";


原书P158 代码

using System;
using System.Text.RegularExpressions;
class chapter8
{
	static void Main()
	{
		string words = "08/14/57 46 02/25/59 45 06/05/85 18" + "03/12/88 16 09/09/90 13";
		string regExp1 = "(\\s\\d{2}\\s)";
		MatchCollection matchSet = Regex.Matches(words,regExp1);
		foreach (Match aMatch in matchSet)
			Console.WriteLine(aMatch.Groups[0].Captures[0]);
	}
}


原书P159 代码

using System;
using System.Text.RegularExpressions;
class chapter8
{
	static void Main()
	{
		string words = "08/14/57 46 02/25/59 45 06/05/85 18 " + "03/12/88 16 09/09/90 13";
		string regExp1 = "(?<dates>(\\d{2}/\\d{2}/\\d{2}))\\s";
		MatchCollection matchSet = Regex.Matches(words,regExp1);
		foreach (Match aMatch in matchSet)
			Console.WriteLine("Date: {0}", aMatch.Groups["dates"]);
	}
}


原书P160 代码1

Console.WriteLine("Date: {0}", aMatch.Groups("dates"));


原书P160 代码2

string words = "lions lion tigers tiger bears,bear";
string regExp1 = "\\w+(?=\\s)";


原书P161 代码1

string words = "subroutine routine subprocedure procedure";
string regExp1 = "\\b(?!sub)\\w+\\b";


原书P161 代码2

string words = "subroutines routine subprocedures
procedure";
string regExp1 = "\\b\\w+(?<=s)\\b";


原书P161 代码3

string regExp1 = "\\b\\w+(?<!s)\\b";


原书P162 代码

using System;
using System.Text.RegularExpressions;
class chapter8
{
	static void Main()
	{
		string dates = "08/14/57 46 02/25/59 45 06/05/85 18 " + "03/12/88 16 09/09/90 13";
		string regExp = "(?<dates>(\\d{2}/\\d{2}/\\d{2}))\\s(?<ages>(\\d{2}))\\s";
		MatchCollection matchSet;
		matchSet = Regex.Matches(dates, regExp);
		Console.WriteLine();
		foreach (Match aMatch in matchSet)
		{
			foreach (Capture aCapture in aMatch.Groups["dates"].Captures)
				Console.WriteLine("date capture: " + aCapture.ToString());
			foreach (Capture aCapture in aMatch.Groups["ages"].Captures)
				Console.WriteLine("age capture: " + aCapture.ToString());
		}
	}
}


原书P163 代码

matchSet = Regex.Matches(dates, regexp, RegexOptions.Multiline);
