using System;
using System.IO;
using Dim.Library;

namespace Dim.Commands
{
	public class BaselineCommand : DimCommand
	{
		public BaselineCommand()
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
		
		#endregion
		
		#region Properties
		
		private bool IsSaving { get; set; }
		private bool IsExecuting { get; set; }
		
		private string BaselineFilePath = Local.ConfigFile.Baseline.GetFullPath() + "\\complete.sql";
		
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
				File.WriteAllText(this.BaselineFilePath, DatabaseProvider.Manager.DumpSchema());
			}

			DimConsole.WriteLine("Baseline file saved! Now you can share changes with others.", this.BaselineFilePath);
			
		}
		
		private void Execute()
		{
			DimConsole.WriteIntro("Executing the current baseline script.");
			if(File.Exists(this.BaselineFilePath))
			{
				
				DimConsole.WriteLine("Backing up existing database first.");
				Backups.SaveFile(base.DryRun, completedCallback: delegate(string filePath)
				{
					DimConsole.WriteLine("Backup completed:", filePath);
				});

				if(!base.DryRun)
				{
//					using(var db = new DatabaseCommander())
//					{
//						DatabaseProvider.Manager.Execute(File.ReadAllText(this.StructureFileName));
//						DatabaseProvider.Manager.Execute(File.ReadAllText(this.DataFileName));
//						DatabaseProvider.Manager.Execute(File.ReadAllText(this.RoutinesFileName));
//					}
					DatabaseProvider.Manager.Execute(File.ReadAllText(this.BaselineFilePath));
				}
				
				

				DimConsole.WriteLine("Baseline script executed!");

			}
			else
			{
				DimConsole.WriteLine("The baseline file does not exist.");
			}
		}
		
	}
}
