using System;

namespace Dim.Library
{
	public interface IDatabaseCommander
	{
		bool IsConnectionOkay();
		
		void DumpRoutines(string filePath);
		
		bool DimLogExists();
		
		void RunCreateDimLog();
		
		string ExceptionMessage { get; }
		
		void DumpStructure(string filePath);
		
		void DumpData(string filePath);
	}
}
