using System;
using System.IO;

namespace Dim
{
	public static class Settings
	{
		
		internal static string MySqlBinPath
		{
			get { return @"C:\Program Files\MySQL\MySQL Server 5.5\bin"; }
		}
		
		internal static string MySqlUserName
		{
			get { return "dim"; }
		}
		
		internal static string MySqlPassword
		{
			get { return "dim456"; }
		}
		
		internal static string MySqlSchemaName
		{
			get { return "dim-tests"; }
		}
		
		internal static string DimDirectory
		{
			get
			{
				var dir = System.Environment.CurrentDirectory + @"\.dim";
				CreateDir(dir, FileAttributes.Directory | FileAttributes.Hidden);
				return dir;
			}
		}
		
		internal static string UpdatesDir
		{
			get 
			{ 
				var dir = System.Environment.CurrentDirectory + @"\dim-updates";
				CreateDir(dir);
				return dir;
			}
		}
		
		internal static string RoutinesDir
		{
			get 
			{ 
				var dir = System.Environment.CurrentDirectory + @"\dim-routines";
				CreateDir(dir);
				return dir;
			}
		}
		
		internal static string BaselineDir
		{
			get 
			{ 
				var dir = System.Environment.CurrentDirectory + @"\dim-baseline";
				CreateDir(dir);
				return dir;
			}
		}
		
		internal static string LocalBackupsDir
		{
			get
			{ 
				var dir = DimDirectory + @"\backups"; 
				CreateDir(dir);
				return dir;
			}
		}
		
		internal static string LocalUpdatesDir
		{
			get
			{
				var dir = DimDirectory + @"\updates";
				CreateDir(dir);
				return dir;
			}
		}
		
		private static void CreateDir(string dir, FileAttributes att = FileAttributes.Directory)
		{
			if(!Directory.Exists(dir))
			{
				var dirInfo = Directory.CreateDirectory(dir);
				dirInfo.Attributes = att;
				DimConsole.WriteInfoLine("New directory created: " + dir.Replace(System.Environment.CurrentDirectory, ""));
			}
		}
	}
}
