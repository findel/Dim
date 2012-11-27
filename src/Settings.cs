using System;

namespace Dim
{
	public static class Settings
	{
		
		internal static string MySqlBinPath
		{
			get
			{
				return @"C:\Program Files\MySQL\MySQL Server 5.5\bin";
			}
		}
		
		internal static string MySqlUserName 
		{
			get
			{
				return "dim";
			}
		}
		
		internal static string MySqlPassword
		{
			get
			{
				return "dim456";
			}
		}
		
		internal static string MySqlSchemaName
		{
			get
			{
				return "dim-tests";
			}
		}
	}
}
