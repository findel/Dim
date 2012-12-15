
using System;

namespace Dim.Scripts
{
	public class DimFile
	{
		public DimFile(){}
		
		public string FileName { get; set; }
		
		public string FileHash { get; set; }
		
		public DateTime Executed { get; set; }
	}
}
