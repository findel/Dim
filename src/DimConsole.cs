﻿
using System;

namespace Dim
{
	public static class DimConsole
	{
		
		public static void WriteIntro(string text)
		{
			Console.WriteLine("#### " + text + " ####");
			Console.WriteLine("#");
		}
		
		public static void WriteLine(string text)
		{
			Console.WriteLine("#    " + text);
			Console.WriteLine("#");
		}
		
		public static void WriteInfoLine(string text)
		{
			Console.WriteLine("#    * " + text);
			Console.WriteLine("#");
		}
		
	}
}
