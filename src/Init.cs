using System;
using System.IO;
using System.Threading;
using ManyConsole;

namespace Dim
{
	public class Init : ConsoleCommand
	{
		public Init()
		{
			base.IsCommand("init", "Initialise a new Dim project");
		}
		
		internal static string DimDirectory
		{
			get
			{
				return System.Environment.CurrentDirectory + @"\.dim";
			}
		}
		
		internal static string DimExecutedUpdatesDir
		{
			get
			{
				var executedUpdatesDir = DimDirectory + @"\dim-updates";
				return executedUpdatesDir;
			}
		}
		
		public override int Run(string[] remainingArguments)
		{
			Console.WriteLine("# Initialising a new Dim project.");
			Console.WriteLine("#\t");
			
			if(!Directory.Exists(DimDirectory))
			{
				var directory = Directory.CreateDirectory(DimDirectory);
				directory.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
				Directory.CreateDirectory(DimExecutedUpdatesDir);
				Console.WriteLine("#\tNew .dim directory created: " + DimDirectory);
			}
			else 
				Console.WriteLine("#\t.dim directory already exists: " + DimDirectory);
			
			if(!Directory.Exists(Update.UpdateDirectory))
			{
				Directory.CreateDirectory(Update.UpdateDirectory);
				Console.WriteLine("#\tNew dim-updates directory created: " + Update.UpdateDirectory);
			}
			else 
				Console.WriteLine("#\tThe dim-updates directory already exists: " + Update.UpdateDirectory);
			
			Console.WriteLine("#\t");
			Console.WriteLine("#\tNew project initialised!");
			Console.WriteLine("#\t");
			return 0;
		}
	}
}
