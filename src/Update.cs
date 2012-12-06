using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Dim.Scripts;
using ManyConsole;

namespace Dim
{
	public class Update : DimCommand
	{
		public Update()
		{
			base.IsCommand("update", "Update your database with any changes shared by others");
		}
		
		public override int Run(string[] remainingArguments)
		{
			if(!Program.IsCorrectlySetup) return 0;
			
			DimConsole.WriteIntro("Update the local database");
			
			var newPatchFiles = Patches.GetNewPatches();
			
			if(newPatchFiles.Count > 0)
			{
				// Display count of files found.
				DimConsole.WriteLine(string.Format("{0} new patch{1} found.",
				                                   newPatchFiles.Count,
				                                   (newPatchFiles.Count > 0 ? "es" : "")));
				
				// Display names of files found.
				for(int i = 0, l = newPatchFiles.Count; i < l; i++)
				{
					int count = i + 1;
					var fileName = Path.GetFileName(newPatchFiles[i]);
					DimConsole.WriteLine(string.Format("{0}. \"{1}\"", count, fileName));
				}
				
				// Backup before running anything (use for rollback)
				DimConsole.WriteLine("Backing up database (local backup)");
				
				Backups.SaveFile(base.DryRun, completedCallback: delegate(string fileName)
				{
					DimConsole.WriteLine("Completed backup:", fileName);
				});
				
				Patches.ExecuteNewPatches(base.DryRun, delegate(string patchFilePath)
				{
					DimConsole.WriteLine("Executing: \"" + Path.GetFileName(patchFilePath) + "\"");
				});
				
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
