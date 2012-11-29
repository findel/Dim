
using System;
using ManyConsole;

namespace Dim
{
	public abstract class DimCommand : ConsoleCommand
	{
		public DimCommand()
		{
			base.HasOption("dry-run", "Do a dry run; this will show the output without actually doing anything.", b => this.DryRun = true);
		}
		
		protected bool DryRun{ get; set; }
	}
}
