
using System;
using System.IO;
using ManyConsole;

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
		
	}
}
