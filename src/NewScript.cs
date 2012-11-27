using System;
using System.IO;
using ManyConsole;

namespace Dim
{
	public class NewScript : ConsoleCommand
	{
		public NewScript()
		{
			base.IsCommand("new", "Create a new script for updating your database.");
			this.HasOption("d|desc=", "desc", d => this.Desc = d);
		}
		
		private string Desc = string.Empty;
		
		public override int Run(string[] remainingArguments)
		{
			var universalNow = DateTime.Now.ToUniversalTime();
			
			var fileName = "{0}-{1}.sql";
			if(!string.IsNullOrEmpty(this.Desc))
				fileName = "{0}-{1}-{2}.sql";
			
			fileName = string.Format(fileName, universalNow.ToString("yyyymmdd"), universalNow.Ticks.ToString(), this.Desc);
			
			File.Create(Update.UpdateDirectory + @"\" + fileName);
			
			Console.WriteLine("# New file created for you to use. Don't edit this after you have shared it with others.");
			Console.WriteLine("#\t New file: " + fileName);
			
			return 0;
		}
	}
}
