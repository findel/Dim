using System;
using System.Diagnostics;
using System.IO;

namespace Dim
{
	public class DatabaseCommander : IDisposable
	{
		public DatabaseCommander(){}
		
		#region Dump Script Methods
		
		public void DumpBackup(string filePath)
		{
			this.Dump(filePath, "--routines --comments");
		}
		
		public void DumpStructure(string filePath)
		{
			this.Dump(filePath, "--no-data");
		}
		
		public void DumpData(string filePath)
		{
			this.Dump(filePath, "--no-create-info");
		}
		
		public void DumpRoutines(string filePath)
		{
			this.Dump(filePath, "--no-data --no-create-info --routines --comments");
		}
		
		private void Dump(string filePath, string options)
		{
			var p = new Process();
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardInput = true;
			p.StartInfo.FileName = Settings.MySqlBinPath + @"\mysqldump.exe";
			p.StartInfo.Arguments = string.Format(@"-u{0} -p{1} {2} {3}", Settings.MySqlUserName, Settings.MySqlPassword, options, Settings.MySqlSchemaName);
			p.Start();
			
			File.WriteAllText(filePath, p.StandardOutput.ReadToEnd());
			
			p.Close();
		}
		
		#endregion
		
		#region Run Script Methods
		
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
		
		#endregion
		
		void IDisposable.Dispose()
		{
			
		}
	}
}
