using System;

namespace Dim.Library
{
	public class DimRecord
	{
		public DimRecord() {}
		
		public DimRecord(DimFile file)
		{
			this.FileName = file.FileName;
			this.FileHash = file.FileHash;
		}
		
		public int Id { get; set; }
		
		public string FileName { get; set; }
		
		public string FileHash { get; set; }
		
		public DateTime Executed { get; set; }
	}
}
