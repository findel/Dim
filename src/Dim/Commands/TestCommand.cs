using System;
using System.Collections.Generic;

using Dim.Library;

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
			
			var allRecords = DatabaseProvider.RecordRepository.GetAll();
			DimConsole.WriteLine("Read from DB (count: " + allRecords.Count + ")");
			foreach (var record in allRecords)
			{
				DimConsole.WriteInfoLine("'" + record.FileName + "'", record.FileHash, record.Executed.ToString("dd-MMM-yyy hh:mm:ss"));
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
			
//			DimConsole.WriteLine("Find file by FileName:");
//			DimConsole.WriteLine("Please enter a file name:");
//			var fileName = Console.ReadLine();
//			var testFile = DatabaseCommander.GetRecordByFileName(fileName);
//			if(testFile != null)
//				DimConsole.WriteLine("File found!", testFile.FileName, testFile.Executed.ToString("dd-MMM-yyy hh:mm:ss"));
//			else
//				DimConsole.WriteLine("File not found :(");
			
			DimConsole.WriteLine("Completed test");
			
			return 0;
		}
	}
}
