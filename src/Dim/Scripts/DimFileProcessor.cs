using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Dim.Config;
using Dim.Database;

namespace Dim.Scripts
{
	public static class DimFileProcessor
	{
		public static List<DimFile> GetAllFiles()
		{
			List<DimFolder> folders = new List<DimFolder>();
			
			if(Local.ConfigFile.Patches != null)
				folders.Add(Local.ConfigFile.Patches);
			
			if(Local.ConfigFile.Routines != null)
				folders.Add(Local.ConfigFile.Routines);
			
			if(Local.ConfigFile.CustomFolders != null 
			   && Local.ConfigFile.CustomFolders.Count > 0)
				folders.AddRange(Local.ConfigFile.CustomFolders);
			
			var files = new List<DimFile>();
			
			foreach(var folder in folders)
			{
				files.AddRange(from f in Directory.GetFiles(folder.GetFullPath())
				                  where Path.GetExtension(f).ToLower() == ".sql"
				                  select new DimFile(folder, f));
			}
			
			return files;
			
		}
		
		public static List<DimFile> GetRunFiles()
		{
			var allFiles = GetAllFiles();
			
			var allRecords = DatabaseCommander.GetAllRecords();
			
			var files = (from file in allFiles
				where allRecords.Select(f => f.FileName == file.FileName).Count() == 0
				select file).ToList();
			
			return files;
		}
		
	}
}
