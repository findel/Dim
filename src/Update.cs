using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ManyConsole;

namespace Dim
{
	public class Update : ConsoleCommand
	{
		public Update()
		{
			base.IsCommand("update", "Update your database with any changed shared by others.");
		}
		
		public static string UpdateDirectory
		{
			get
			{
				return System.Environment.CurrentDirectory + @"\dim-updates";
			}
		}
		
		public override int Run(string[] remainingArguments)
		{
			#region Mock Output
			
//			Console.WriteLine("# 2 migrations scripts found.");
//			Console.WriteLine("#\t'2012110-00000001-new-comments.sql'");
//			Console.WriteLine("#\t'2012110-00000002-change-entries.sql'");
//			Console.WriteLine("#\t");
//			Thread.Sleep(1000);
//			Console.WriteLine("#\tBacking up existing database (local dump).");
//			Thread.Sleep(1000);
//			Thread.Sleep(1000);
//			Thread.Sleep(1000);
//			Console.WriteLine("#\tRunning '2012110-00000001-new-comments.sql'");
//			Thread.Sleep(1000);
//			Thread.Sleep(1000);
//			Thread.Sleep(1000);
//			Console.WriteLine("#\tRunning '2012110-00000002-change-entries.sql'");
//			Thread.Sleep(1000);
//			Thread.Sleep(1000);
//			Thread.Sleep(1000);
//			Console.WriteLine("#\t");
//			Console.WriteLine("#\tUpdate Completed!");
//			Console.WriteLine("#\t");
			
			#endregion
			
			if(Directory.Exists(Update.UpdateDirectory))
			{
				var files = Directory.GetFiles(Update.UpdateDirectory);
				
				var toExecute = new List<string>();
				
				foreach(var file in files)
				{
					var fileName = Path.GetFileName(file);
					var executedPath = Init.DimExecutedUpdatesDir + @"\" + fileName;
					if(!File.Exists(executedPath))
						toExecute.Add(file);
				}
				
				if(toExecute.Count > 0)
				{
					Console.WriteLine("# " + toExecute.Count.ToString() + " migrations scripts found.");
					foreach(var file in toExecute)
					{
						var fileName = Path.GetFileName(file);
							Console.WriteLine("#\t " + fileName);
					}
					
					foreach(var file in toExecute)
					{
						var fileName = Path.GetFileName(file);
						var executedPath = Init.DimExecutedUpdatesDir + @"\" + fileName;
						Console.WriteLine("#\tRunning '" + fileName + "'");
						File.Copy(file, executedPath);
						Console.WriteLine("#\tExecuted '" + fileName + "'");
					}
					
				}
				else
				{
					Console.WriteLine("# No migration files found.");
				}
			}
			
			return 0;
		}
	}
}
