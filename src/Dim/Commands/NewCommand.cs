﻿using System;
using System.IO;
using Dim.Library;
using ManyConsole;

namespace Dim.Commands
{
	public class NewCommand : DimCommand
	{
		public NewCommand()
		{
			base.IsCommand("new", "Create a new script for updating your database");
			this.HasOption("d|desc=", "desc", d => this.Desc = d);
		}
		
		private string Desc = string.Empty;
		
		public override int Run(string[] remainingArguments)
		{
			if(!Program.IsCorrectlySetup()) return 0;
			
			DimConsole.WriteIntro("Creating a new file");
			var universalNow = DateTime.Now.ToUniversalTime();
			
			var fileName = "{0}-{1}.sql";
			if(!string.IsNullOrEmpty(this.Desc))
				fileName = "{0}-{1}-{2}.sql";
			
			fileName = string.Format(fileName, universalNow.ToString("yyyyMMdd"), universalNow.Ticks.ToString(), this.Desc);
			
			if(!base.DryRun)
				File.Create(Local.ConfigFile.Patches.GetFullPath() + @"\" + fileName);
			
			DimConsole.WriteLine("A new file has been created for you to use. ",
			                    fileName);
			
			// TODO Allow select folder, and if RunOnce then say "Don't edit after you have shared it with others."
			
			return 0;
		}
	}
}
