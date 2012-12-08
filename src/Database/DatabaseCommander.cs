using System;
using System.Diagnostics;
using System.IO;
using Dim.Config;

namespace Dim.Database
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
			p.StartInfo.Arguments = string.Format(@"-h{0} -P{1} -u{2} -p{3} {4} {5}",
			                                      Local.ConfigFile.Host,
			                                      Local.ConfigFile.Port,
			                                      Local.ConfigFile.Username,
			                                      Local.ConfigFile.Password,
			                                      options, 
			                                      Local.ConfigFile.Schema);
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
			p.StartInfo.Arguments = string.Format("-h{0} -P{1} -u{2} -p{3} {4}",
			                                      Local.ConfigFile.Host,
			                                      Local.ConfigFile.Port,
			                                      Local.ConfigFile.Username,
			                                      Local.ConfigFile.Password,
			                                      Local.ConfigFile.Schema);
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
