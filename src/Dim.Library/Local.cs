
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dim.Library
{
	public static class Local
	{
		
		internal static string LocalDimDirectory
		{
			get
			{
				var dir = System.Environment.CurrentDirectory + @"\.dim";
				
				CreateDir(dir, FileAttributes.Directory | FileAttributes.Hidden);
				
//				if(CreateDir(dir, FileAttributes.Directory | FileAttributes.Hidden))
//					DimConsole.WriteInfoLine("Tell your version control software to ignore the .dim directory.");
				
				return dir;
			}
		}
		
		internal static string LocalBackupsDir
		{
			get
			{ 
				var dir = LocalDimDirectory + @"\backups"; 
				CreateDir(dir);
				return dir;
			}
		}
		
		// TODO Remove local patches directory - use database instead.
		internal static string LocalPatchesDir
		{
			get
			{
				var dir = LocalDimDirectory + @"\patches";
				CreateDir(dir);
				return dir;
			}
		}
		
		internal static string LocalDimConfig
		{
			get
			{
				return System.Environment.CurrentDirectory + @"\dimconfig.json";
			}
		}
		
		public static bool LocalConfigExists
		{
			get
			{
				return File.Exists(LocalDimConfig);
			}
		}
		
		public static ConfigFile ConfigFile
		{
			get
			{
				return instance;
			}
		}
		
		private static ConfigFile instance = CreateInstance();
		
		private static ConfigFile CreateInstance()
		{
			ConfigFile file = null;
			if(Local.LocalConfigExists)
			{
				using (var streamReader = new StreamReader(Local.LocalDimConfig))
				{
					string configString = streamReader.ReadToEnd();
					//JObject jobject = JObject.Parse(configString);
					file = JsonConvert.DeserializeObject<ConfigFile>(configString);
				}
			}
			return file;
		}
		
		public static void SaveConfig(ConfigFile file)
		{
			var configString = JsonConvert.SerializeObject(file, Formatting.Indented);
			File.WriteAllText(Local.LocalDimConfig, configString);
		}
		
		internal static bool CreateDir(string dir, FileAttributes att = FileAttributes.Directory)
		{
			if(!Directory.Exists(dir))
			{
				var dirInfo = Directory.CreateDirectory(dir);
				dirInfo.Attributes = att;
				//DimConsole.WriteInfoLine("New directory created: " + dir.Replace(System.Environment.CurrentDirectory, ""));
				return true;
			}
			else
				return false;
		}
		
	}
}
