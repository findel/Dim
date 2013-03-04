
using System;
using System.IO;
using ManyConsole;

using Dim.Library;

namespace Dim.Commands
{
	public abstract class DimCommand : ConsoleCommand
	{
		public DimCommand()
		{
			base.SkipsCommandSummaryBeforeRunning();
			base.HasOption("dry-run", "Do a dry run; this will show the output without actually doing anything.", b => this.DryRun = true);
		}
		
		protected bool DryRun { get; set; }
		
		protected void CheckRecordsTableExists()
		{
			// Check for the dimfiles
			if (!DatabaseProvider.Commander.DimLogExists())
			{
				DimConsole.WriteInfoLine("Required dimfiles table doesn't exist.", "Creating empty table.");
				if (!this.DryRun) DatabaseProvider.Commander.RunCreateDimLog();
				DimConsole.WriteInfoLine("New dimfiles table created.");
			}
		}
		
	}
}
