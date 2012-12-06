using System;
using System.IO;
using Dim.Scripts;

namespace Dim
{
	public class Routines : DimCommand
	{
		public Routines()
		{
			base.IsCommand("routines", "Save or execute the routines for your database");
			
			base.HasOption("s|save",
			               "Save/create a new routines script, from the routines in your database.",
			               x => this.IsSaving = true);
			
			base.HasOption("e|execute",
			               "Execute any existing routines script. This will update your database.",
			               x => this.IsExecuting = true);
		}
		
		private bool IsSaving { get; set; }
		private bool IsExecuting { get; set; }
		
		public override int Run(string[] remainingArguments)
		{
			if(!Program.IsCorrectlySetup) return 0;
			
			var routinesFile = Settings.SharedRoutinesDir + "\\routines.sql";
			
			if(this.IsSaving)
			{
				this.Save(routinesFile);
			}
			else if(this.IsExecuting)
			{
				this.Execute(routinesFile);
			}
			else
			{
				throw new ManyConsole.ConsoleHelpAsException("Neither \"save\" or \"execute\" was chosen");
			}
			
			return 0;
		}
		
		private void Save(string routinesFile)
		{
			DimConsole.WriteIntro("Saving a new routines script.");

			if(!this.DryRun)
			{
				using(var db = new DatabaseCommander())
				{
					db.DumpRoutines(routinesFile);
				}
			}

			DimConsole.WriteLine("Routines Saved!", routinesFile);
		}
		
		private void Execute(string routinesFile)
		{
			DimConsole.WriteIntro("Executing the current routines script.");
			if(File.Exists(routinesFile))
			{
				DimConsole.WriteLine("Backing up existing database first.");
				
				Backups.SaveFile(base.DryRun, completedCallback: delegate(string filePath)
				{
					DimConsole.WriteLine("Backup complete:", filePath);
				});

				if(!base.DryRun)
				{
					using(var db = new DatabaseCommander())
					{
						db.RunFile(routinesFile);
					}
				}

				DimConsole.WriteLine("Routines script executed!");

			}
			else
			{
				DimConsole.WriteLine("routines.sql file does not exist.");
			}
		}
		
	}
}
