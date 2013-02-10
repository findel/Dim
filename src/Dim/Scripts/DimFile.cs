
using System;
using System.IO;
using Dim.Config;

namespace Dim.Scripts
{
	public class DimFile
	{
		public DimFile(){}
		
		public DimFile(DimFolder folder, string fileName)
		{
			this.Parent = folder;
			this.FileName = Path.GetFileName(fileName);
		}
		
		public int Id { get; set; }
		
		public string FileName { get; set; }
		
		public string FileHash { get; set; }
		
		public DateTime Executed { get; set; }
		
		public DimFolder Parent { get; set; }
		
		public string FilePath
		{
			get
			{
				return this.Parent.GetFullPath() + @"\" + this.FileName;
			}
		}
	}
}
