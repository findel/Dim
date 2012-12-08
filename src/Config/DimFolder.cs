using System;
using System.Collections.Generic;
using System.IO;

namespace Dim.Config
{
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
}
