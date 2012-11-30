using System;
using System.Diagnostics;
using System.IO;

namespace Dim
{
	public class DatabaseCommander : IDisposable
	{
		public DatabaseCommander(){}
		
		public void Dump(string filePath)
		{
			var p = new Process();
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardInput = true;
			p.StartInfo.FileName = Settings.MySqlBinPath + @"\mysqldump.exe";
			p.StartInfo.Arguments = string.Format(@"-u{0} -p{1} --routines --comments {2}",
			                                      Settings.MySqlUserName,
			                                      Settings.MySqlPassword,
			                                      Settings.MySqlSchemaName);
			p.Start();
			
			File.WriteAllText(filePath, p.StandardOutput.ReadToEnd());
			
			p.Close();
		}
		
		public void DumpRoutines(string filePath)
		{
			var p = new Process();
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardInput = true;
			p.StartInfo.FileName = Settings.MySqlBinPath + @"\mysqldump.exe";
			p.StartInfo.Arguments = string.Format(@"-u{0} -p{1} --no-data --no-create-info --routines --comments {2}",
			                                      Settings.MySqlUserName,
			                                      Settings.MySqlPassword,
			                                      Settings.MySqlSchemaName);
			p.Start();
			
			File.WriteAllText(filePath, p.StandardOutput.ReadToEnd());
			
			p.Close();
		}
		
		public void RunFile(string filePath)
		{
			var p = new Process();
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardInput = true;
			p.StartInfo.FileName = Settings.MySqlBinPath + "\\mysql.exe";
			p.StartInfo.Arguments = string.Format("-u{0} -p{1} {2}",
			                                      Settings.MySqlUserName,
			                                      Settings.MySqlPassword,
			                                      Settings.MySqlSchemaName);
			p.Start();

			using (var streamReader = new StreamReader(filePath))
			{
				p.StandardInput.Write(streamReader.ReadToEnd());
				p.StandardInput.Flush();
			}

			p.Close();
		}
		
		
		void IDisposable.Dispose()
		{
			
		}
	}
}
