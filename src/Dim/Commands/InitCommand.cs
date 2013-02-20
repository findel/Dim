using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ManyConsole;

using Dim.Library;

namespace Dim.Commands
{
	public class InitCommand : DimCommand
	{
		public InitCommand()
		{
			base.IsCommand("init", "Initialise a new Dim project");
			
			base.HasRequiredOption("h|host=",
			                       "The host (name or i.p. address) for your database",
			                       h => this.Host = h);
			
			base.HasRequiredOption("P|port=",
			                       "The port for you database",
			                       p => this.Port = p);
			
			base.HasRequiredOption("u|username=",
			                       "The username to connect to your database.",
			                       u => this.Username = u);
			
			base.HasRequiredOption("p|password=",
			                       "The password to connect to your database.",
			                       p => this.Password = p);
			
			base.HasRequiredOption("s|schema=",
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
			DimConsole.WriteIntro("Initialising a new Dim project.");
			
			if(!Local.LocalConfigExists)
			{
				var config = new ConfigFile(defaultSettings: true)
				{
					Host = this.Host,
					Port = this.Port,
					Username = this.Username,
					Password = this.Password,
					Schema = this.Schema
				};
				if(!base.DryRun) Local.SaveConfig(config);
				DimConsole.WriteLine("New config file created.", Local.LocalDimConfig);
			}
			
			DimConsole.WriteLine("New Dim project initialised!");
			
			return 0;
		}
	}
}
