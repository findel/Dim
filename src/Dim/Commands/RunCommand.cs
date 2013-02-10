using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Dim.Scripts;
using ManyConsole;

namespace Dim.Commands
{
	public class RunCommand : DimCommand
	{
		public RunCommand()
		{
			base.IsCommand("run", "Run any scripts needed to update your database");
		}
		
		public override int Run(string[] remainingArguments)
		{
			if(!Program.IsCorrectlySetup()) return 0;
			
			DimConsole.WriteIntro("Run updates on the local database");
			
			// Check for the dimfiles
			using(var commander = new Database.DatabaseCommander())
			{
				if(!commander.DimLogExists())
				{
					DimConsole.WriteInfoLine("Required dimfiles table doesn't exist.", "Creating empty table.");
					if(!base.DryRun) commander.RunCreateDimLog();
					DimConsole.WriteInfoLine("New dimfiles table created.");
				}
			}
			
			var runFiles = DimFileProcessor.GetRunFiles();
			
			if(runFiles.Count > 0)
			{
				// Display count of files found.
				DimConsole.WriteLine(string.Format("{0} script(s) found that need to be executed.", runFiles.Count));
				
				// Display names of files found.
				for(int i = 0, l = runFiles.Count; i < l; i++)
				{
					int count = i + 1;
					var fileName = Path.GetFileName(runFiles[i].FileName);
					DimConsole.WriteLine(string.Format("{0}. \"{1}\"", count, fileName));
				}
				
				// Backup before running anything (use for rollback)
				DimConsole.WriteLine("Backing up database (local backup)");
				Backups.SaveFile(base.DryRun, completedCallback: delegate(string fileName)
				{
					DimConsole.WriteLine("Completed backup:", fileName);
				});
				
				// Execute all new patches
				foreach (var file in runFiles)
				{
					DimConsole.WriteLine("Executing: \"" + Path.GetFileName(file.FileName) + "\"");
					DimFileProcessor.ExecuteFile(file, base.DryRun,
					                             successCallback: delegate()
					                             {
					                             	DimConsole.WriteLine("Executed successfully!");
					                             },
					                             failureCallback: delegate(string message)
					                             {
					                             	DimConsole.WriteLine("Execution failed:", message);
					                             });
				}
				
				DimConsole.WriteLine("Update completed.");
			}
			else
			{
				DimConsole.WriteLine("No patch files found.");
			}
			
			
			return 0;
		}


	}
}
