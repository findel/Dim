using System;
using System.IO;
using Dim.Library;

namespace Dim.Commands
{
	public class StatusCommand : DimCommand
	{
		public StatusCommand()
		{
			base.IsCommand("status", "View the current status, lists any scripts ready to run");
		}
		
		public override int Run(string[] remainingArguments)
		{
			if(!Program.IsCorrectlySetup()) return 0;
			
			DimConsole.WriteIntro("Checking status against the database");
			
			base.CheckRecordsTableExists();
			
			var runFiles = DimFileProcessor.GetRunFiles();
			
			if(runFiles.Count > 0)
			{
				// Display count of files found.
				DimConsole.WriteLine(string.Format("{0} script(s) found to be run.", runFiles.Count));
				
				// Display names of files found.
				for(int i = 0, l = runFiles.Count; i < l; i++)
				{
					int count = i + 1;
					var fileName = Path.GetFileName(runFiles[i].FileName);
					DimConsole.WriteLine(string.Format("{0}. \"{1}\"", count, fileName));
				}
			}
			else
			{
				DimConsole.WriteLine("No scripts found to be run.");
			}
			
			return 0;
			
		}
	}
}
