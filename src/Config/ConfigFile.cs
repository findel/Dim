using System;
using System.IO;

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
	}
}
