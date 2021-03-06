# AS400 Encoding vs Windows and .NET

As part of my work at Bunzl USA. I had a chance to make a deep research on why XML data submitted from AS400 system to .NET WebApi fails.

I want to help other people to save time, but I will not go too deep into the details as I present this some time after the discovery. You can take it from here...

Attached XML formatter in src folder


----------



By default IBM, ASCII 819 in Windows/.NET is https://en.wikipedia.org/wiki/ISO/IEC_8859-1 but not quite

> The Windows-1252 codepage coincides with ISO-8859-1 for all codes except the range 128 to 159 (hex 80 to 9F), where the little-used C1 controls are replaced with additional characters including all the missing characters provided by ISO-8859-15. Code page 28591 a.k.a. Windows-28591 is the actual ISO-8859-1 codepage.
   
Conversion Test

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

![Console output](/encodings.png)

