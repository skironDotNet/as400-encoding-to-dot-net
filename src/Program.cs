using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace As400EncodingTest
{
	class Program
	{
		static void Main(string[] args)
		{                                
			//http://www.rapidtables.com/code/text/ascii-table.htm
			var asciiArray = new byte[] { 0x41, 0xD1, 0x42, 0xA0, 0x58 }; //A, Ñ, B, Space of A0, X
			var a = Encoding.ASCII;
			var b = Encoding.GetEncoding("iso-8859-1");      //ASCII 819 equivlent https://en.wikipedia.org/wiki/ISO/IEC_8859-1
			var c = Encoding.GetEncoding("iso-8859-15");     //ASCII 819 equivlent
			var d = Encoding.Unicode;
			var e = Encoding.UTF7;
			var f = Encoding.UTF32;

			Console.WriteLine("IBM ASCII 819 ");
			Console.WriteLine("byte[] { 0x41, 0xD1, 0x42, 0xA0, 0x58 }; //A, Ñ, B, Space of A0, X");
			Console.WriteLine();
			Console.WriteLine("UTF8:\t\t" + Encoding.UTF8.GetString(asciiArray));
			Console.WriteLine("ASCII:\t\t" + a.GetString(asciiArray));
			Console.WriteLine("iso-8859-1:\t" + b.GetString(asciiArray));
			Console.WriteLine("iso-8859-15:\t" + c.GetString(asciiArray));
			Console.WriteLine("Unicode:\t" + d.GetString(asciiArray));
			Console.WriteLine("UTF7:\t\t" + e.GetString(asciiArray));
			Console.WriteLine("UTF32:\t\t" + f.GetString(asciiArray));
			Console.WriteLine();

			byte[] chars = Encoding.UTF8.GetBytes(Encoding.UTF7.GetString(asciiArray));
			Console.WriteLine("UTF7 to UTF8:\t" + Encoding.UTF8.GetString(chars));
			Console.ReadLine();
		}
	}
}
