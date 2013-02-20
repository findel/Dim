
using System;

namespace Dim.Library
{
	/// <summary>
	/// Use this static class to access the correct implementations of interfaces for the current database provider.
	/// </summary>
	public static class DatabaseProvider
	{
		public static IRecordRepository RecordRepository { get; set; }
		
		public static IDatabaseManager Manager { get; set; }
		
		// HACK Temp Interface
		public static IDatabaseCommander Commander { get; set; }
	}
}
