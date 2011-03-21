<Query Kind="Program">
  <Reference>&lt;ProgramFiles&gt;\Microsoft Visual Studio 9.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
</Query>

	static unsafe void Main()
	{
		int* fib = stackalloc int[100];
		int* p = fib;
		*p++ = *p++ = 1;
		for (int i = 2; i < 100; ++i, ++p)
		{
			*p = p[-1] + p[-2];
		}
		for (int i = 0; i < 10; ++i)
		{
			Console.WriteLine(fib[i]);
		}
	}
