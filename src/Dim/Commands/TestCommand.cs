using System;
using Dim.Database;

namespace Dim.Commands
{
	public class TestCommand : DimCommand
	{
		public TestCommand()
		{
			base.IsCommand("test", "Run a test. This is for development only");
		}
		
		public override int Run(string[] remainingArguments)
		{
			DimConsole.WriteIntro("Run a test");
			
			DimConsole.WriteLine("Completed test");
			
			return 0;
			
		}
	}
}
