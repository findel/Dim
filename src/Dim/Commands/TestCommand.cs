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
			
			var filesOnRecord = DatabaseCommander.GetAllRecords();
			DimConsole.WriteLine("Read from DB (count: " + filesOnRecord.Count + ")");
			foreach (var file in filesOnRecord)
			{
				DimConsole.WriteInfoLine("'" + file.FileName + "'", file.FileHash, file.Executed.ToString("dd-MMM-yyy hh:mm:ss"));
			}
			
			var filesOnSystem = DimFileProcessor.GetAllFiles();
			DimConsole.WriteLine("Read from FS (count : " + filesOnSystem.Count + ")");
			foreach(var file in filesOnSystem)
			{
				var hash = DimFileProcessor.GetFileHash(file);
				DimConsole.WriteInfoLine("'" + file.FileName + "'", file.Parent.RunKind.ToString(), hash);
			}
			
			var filesToRun  = DimFileProcessor.GetRunFiles();
			DimConsole.WriteLine("Only \"run\" files (count: " + filesToRun.Count + ")");
			foreach(var file in filesToRun)
			{
				var hash = DimFileProcessor.GetFileHash(file);
				DimConsole.WriteInfoLine("'" + file.FileName + "'", file.Parent.RunKind.ToString(), hash);
			}
			
			DimConsole.WriteLine("Completed test");
			
			return 0;
		}
	}
}
