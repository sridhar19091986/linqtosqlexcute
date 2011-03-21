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

原书P95-1 代码

public void GenPrimes()
{
	for (int outer = 2; outer <= arr.GetUpperBound(0); outer++)
		for (int inner = outer + 1; inner <= arr.GetUpperBound(0); inner++)
			if (arr[inner] == 1)
				if ((inner % outer) == 0)
					arr[inner] = 0;
}


原书P95-2 代码

public void ShowPrimes()
{
	for (int i = 2; i <= arr.GetUpperBound(0); i++)
		if (arr[i] == 1)
			Console.Write(i + " ");
}


原书P95-3 代码

static void Main()
{
	int size = 100;
	CArray primes = new CArray(size - 1);
	for (int i = 0; i <= size - 1; i++)
		primes.Insert(1);
	primes.GenPrimes();
	primes.ShowPrimes();
}


原书P101-P103 代码

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
public class Form1 : System.Windows.Forms.Form
{
	private System.Windows.Forms.Button btnAdd;
	private System.Windows.Forms.Button btnClear;
	private System.Windows.Forms.Button btnOr;
	private System.Windows.Forms.Button btnXor;
	private System.Forms.Label lblInt1Bits;
	private System.Forms.Label lblInt2Bits;
	private System.Forms.TextBox txtInt1;
	private System.Forms.TextBox txtInt2;
	// other Windows app code here
	private void btnAdd_Click(object sender, System.EventArgs e)
	{
		int val1, val2;
		val1 = Int32.Parse(txtInt1.Text);
		val2 = Int32.Parse(txtInt2.Text);
		lblInt1Bits.Text = ConvertBits(val1).ToString();
		lblInt2Bits.Text = ConvertBits(val2).ToString();
	}
	private StringBuilder ConvertBits(int val)
	{
		int dispMask = 1 << 31;
		StringBuilder bitBuffer = new StringBuilder(35);
		for (int i = 1; i <= 32; i++)
		{
			if ((val && bitMask) == 0)
				bitBuffer.Append("0");
			else
				bitBuffer.Append("1");
			val <<= 1;
			if ((i % 8) == 0)
				bitBuffer.Append(" ");
		}
		return bitBuffer;
	}
	private void btnClear_Click(object sender, System.Eventargs e)
	{
		txtInt1.Text = "";
		txtInt2.Text = "";
		lblInt1Bits.Text = "";
		lblInt2Bits.Text = "";
		lblBitResult.Text = "";
		txtInt1.Focus();
	}
	private void btnOr_Click(object sender, System.EventsArgs e)
	{
		int val1, val2;
		val1 = Int32.Parse(txtInt1.Text);
		val2 = Int32.Parse(txtInt2.Text);
		lblInt1Bits.Text = ConvertBits(val1).ToString();
		lblInt2Bits.Text = ConvertBits(val2).ToString();
		lblBitResult.Text = ConvertBits(val1 ||
		val2).ToString();
	}
	private void btnXOr_Click(object sender, System.EventsArgs e)
	{
		int val1, val2;
		val1 = Int32.Parse(txtInt1.Text);
		val2 = Int32.Parse(txtInt2.Text);
		lblInt1Bits.Text = ConvertBits(val1).ToString();
		lblInt2Bits.Text = ConvertBits(val2).ToString();
		lblBitResult.Text = ConvertBits(val1 ^ val2).ToString();
	}
}


原书P106 代码

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
public class Form1 : System.Windows.Forms.Form
{
	// Windows generated code omitted here
	private void btnOr_Click(object sender,
	System.EventsArgs e)
	{
		int val1, val2;
		val1 = Int32.Parse(txtInt1.Text);
		val2 = Int32.Parse(txtInt2.Text);
		lblInt1Bits.Text = ConvertBits(val1).ToString();
		lblInt2Bits.Text = ConvertBits(val2).ToString();
		lblBitResult.Text = ConvertBits(val1 || val2).
		ToString();
	}
	private StringBuilder ConvertBits(int val)
	{
		int dispMask = 1 << 31;
		StringBuilder bitBuffer = new StringBuilder(35);
		for (int i = 1; i <= 32; i++)
		{
			if ((val && bitMask) == 0)
				bitBuffer.Append("0");
			else
				bitBuffer.Append("1");
			val <<= 1;
			if ((i % 8) == 0)
				bitBuffer.Append(" ");
		}
		return bitBuffer;
	}
}


原书P107-P108 代码

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
public class Form1 : System.Windows.Forms.Form
{
	// Windows generated code omitted
	private StringBuilder ConvertBits(int val)
	{
		int dispMask = 1 << 31;
		StringBuilder bitBuffer = new StringBuilder(35);
		for (int i = 1; i <= 32; i++)
		{
			if ((val && bitMask) == 0)
				bitBuffer.Append("0");
			else
				bitBuffer.Append("1");
			val <<= 1;
			if ((i % 8) == 0)
				bitBuffer.Append(" ");
		}
		return bitBuffer;
	}
	private void btnOr_Click(object sender, System.EventsArgs e)
	{
		txtInt1.Text = "";
		txtBitShift.Text = "";
		lblInt1Bits.Text = "";
		lblOrigBits.Text = "";
		txtInt1.Focus();
	}
	private void btnLeft_Click(object sender, System.EventsArgs e)
	{
		int value = Int32.Parse(txtInt1.Text);
		lblOrigBits.Text = ConvertBits(value).ToString();
		value <<= Int32.Parse(txtBitShift.Text);
		lblInt1Bits.Text = ConvertBits(value).ToString();
	}
	private void btnRight_Click(object sender, System.EventsArgs e)
	{
		int value = Int32.Parse(txtInt1.Text);
		lblOrigBits.Text = ConvertBits(value).ToString();
		value >>= Int32.Parse(txtBitShift.Text);
		lblInt1Bits.Text = ConvertBits(value).ToString();
	}
}


原书P110-1 代码
BitArray BitSet = new BitArray(32);


原书P110-2 代码

BitArray BitSet = new BitArray(32,true);


原书P110-3 代码

byte[] ByteSet = new byte[] { 1, 2, 3, 4, 5 };
BitArray BitSet = new BitArray(ByteSet);


原书P111 代码

byte[] ByteSet = new byte[] {1, 2, 3, 4, 5};
BitArray BitSet = new BitArray(ByteSet);
for (int bits = 0; bits <= BitSet.Count-1; bits++)
Console.Write(BitSet.Get(bits) + " ");


原书P112 代码

using System;
using System.Collections; 
class chapter6
{
	static void Main()
	{
		int bits;
		string[] binNumber = new string[8];
		int binary;
		byte[] ByteSet = new byte[] { 1, 2, 3, 4, 5 };
		BitArray BitSet = new BitArray(ByteSet);
		bits = 0;
		binary = 7;
		for (int i = 0; i <= BitSet.Count - 1; i++)
		{
			if (BitSet.Get(i) == true)
				binNumber[binary] = "1";
			else
				binNumber[binary] = "0";
			bits++;
			binary--;
			if ((bits % 8) == 0)
			{
				binary = 7;
				bits = 0;
				for (int j = 0; j <= 7; j++)
					Console.Write(binNumber[j]);
				Console.WriteLine();
			}
		}
	}
}


原书P113-1 代码

BitArray.Set(bit, value)


原书P113-2 代码

bitSet1.Or(bitSet2)


原书P113-3 代码

bitSet.Clone()


原书P114 代码

bitSet.CopyTo(arrBits)


原书P115-P116 代码

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
public class Form1 : System.Windows.Forms.Form
{
	// Windows generated code omitted
	private void btnPrime_Click(object sender,
	System.EventsArgs e)
	{
		BitArray[] bitSet = new BitArray[1024];
		int value = Int32.Parse(txtValue.Text);
		BuildSieve(bitSet);
		if (bitSet.Get(value))
			lblPrime.Text = (value + " is a prime number.");
		else
			lblPrime.Text = (value + " is not a prime number.");
	}
	private void BuildSieve(BitArray bits)
	{
		string primes;
		for (int i = 0; i <= bits.Count - 1; i++)
			bits.Set(i, 1);
		int lastBit = Int32.Parse(Math.
		Sqrt(bits.Count));
		for (int i = 2; i <= lastBit - 1; i++)
			if (bits.Get(i))
				for (int j = 2 * i; j <= bits.Count - 1; j++)
					bits.Set(j, 0);
		int counter = 0;
		for (int i = 1; i <= bits.Count - 1; i++)
			if (bits.Get(i))
			{
				primes += i.ToString();
				counter++;
				if ((counter % 7) == 0)
					primes += "\n";
				else
					primes += "\n";
			}
		txtPrimes.Text = primes;
	}
}


原书P117-1 代码

int lastBit = Int32.Parse(Math.Sqrt(bits.Count).ToString());
for(int i = 2; i <= lastBit-1; i++)
if (bits.Get(i))
for (int j = 2*i; j <= bits.Count-1; j++)
bits.Set(j, false);


原书P117-2 代码

bitSet.Get(value)
