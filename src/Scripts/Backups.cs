using System;
using Dim.Database;

namespace Dim.Scripts
{
	public static class Backups
	{
		public static void SaveFile(bool dryRun, string filePath = null, Action<string> completedCallback = null)
		{
			
			if(string.IsNullOrEmpty(filePath))
			{
				var universalNow = DateTime.Now.ToUniversalTime();
				filePath = string.Format("{0}\\{1}-{2}.sql",
			                         Config.Settings.LocalBackupsDir,
			                         universalNow.ToString("yyyyMMdd"),
			                         universalNow.Ticks.ToString());
			}
			
			if(!dryRun)
			{
				using(var db = new DatabaseCommander())
				{
					db.DumpBackup(filePath);
				}
			}
			
			if(completedCallback != null)
				completedCallback(filePath);
		}
	}
}
