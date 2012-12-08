using System;
using System.Collections.Generic;
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
		
		public string MySqlPath { get; set; }
		
		public DimFolder Patches { get; set; }
		
		public DimFolder Routines { get; set; }
		
		public DimFolder Baseline { get; set; }
		
		public List<DimFolder> CustomFolders { get; set; }
	}
	
	public class DimFolder
	{
		public DimFolder(){}
		
		public string Path { get; set; }
		
		public RunKind RunKind { get; set; }
		
		public string GetFullPath()
		{
			var dir = System.Environment.CurrentDirectory + this.Path;
			Local.CreateDir(dir);
			return dir;
		}
	}
	
	public enum RunKind
	{
		None = 0,
		RunOnce = 1,
		RunIfChanged = 2,
		RunAlways = 3
	}
}
