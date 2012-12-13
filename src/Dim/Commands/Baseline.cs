using System;
using System.IO;
using Dim.Config;
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
		
		
		#region Fields
		
		private string _StructureFileName;
		private string _DataFileName;
		private string _RoutinesFileName;
		
		#endregion
		
		#region Properties
		
		private bool IsSaving { get; set; }
		private bool IsExecuting { get; set; }
		
		private string StructureFileName
		{
			get
			{
				if(string.IsNullOrEmpty(_StructureFileName))
					_StructureFileName = Local.ConfigFile.Baseline.GetFullPath() + "\\structure.sql";
				return _StructureFileName;
			}
		}
		private string DataFileName
		{ 
			get
			{
				if(string.IsNullOrEmpty(_DataFileName))
					_DataFileName = Local.ConfigFile.Baseline.GetFullPath() + "\\data.sql";
				return _DataFileName;
			}
		}
		private string RoutinesFileName
		{
			get
			{
				if(string.IsNullOrEmpty(_RoutinesFileName))
					_RoutinesFileName = Local.ConfigFile.Routines.GetFullPath()  + "\\routines.sql";
				return _RoutinesFileName;
			}
		}
		
		#endregion
		

		
		public override int Run(string[] remainingArguments)
		{
			if(!Program.IsCorrectlySetup()) return 0;
			
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
					db.DumpStructure(this.StructureFileName);
					db.DumpData(this.DataFileName);
					db.DumpRoutines(this.RoutinesFileName);
				}
			}

			DimConsole.WriteLine("Structure file:", this.StructureFileName);
			DimConsole.WriteLine("Data file:", this.DataFileName);
			DimConsole.WriteLine("Routines file:", this.RoutinesFileName);
			
			DimConsole.WriteLine("Saved! Now you can share changes with others.");
		}
		
		private void Execute()
		{
			DimConsole.WriteIntro("Executing the current baseline script.");
			if(File.Exists(this.StructureFileName) && File.Exists(this.DataFileName) && File.Exists(this.RoutinesFileName))
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
						db.RunFile(this.StructureFileName);
						db.RunFile(this.DataFileName);
						db.RunFile(this.RoutinesFileName);
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
