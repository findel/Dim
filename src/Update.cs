using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using ManyConsole;

namespace Dim
{
	public class Update : DimCommand
	{
		public Update()
		{
			base.IsCommand("update", "Update your database with any changed shared by others.");
		}
		
		public override int Run(string[] remainingArguments)
		{
			
			DimConsole.WriteIntro("Checking for scripts to update the database");
			
			var filesToUse = (from f in Directory.GetFiles(Settings.UpdatesDir)
			                  where Path.GetExtension(f).ToLower() == ".sql"
			                  && !File.Exists(Settings.LocalUpdatesDir + @"\" + Path.GetFileName(f))
			                  select f).ToList();
			
			if(filesToUse.Count > 0)
			{
				// Display count of files found.
				DimConsole.WriteLine(string.Format("{0} script{1} found.",
				                                   filesToUse.Count,
				                                   (filesToUse.Count > 0 ? "s" : "")));
				
				// Display names of files found.
				for(int i = 0, l = filesToUse.Count; i < l; i++)
				{
					int count = i + 1;
					var fileName = Path.GetFileName(filesToUse[i]);
					DimConsole.WriteLine(string.Format("{0}. \"{1}\"", count, fileName));
				}
				
				// Backup before running anything (use for rollback)
				DimConsole.WriteLine("Backing up database (local backup)");
				Backup.CreateBackup(base.DryRun);
				
				foreach(var filePath in filesToUse)
				{
					var fileName = Path.GetFileName(filePath);
					
					DimConsole.WriteLine("Running: \"" + fileName + "\"");
					
					// Execute mysql
					if(!base.DryRun)
					{
						using(var db = new DatabaseCommander())
						{
							db.RunFile(filePath);
						}
						File.Copy(filePath, Settings.LocalUpdatesDir + "\\" + fileName);
					}
					DimConsole.WriteLine("Finished.");
				}
				DimConsole.WriteLine("Update Completed.");
			}
			else
			{
				DimConsole.WriteLine("No migration files found.");
			}
			
			
			return 0;
		}


	}
}
