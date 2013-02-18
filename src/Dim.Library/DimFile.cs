using System;
using System.IO;

namespace Dim.Library
{
	public class DimFile
	{
		public DimFile(){}
		
		public DimFile(DimFolder folder, string fileName)
		{
			this.Parent = folder;
			this.FileName = Path.GetFileName(fileName);
		}
		
		public string FileName { get; set; }
		
		public string FileHash { get; set; }
		
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
