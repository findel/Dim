
using System;

namespace Dim
{
	public static class DimConsole
	{
		
		public static void WriteIntro(string intro)
		{
			Console.WriteLine("#### " + intro + " ####");
			Console.WriteLine("#");
		}
		
		public static void WriteLine(string line1, string line2 = "", string line3 = "")
		{
			Console.WriteLine("#    " + line1);
			
			if(!string.IsNullOrEmpty(line2))
				Console.WriteLine("#    " + line2);
			
			if(!string.IsNullOrEmpty(line3))
				Console.WriteLine("#    " + line3);
			
			Console.WriteLine("#");
		}
		
		public static void WriteInfoLine(string line1)
		{
			Console.WriteLine("#    * " + line1);
			Console.WriteLine("#");
		}
		
	}
}
