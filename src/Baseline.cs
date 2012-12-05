using System;
using System.IO;

namespace Dim
{
	public class Baseline : DimCommand
	{
		public Baseline()
		{
			base.IsCommand("baseline", "Save or execute a \"baseline\" script; used to create a database from scratch");
			
			base.HasOption("s|save",
			               "Save a new baseline script. Make sure you're happy with the existing data.",
			               x => this.IsSaving = true);
			
			base.HasOption("e|execute",
			               "Execute the existing baseline script. This will replace any existing database.",
			               x => this.IsExecuting = true);
		}
		
		private bool IsSaving { get; set; }
		private bool IsExecuting { get; set; }
		
		public override int Run(string[] remainingArguments)
		{
			if(!Program.IsCorrectlySetup) return 0;
			
			var baselineFile = Settings.SharedBaselineDir + "\\baseline.sql";
			
			if(this.IsSaving)
			{
				this.Save(baselineFile);
			}
			else if(this.IsExecuting)
			{
				this.Execute(baselineFile);
			}
			else
			{
				throw new ManyConsole.ConsoleHelpAsException("Neither \"save\" or \"execute\" was chosen");
			}
			
			return 0;
		}
		
		private void Save(string baselineFile)
		{
			DimConsole.WriteIntro("Saving a new baseline script.");

			if(!this.DryRun)
			{
				using(var db = new DatabaseCommander())
				{
					db.Dump(baselineFile);
				}
			}

			DimConsole.WriteLine("Saved! Now you can share this with others.", baselineFile);
		}
		
		private void Execute(string baselineFile)
		{
			DimConsole.WriteIntro("Executing the current baseline script.");
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

				DimConsole.WriteLine("Baseline script executed!");

			}
			else
			{
				DimConsole.WriteLine("baseline.sql file does not exist.");
			}
		}
		
	}
}
