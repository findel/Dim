using System;
using System.Diagnostics;
using Dim.Library;

namespace Dim.MySql
{
	/// <summary>
	/// Implementation of the IDatabaseManager for the MySql database.
	/// </summary>
	public class MySqlDatabaseManager : IDatabaseManager
	{
		public MySqlDatabaseManager()
		{
			commandLineString = string.Format(@"-h{0} -P{1} -u{2} -p{3}",
			                                      Local.ConfigFile.Host,
			                                      Local.ConfigFile.Port,
			                                      Local.ConfigFile.Username,
			                                      Local.ConfigFile.Password);
		}
		
		private string commandLineString;
		
		public void Execute(string sql)
		{
			string arguments = string.Format("{0} --comments {1}", this.commandLineString, Local.ConfigFile.Schema);
			
			MySqlExecute(arguments, sql);
		}
		
		public string DumpSchema()
		{
			var p = new Process();
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardInput = true;
			p.StartInfo.FileName = Local.ConfigFile.MySqlPath + @"\mysqldump.exe";
			p.StartInfo.Arguments = string.Format(@"{0} --routines --comments {1}", this.commandLineString, Local.ConfigFile.Schema);
			p.Start();
			
			string dump = p.StandardOutput.ReadToEnd();
			
			p.Close();
			
			return dump;
		}
		
		public void CreateSchema()
		{
			var sql = "DROP SCHEMA IF EXISTS `{0}`; CREATE SCHEMA `{0}`;";
			sql = string.Format(sql, Local.ConfigFile.Schema);
			
			var arguments = this.commandLineString;
			
			MySqlExecute(arguments, sql);
		}
		
		private void MySqlExecute(string arguments, string sql)
		{
			ProcessExecute("mysql.exe", arguments, sql);
		}
		
		private void ProcessExecute(string fileName, string arguments, string input)
		{
			var p = new Process();
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardInput = true;
			p.StartInfo.FileName  = Local.ConfigFile.MySqlPath + "\\" + fileName;
			p.StartInfo.Arguments = arguments;
			p.Start();
			p.StandardInput.Write(input);
			p.StandardInput.Flush();
			p.StandardInput.Close();
			p.WaitForExit();
			p.Close();
		}
	}
}
