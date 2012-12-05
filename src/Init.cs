using System;
using System.IO;
using System.Threading;
using ManyConsole;

namespace Dim
{
	public class Init : DimCommand
	{
		public Init()
		{
			base.IsCommand("init", "Initialise a new Dim project");
		}
		
		public override int Run(string[] remainingArguments)
		{
			DimConsole.WriteIntro("Initialising a new Dim project.");
			
			if(!File.Exists(Settings.LocalDimConfig))
			{
				File.Create(Settings.LocalDimConfig);
				DimConsole.WriteLine("New dim.config file created.", Settings.LocalDimConfig);
			}
			
			var loadDimDir = Settings.LocalDimDirectory;
			DimConsole.WriteInfoLine("Tell your version control software to ignore the .dim directory.");
			var loadLocalBackup = Settings.LocalBackupsDir;
			var loadLocalUpdate = Settings.LocalPatchesDir;
			var loadUpdates = Settings.SharedPatchesDir;
			var loadRoutinesDir = Settings.SharedRoutinesDir;
			var loadBaselineDir = Settings.SharedBaselineDir;
			
			DimConsole.WriteLine("New project initialised!");
			
			return 0;
		}
	}
}
