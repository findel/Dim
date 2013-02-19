using System;
using System.IO;

using Dim.Library;

namespace Dim.Commands
{
	public class ConfigCommand : DimCommand
	{
		public ConfigCommand()
		{
			base.IsCommand("config", "Edit settings in your Dim config file");
			
			base.HasOption("h|host=",
			               "The host (name or i.p. address) for your database",
			               delegate(string host)
			               {
			               	this.Host = host;
			               	this.IsSettingValues = true;
			               });
			
			base.HasOption("P|port=",
			               "The port for you database",
			               delegate(string port)
			               {
			               	this.Port = port;
			               	this.IsSettingValues = true;
			               });
			
			base.HasOption("u|username=",
			               "The username to connect to your database.",
			               delegate(string username)
			               {
			               	this.Username = username;
			               	this.IsSettingValues = true;
			               });
			
			base.HasOption("p|password=",
			               "The password to connect to your database.",
			               delegate(string password)
			               {
			               	this.Password = password;
			               	this.IsSettingValues = true;
			               });
			
			base.HasOption("s|schema=",
			               "The database schema to work with",
			               delegate(string schema)
			               {
			               	this.Schema = schema;
			               	this.IsSettingValues = true;
			               });
			
			base.HasOption("mysqlpath=",
			               "The path to the MySql installation directory (bin)",
			               delegate(string path)
			               {
			               	this.MySqlPath = path;
			               	this.IsSettingValues = true;
			               });
			
		}
		
		
		private bool IsSettingValues { get; set; }
		
		private string Host { get; set; }
		private string Port { get; set; }
		private string Username { get; set; }
		private string Password { get; set; }
		private string Schema { get; set; }
		private string MySqlPath { get; set; }
		
		public override int Run(string[] remainingArguments)
		{
			if(!Program.IsCorrectlySetup(checkConnection:false)) return 0;
			
			if(this.IsSettingValues)
			{
				this.SettingValues();
			}
			else
			{
				DimConsole.WriteIntro("Showing config");
				
				foreach (var line in File.ReadAllLines(Local.LocalDimConfig))
				{
					DimConsole.WriteLine(line);
				}
			}
			
			return 0;
			
		}

		private void SettingValues()
		{
			DimConsole.WriteIntro("Setting fields in the local config file");

			var config = Local.ConfigFile;

			if(!string.IsNullOrEmpty(this.Host))
			{
				config.Host = this.Host;
				DimConsole.WriteLine(string.Format("'Host' has been set to '{0}'", config.Host));
			}

			if(!string.IsNullOrEmpty(this.Port))
			{
				config.Port = this.Port;
				DimConsole.WriteLine(string.Format("'Port' has been set to '{0}'", config.Port));
			}

			if(!string.IsNullOrEmpty(this.Username))
			{
				config.Username = this.Username;
				DimConsole.WriteLine(string.Format("'Username' has been set to '{0}'", config.Username));
			}

			if(!string.IsNullOrEmpty(this.Password))
			{
				config.Password = this.Password;
				DimConsole.WriteLine(string.Format("'Password' has been set to '{0}'", config.Password));
			}

			if(!string.IsNullOrEmpty(this.Schema)) 
			{
				config.Schema = this.Schema;
				DimConsole.WriteLine(string.Format("'Schema' has been set to '{0}'", config.Schema));
			}

			if(!string.IsNullOrEmpty(this.MySqlPath)) 
			{
				config.MySqlPath = this.MySqlPath;
				DimConsole.WriteLine(string.Format("'MySqlPath' has been set to '{0}'", config.MySqlPath));
			}

			if(!base.DryRun)
				Local.SaveConfig(config);
		}
		
	}
}
