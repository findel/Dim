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
					DimConsole.WriteInfoLine("This directory is not setup correctly for Dim. The config file is missing.");
					DimConsole.WriteInfoLine("Run 'dim init' to create a new Dim project here.");
					correct = false;
				}
				else
				{
					try
					{
						var config = Config.Local.ConfigFile;
					}
					catch (Exception ex)
					{
						DimConsole.WriteIntro("Config could not load!");
						DimConsole.WriteInfoLine("The config file could not be loaded.");
						DimConsole.WriteLine(ex.InnerException.GetType().ToString(), ex.InnerException.Message);
						correct = false;
					}
					
					// TODO Check that credentials can connect.
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