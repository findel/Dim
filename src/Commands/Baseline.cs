using System;
using System.IO;
using Dim.Database;
using Dim.Scripts;

namespace Dim.Commands
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
		
		private string structureFileName = Settings.SharedBaselineDir + "\\structure.sql";
		private string dataFileName = Settings.SharedBaselineDir + "\\data.sql";
		private string routinesFileName = Settings.SharedRoutinesDir + "\\routines.sql";
		
		public override int Run(string[] remainingArguments)
		{
			if(!Program.IsCorrectlySetup) return 0;
			
			if(this.IsSaving)
			{
				this.Save();
			}
			else if(this.IsExecuting)
			{
				this.Execute();
			}
			else
			{
				throw new ManyConsole.ConsoleHelpAsException("Neither \"save\" or \"execute\" was chosen");
			}
			
			return 0;
		}
		
		private void Save()
		{
			DimConsole.WriteIntro("Saving a new baseline script.");

			if(!this.DryRun)
			{
				using(var db = new DatabaseCommander())
				{
					db.DumpStructure(this.structureFileName);
					db.DumpData(this.dataFileName);
					db.DumpRoutines(this.routinesFileName);
				}
			}

			DimConsole.WriteLine("Structure file:", this.structureFileName);
			DimConsole.WriteLine("Data file:", this.dataFileName);
			DimConsole.WriteLine("Routines file:", this.routinesFileName);
			
			DimConsole.WriteLine("Saved! Now you can share changes with others.");
		}
		
		private void Execute()
		{
			DimConsole.WriteIntro("Executing the current baseline script.");
			if(File.Exists(this.structureFileName) && File.Exists(this.dataFileName) && File.Exists(this.routinesFileName))
			{
				
				DimConsole.WriteLine("Backing up existing database first.");
				Backups.SaveFile(base.DryRun, completedCallback: delegate(string filePath)
				{
					DimConsole.WriteLine("Backup completed:", filePath);
				});

				if(!base.DryRun)
				{
					using(var db = new DatabaseCommander())
					{
						db.RunFile(this.structureFileName);
						db.RunFile(this.dataFileName);
						db.RunFile(this.routinesFileName);
					}
				}

				DimConsole.WriteLine("Baseline script executed!");

			}
			else
			{
				DimConsole.WriteLine("One of the required baseline files does not exist.");
			}
		}
		
	}
}
