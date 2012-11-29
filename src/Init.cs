using System;
using System.IO;
using System.Threading;
using ManyConsole;

namespace Dim
{
	public class Init : DimCommand
	{
		public Init()
		{
			base.IsCommand("init", "Initialise a new Dim project");
		}
		
		public override int Run(string[] remainingArguments)
		{
			DimConsole.WriteIntro("Initialising a new Dim project.");
			
//			if(!Directory.Exists(DimDirectory))
//			{
//				var directory = Directory.CreateDirectory(DimDirectory);
//				directory.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
//				Directory.CreateDirectory(DimExecutedUpdatesDir);
//				Console.WriteLine("#\tNew .dim directory created: " + DimDirectory);
//			}
//			else 
//				Console.WriteLine("#\t.dim directory already exists: " + DimDirectory);
//			
//			if(!Directory.Exists(Settings.UpdatesDir))
//			{
//				Directory.CreateDirectory(Settings.UpdatesDir);
//				Console.WriteLine("#\tNew dim-updates directory created: " + Settings.UpdatesDir);
//			}
//			else 
//				Console.WriteLine("#\tThe dim-updates directory already exists: " + Settings.UpdatesDir);
			
			DimConsole.WriteLine("New project initialised!");
			
			return 0;
		}
	}
}
