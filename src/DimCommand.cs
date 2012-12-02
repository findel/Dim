
using System;
using System.IO;
using ManyConsole;

namespace Dim
{
	public abstract class DimCommand : ConsoleCommand
	{
		public DimCommand()
		{
			base.HasOption("dry-run", "Do a dry run; this will show the output without actually doing anything.", b => this.DryRun = true);
			this.LocalConfigNotFound = !File.Exists(Settings.LocalDimConfig);
		}
		
		protected bool DryRun { get; set; }
		
		protected bool LocalConfigNotFound { get; set; }
		
		protected int RunDimInit()
		{
			DimConsole.WriteIntro("Dim project not found!");
			DimConsole.WriteInfoLine("This directory is not setup correctly for Dim. The 'dim.config' file is missing.");
			DimConsole.WriteInfoLine("Run 'dim init' to create a new Dim project here.");
			return 0;
		}
	}
}
