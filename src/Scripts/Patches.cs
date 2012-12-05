using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dim.Scripts
{
	public static class Patches
	{
		
		public static List<string> GetNewPatches()
		{
			var newPatches = (from f in Directory.GetFiles(Settings.SharedPatchesDir)
			                  where Path.GetExtension(f).ToLower() == ".sql"
			                  && !File.Exists(Settings.LocalPatchesDir + @"\" + Path.GetFileName(f))
			                  select f).ToList();
			return newPatches;
		}
		
		public static void RegisterPatchCompleted(string patchFilePath)
		{
			//File.Copy(patchFilePath, Settings.LocalPatchesDir + "\\" + fileName);
			File.Create(Settings.LocalPatchesDir + "\\" + Path.GetFileName(patchFilePath));
		}
		
		public static void ExecuteNewPatches(bool dryRun, Action<string> callBeforeExecute = null)
		{
			var newPatches = GetNewPatches();
			foreach(string filePath in newPatches)
			{
				if(callBeforeExecute != null)
					callBeforeExecute(filePath);
				
				// Execute mysql
				if(!dryRun)
				{
					using(var db = new DatabaseCommander())
					{
						db.RunFile(filePath);
					}
					Patches.RegisterPatchCompleted(filePath);
				}
			}
		}
		
	}
}
