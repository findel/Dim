using System;
using System.Collections.Generic;
using System.Linq;

using Dim.Config;
using Dim.Database;
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
				
				if(!Local.LocalConfigExists)
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
						var config = Local.ConfigFile;
					}
					catch (Exception ex)
					{
						DimConsole.WriteIntro("Config could not load!");
						DimConsole.WriteInfoLine("The config file could not be loaded.");
						DimConsole.WriteErrorLine(ex.InnerException.GetType().ToString());
						DimConsole.WriteErrorLine(ex.InnerException.Message);
						correct = false;
					}
					
					// Check that credentials can connect.
					using(var comm = new DatabaseCommander())
					{
						if(!comm.IsConnectionOkay())
						{
							DimConsole.WriteIntro("Database connection failed!");
							DimConsole.WriteInfoLine("The database could not be connected to using the provided details.");
							DimConsole.WriteErrorLine(comm.MySqlException.Message);
							correct = false;
						}
					}
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