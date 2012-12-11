using System;
using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dim.Config
{
	public class DimFolder
	{
		public DimFolder(){}
		
		public string Path { get; set; }
		
		[JsonConverter(typeof(StringEnumConverter))]
		public RunKind RunKind { get; set; }
		
		public string GetFullPath()
		{
			var dir = System.Environment.CurrentDirectory + this.Path;
			Local.CreateDir(dir);
			return dir;
		}
	}
}
