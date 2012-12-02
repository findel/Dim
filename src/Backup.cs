using System;

namespace Dim
{
	public class Backup : DimCommand
	{
		public Backup()
		{
			base.IsCommand("backup", "Do a complete backup of the database");
			base.HasOption("f|filepath=",
			               "Optional file path to save backup file. Otherwise new file will be created in the 'dim-backups' directory.",
			               p => this.FilePath = p);
		}
		
		private string FilePath { get; set; }
		
		public override int Run(string[] remainingArguments)
		{
			if(base.LocalConfigNotFound)
				return base.RunDimInit();
			
			DimConsole.WriteIntro("Running a complete backup");
			CreateBackup(base.DryRun, this.FilePath);
			return 0;
		}
		
		internal static void CreateBackup(bool dryRun, string filePath = null)
		{
			if(string.IsNullOrEmpty(filePath))
			{
				var universalNow = DateTime.Now.ToUniversalTime();
				filePath = string.Format("{0}\\{1}-{2}.sql",
			                         Settings.LocalBackupsDir,
			                         universalNow.ToString("yyyyMMdd"),
			                         universalNow.Ticks.ToString());
			}
			
			if(!dryRun)
			{
				using(var db = new DatabaseCommander())
				{
					db.Dump(filePath);
				}
			}
			
			DimConsole.WriteLine("Backup completed:",  filePath);
		}
		
	}
}
