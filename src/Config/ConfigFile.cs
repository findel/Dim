using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dim.Config
{
	public class ConfigFile
	{
		public ConfigFile(){}
		
		public string Host { get; set; }
		
		public string Port { get; set; }
		
		public string Username { get; set; }
		
		public string Password { get; set; }
		
		public string Schema { get; set; }
		
		private static ConfigFile instance = CreateInstance();
		
		public static ConfigFile GetInstance()
		{
			return instance;
		}
		
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
		
		public void SaveConfig()
		{
			if(!Local.LocalConfigExists)
			{
				var configString = JsonConvert.SerializeObject(this, Formatting.Indented);
				File.WriteAllText(Settings.LocalDimConfig, configString);
			}
		}
	}
}
