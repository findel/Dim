using System;
using Dim.Config;

namespace Dim.Commands
{
	public class ConfigCommand : DimCommand
	{
		public ConfigCommand()
		{
			base.IsCommand("config", "Edit settings in your Dim config file");
			
			base.HasOption("h|host=",
			               "The host (name or i.p. address) for your database",
			               h => this.Host = h);
			
			base.HasOption("P|port=",
			               "The port for you database",
			               p => this.Port = p);
			
			base.HasOption("u|username=",
			               "The username to connect to your database.",
			               u => this.Username = u);
			
			base.HasOption("p|password=",
			               "The password to connect to your database.",
			               p => this.Password = p);
			
			base.HasOption("s|schema=",
			               "The database schema to work with",
			               s => this.Schema = s);
			
		}
		
		private string Host { get; set; }
		private string Port { get; set; }
		private string Username { get; set; }
		private string Password { get; set; }
		private string Schema { get; set; }
		
		public override int Run(string[] remainingArguments)
		{
			if(!Program.IsCorrectlySetup(checkConnection:false)) return 0;
			
			DimConsole.WriteIntro("Modify your local config file");
			
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
			
			if(!base.DryRun)
				Local.SaveConfig(config);
			
			return 0;
			
		}
		
		
	}
}
