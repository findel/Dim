using System;
using System.Collections.Generic;
using System.Linq;
using ManyConsole;

namespace Dim
{
	class Program
	{
		private static int Main(string[] args)
		{
			// locate any commands in the assembly (or use an IoC container, or whatever source)
			var commands = GetCommands();
			
			// run the command for the console input
			return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
		}
		
		public static bool IsCorrectlySetup
		{
			get
			{
				bool correct = true;
				
				if(!Config.Local.LocalConfigExists)
				{
					DimConsole.WriteIntro("DIM project not found!");
					DimConsole.WriteInfoLine("This directory is not setup correctly for Dim. The 'dim.config' file is missing.");
					DimConsole.WriteInfoLine("Run 'dim init' to create a new Dim project here.");
					correct = false;
				}
				
				return correct;
			}
		}
		
		private static IEnumerable<ConsoleCommand> GetCommands()
		{
			return ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
		}

	}
}