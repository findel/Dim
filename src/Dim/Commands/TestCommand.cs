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
			
			DimConsole.WriteLine("Read from DB");
			
			var files = DatabaseCommander.GetAllRecords();
			foreach (var file in files)
			{
				DimConsole.WriteInfoLine(file.FileName, file.FileHash, file.Executed.ToString("dd-MMM-yyy hh:mm:ss"));
			}
			
			DimConsole.WriteLine("Read from FS");
			
			var fsFiles = DimFileProcessor.GetAllFiles();
			foreach(var file in fsFiles)
			{
				DimConsole.WriteInfoLine(file.FileName, file.Parent.RunKind.ToString());
			}
			
			DimConsole.WriteLine("Only \"run\" files");
			
			var runFiles  = DimFileProcessor.GetRunFiles();
			foreach(var file in fsFiles)
			{
				DimConsole.WriteInfoLine(file.FileName, file.Parent.RunKind.ToString());
			}
			
			DimConsole.WriteLine("Completed test");
			
			return 0;
			
		}
	}
}
