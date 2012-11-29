using System;
using System.IO;
using ManyConsole;

namespace Dim
{
	public class NewScript : DimCommand
	{
		public NewScript()
		{
			base.IsCommand("new", "Create a new script for updating your database");
			this.HasOption("d|desc=", "desc", d => this.Desc = d);
		}
		
		private string Desc = string.Empty;
		
		public override int Run(string[] remainingArguments)
		{
			DimConsole.WriteIntro("Creating a new file");
			var universalNow = DateTime.Now.ToUniversalTime();
			
			var fileName = "{0}-{1}.sql";
			if(!string.IsNullOrEmpty(this.Desc))
				fileName = "{0}-{1}-{2}.sql";
			
			fileName = string.Format(fileName, universalNow.ToString("yyyyMMdd"), universalNow.Ticks.ToString(), this.Desc);
			
			if(!base.DryRun)
				File.Create(Settings.UpdatesDir + @"\" + fileName);
			
			DimConsole.WriteLine("A new file has been created for you to use. Don't edit after you have shared it with others.");
			DimConsole.WriteLine("New file: " + fileName);
			
			return 0;
		}
	}
}
