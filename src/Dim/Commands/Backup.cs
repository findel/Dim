using System;

using Dim.Library;

namespace Dim.Commands
{
	public class Backup : DimCommand
	{
		public Backup()
		{
			base.IsCommand("backup", "Do a complete backup of the database");
			base.HasOption("f|filepath=",
			               "Optional file path to save backup file. Otherwise new file will be created in the 'backups' directory.",
			               p => this.FilePath = p);
		}
		
		private string FilePath { get; set; }
		
		public override int Run(string[] remainingArguments)
		{
			if(!Program.IsCorrectlySetup()) return 0;
			
			DimConsole.WriteIntro("Running a complete backup");
			//CreateBackup(base.DryRun, this.FilePath);
			Backups.SaveFile(base.DryRun, this.FilePath, delegate(string filePath)
			{
				DimConsole.WriteLine("Backup completed:", filePath);
			});
			return 0;
		}
		
	}
}
