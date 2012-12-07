
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dim.Config
{
	public static class Local
	{
		public static bool LocalConfigExists
		{
			get
			{
				return File.Exists(Settings.LocalDimConfig);
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
				using (var streamReader = new StreamReader(Settings.LocalDimConfig))
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
			if(!Local.LocalConfigExists)
			{
				var configString = JsonConvert.SerializeObject(file, Formatting.Indented);
				File.WriteAllText(Settings.LocalDimConfig, configString);
			}
		}
		
	}
}
