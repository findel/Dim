
using System;
using System.IO;

namespace Dim.Config
{
	public static class Local
	{
		public static bool LocalConfigExists
		{
			get
			{
				return File.Exists(Settings.LocalDimConfig);
			}
		}
		
		public static ConfigFile ConfigFile
		{
			get
			{
				return ConfigFile.GetInstance();
			}
		}
	}
}
