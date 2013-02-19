using System;

namespace Dim.Library
{
	public interface IDatabaseManager
	{
		
		void Execute(string statement);
		
		string DumpSchema();
		
	}
}
