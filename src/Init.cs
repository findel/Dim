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
			
			var loadDimDir = Settings.DimDirectory;
			DimConsole.WriteInfoLine("Tell your version control software to ignore the .dim directory.");
			var loadLocalBackup = Settings.LocalBackupsDir;
			var loadLocalUpdate = Settings.LocalUpdatesDir;
			var loadUpdates = Settings.UpdatesDir;
			var loadRoutinesDir = Settings.RoutinesDir;
			var loadBaselineDir = Settings.BaselineDir;
			
			DimConsole.WriteLine("New project initialised!");
			
			return 0;
		}
	}
}
