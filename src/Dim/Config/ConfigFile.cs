using System;
using System.Collections.Generic;
using System.IO;

namespace Dim.Config
{
	public class ConfigFile
	{
		public ConfigFile(){}
		
		public ConfigFile(bool defaultSettings = false)
		{
			if(defaultSettings)
			{
				this.MySqlPath = @"C:\Program Files\MySQL\MySQL Server 5.5\bin";
				
				this.Patches = new DimFolder()
				{
					Path = @"\dim\patches",
					RunKind = RunKind.RunOnce
				};
				
				this.Routines = new DimFolder()
				{
					Path = @"\dim\routines",
					RunKind = RunKind.RunIfChanged
				};
				
				this.Baseline = new DimFolder()
				{
					Path = @"\dim\baseline",
					RunKind = RunKind.None
				};
				
				var viewsFolder = new DimFolder()
				{
					Path = @"\dim\views",
					RunKind = RunKind.RunAlways
				};
				
				var lookupFolder = new DimFolder()
				{
					Path = @"\dim\lookup-data",
					RunKind = RunKind.RunIfChanged
				};
				
				this.CustomFolders = new List<DimFolder>();
				this.CustomFolders.Add(viewsFolder);
				this.CustomFolders.Add(lookupFolder);
			}
		}
		
		public string Host { get; set; }
		
		public string Port { get; set; }
		
		public string Username { get; set; }
		
		public string Password { get; set; }
		
		public string Schema { get; set; }
		
		public string MySqlPath { get; set; }
		
		public DimFolder Patches { get; set; }
		
		public DimFolder Routines { get; set; }
		
		public DimFolder Baseline { get; set; }
		
		public List<DimFolder> CustomFolders { get; set; }
	}
}
