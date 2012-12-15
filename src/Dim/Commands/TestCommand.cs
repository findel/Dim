using System;
using System.Collections.Generic;
using Dim.Database;
using Dim.Scripts;

namespace Dim.Commands
{
	public class TestCommand : DimCommand
	{
		public TestCommand()
		{
			base.IsCommand("test", "Run a test. This is for development only");
		}
		
		public override int Run(string[] remainingArguments)
		{
			DimConsole.WriteIntro("Run a test");
			
			using(var commander = new DatabaseCommander())
			{
				var files = commander.GetDatabaseRecords();
				
				
				foreach (var file in files)
				{
					DimConsole.WriteInfoLine(file.FileName, file.FileHash, file.Executed.ToString("dd-MMM-yyy hh:mm:ss"));
				}
				
				
			}
			
			DimConsole.WriteLine("Completed test");
			
			return 0;
			
		}
	}
}
