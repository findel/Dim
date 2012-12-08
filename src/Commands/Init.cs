using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Dim.Config;
using ManyConsole;

namespace Dim.Commands
{
	public class Init : DimCommand
	{
		public Init()
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
			
			if(!File.Exists(Settings.LocalDimConfig))
			{
				
				var config = new ConfigFile()
				{
					Host = this.Host,
					Port = this.Port,
					Username = this.Username,
					Password = this.Password,
					Schema = this.Schema,
					Patches = new DimFolder()
					{
						Path = "\\dim-patches",
						RunKind = RunKind.RunOnce
					},
					Routines = new DimFolder()
					{
						Path = "\\dim-routines",
						RunKind = RunKind.RunIfChanged
					},
					Baseline = new DimFolder()
					{
						Path = "\\dim-baseline",
						RunKind = RunKind.None
					}
				};
				
//				var viewsFolder = new DimFolder()
//				{
//					Path = "\\dim-views",
//					RunKind = RunKind.RunAlways
//				};
//				
//				var lookupFolder = new DimFolder()
//				{
//					Path = "\\dim-lookup",
//					RunKind = RunKind.RunIfChanged
//				};
//				config.CustomFolders = new List<DimFolder>();
//				config.CustomFolders.Add(viewsFolder);
//				config.CustomFolders.Add(lookupFolder);
				
				Config.Local.SaveConfig(config);
				
				DimConsole.WriteLine("New config file created.", Settings.LocalDimConfig);
			}
			
			var loadDimDir = Settings.LocalDimDirectory;
			DimConsole.WriteInfoLine("Tell your version control software to ignore the .dim directory.");
			var loadLocalBackup = Settings.LocalBackupsDir;
			var loadLocalUpdate = Settings.LocalPatchesDir;
			var loadUpdates = Settings.SharedPatchesDir;
			var loadRoutinesDir = Settings.SharedRoutinesDir;
			var loadBaselineDir = Settings.SharedBaselineDir;
			
			DimConsole.WriteLine("New project initialised!");
			
			return 0;
		}
	}
}
