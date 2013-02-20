using System;
using System.IO;

namespace Dim.Library
{
	public static class Backups
	{
		
		public static void SaveFile(bool dryRun, string filePath = null, Action<string> completedCallback = null)
		{
			
			if(string.IsNullOrEmpty(filePath))
			{
				var universalNow = DateTime.Now.ToUniversalTime();
				filePath = string.Format("{0}\\{1}-{2}-{3}.sql",
			                         Local.LocalBackupsDir,
			                         Local.ConfigFile.Schema,
			                         universalNow.ToString("yyyyMMdd"),
			                         universalNow.Ticks.ToString());
			}
			
			if(!dryRun)
			{
				File.WriteAllText(filePath, DatabaseProvider.Manager.DumpSchema());
			}
			
			if(completedCallback != null)
				completedCallback(filePath);
		}
	}
}
