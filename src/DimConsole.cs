
using System;

namespace Dim
{
	public static class DimConsole
	{
		
		public static void WriteIntro(string intro)
		{
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("#### " + intro + " ####");
			Console.ResetColor();
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
			Console.Write("#    ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("* " + line1);
			Console.ResetColor();
			Console.WriteLine("#");
			
		}
		
		private static int loaderCounter = 0;
		
		public static void WriteLoader()
		{
			Console.Write("#    ");
			Console.ForegroundColor = ConsoleColor.Green;
			loaderCounter++;
			switch (loaderCounter)
			{
				case 1:
					Console.Write("-         ");
					break;
				case 2:
					Console.Write("=         ");
					break;
				case 3:
					Console.Write("=-        ");
					break;
				case 4:
					Console.Write("==        ");
					break;
				case 5:
					Console.Write("==-       ");
					break;
				case 6:
					Console.Write("===       ");
					break;
				case 7:
					Console.Write("===-      ");
					break;
				case 8:
					Console.Write("====      ");
					break;
				case 9:
					Console.Write("====-     ");
					break;
				case 10:
					Console.Write("=====     ");
					break;
				case 11:
					Console.Write("=====-    ");
					break;
				case 12:
					Console.Write("======    ");
					break;
				case 13:
					Console.Write("======-   ");
					break;
				case 14:
					Console.Write("=======   ");
					break;
				case 15:
					Console.Write("=======-  ");
					break;
				case 16:
					Console.Write("========  ");
					break;
				case 17:
					Console.Write("========- ");
					break;
				case 18:
					Console.Write("========= ");
					break;
				case 19:
					Console.Write("=========-");
					break;
				case 20:
					Console.Write("==========");
					loaderCounter = 0;
					break;

			}
			Console.ResetColor();
			Console.SetCursorPosition(Console.CursorLeft - 15, Console.CursorTop);
		}
		
		public static void StopSpiner()
		{
			Console.Write("               ");
			Console.SetCursorPosition(Console.CursorLeft - 15, Console.CursorTop);
		}
		
	}
}
