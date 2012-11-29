using System;
using System.IO;

namespace Dim
{
	public class Baseline : DimCommand
	{
		public Baseline()
		{
			base.IsCommand("baseline", "Create or restore a \"baseline\" script to create a database from scratch");
			
			base.HasOption("c|create",
			               "Create a new baseline script. Make sure you're happy with the existing data.",
			               c => this.IsCreate = true);
			
			base.HasOption("r|restore",
			               "Restore the existing baseline script. This will replace any existing database.",
			               r => this.IsRestore = true);
		}
		
		private bool IsCreate { get; set; }
		private bool IsRestore { get; set; }
		
		public override int Run(string[] remainingArguments)
		{
			var baselineFile = Settings.BaselineDir + "\\baseline.sql";
			
			if(this.IsCreate)
			{
				DimConsole.WriteLine("Creating a new baseline script.");
				
				if(!this.DryRun)
				{
					using(var db = new DatabaseCommander())
					{
						db.Dump(baselineFile);
					}
				}
				
				DimConsole.WriteLine("Baseline created. Now you can share this with others.");
			}
			else if(this.IsRestore)
			{
				DimConsole.WriteLine("Restoring the current baseline.");
				if(File.Exists(baselineFile))
				{
					DimConsole.WriteLine("Backing up existing database first.");
					Backup.CreateBackup(base.DryRun);
					
					if(!base.DryRun)
					{
						using(var db = new DatabaseCommander())
						{
							db.RunFile(baselineFile);
						}
					}
					
					DimConsole.WriteLine("Baseline restored!");
					
				}
				else
				{
					DimConsole.WriteLine("baseline.sql file does not exist.");
				}
			}
			else
			{
				throw new ManyConsole.ConsoleHelpAsException("Neither \"create\" or \"restore\" was chosen");
			}
			return 0;
		}
	}
}
