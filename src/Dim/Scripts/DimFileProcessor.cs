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
			var filesOnSystem = GetAllFiles();
			var fileInRecords = DatabaseCommander.GetAllRecords();
			
			// Only return files that are not already in the database records
			List<DimFile> filesToRun = new List<DimFile>();
			
			// Use a loop for now, could it be improved with Linq?
			foreach(var file in filesOnSystem)
			{
				if((from f in (fileInRecords) where f.FileName == file.FileName select f).Count() == 0)
				{
					filesToRun.Add(file);
				}
			}
			
			// TODO Consider not only files not foudn in the records, but their "RunKind" - We're only covering "RunOnce" at the moment.
			
			return filesToRun;
		}
		
	}
}
