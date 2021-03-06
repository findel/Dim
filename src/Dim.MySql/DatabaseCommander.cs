﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;

using MySql.Data.MySqlClient;
using Dim.Library;

namespace Dim.MySql
{
	public class DatabaseCommander : IDisposable, IDatabaseCommander
	{
		public DatabaseCommander(){}
		
		#region Fields
		
		private static string _MySqlConnectionString;
		
		private string _MySqlCommandlineString;
		
		#endregion
		
		#region Properties
		
		public static string MySqlConnectionString
		{
			get
			{
				if(string.IsNullOrEmpty(_MySqlConnectionString))
					_MySqlConnectionString = string.Format("server={0};port={1};uid={2};pwd={3};database={4};",
					                                       Local.ConfigFile.Host,
					                                       Local.ConfigFile.Port,
					                                       Local.ConfigFile.Username,
					                                       Local.ConfigFile.Password,
					                                       Local.ConfigFile.Schema);
				return _MySqlConnectionString;
			}
		}
		
		public string MySqlCommandlineString
		{
			get
			{
				if(string.IsNullOrEmpty(_MySqlCommandlineString))
					_MySqlCommandlineString = string.Format(@"-h{0} -P{1} -u{2} -p{3}",
			                                      Local.ConfigFile.Host,
			                                      Local.ConfigFile.Port,
			                                      Local.ConfigFile.Username,
			                                      Local.ConfigFile.Password);
				return _MySqlCommandlineString;
			}
		}
		
		#endregion
		
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
			p.StartInfo.FileName = Local.ConfigFile.MySqlPath + @"\mysqldump.exe";
			p.StartInfo.Arguments = string.Format(@"{0} {1} {2}", this.MySqlCommandlineString, options, Local.ConfigFile.Schema);
			p.Start();
			
			File.WriteAllText(filePath, p.StandardOutput.ReadToEnd());
			
			p.Close();
		}
		
		#endregion
		
		#region Run Script Methods
		
		public void RunFile(string filePath)
		{
			using (var streamReader = new StreamReader(filePath))
			{
				this.Execute(streamReader.ReadToEnd());
			}
		}
		
		public void RunCreateDimLog()
		{
			if(!this.DimLogExists())
				this.Execute(GetFromResources("DatabaseCreateTable.txt"));
		}
		
		public void Execute(string sql)
		{
			var p = new Process();
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardInput = true;
			p.StartInfo.FileName = Local.ConfigFile.MySqlPath + "\\mysql.exe";
			p.StartInfo.Arguments = string.Format("{0} {1}", this.MySqlCommandlineString, Local.ConfigFile.Schema);
			p.Start();
			p.StandardInput.Write(sql);
			p.StandardInput.Flush();
			p.Close();
		}
		
		private static string GetFromResources(string resourceName)
		{
			resourceName = "Dim.Library." + resourceName;
			Assembly assem = Assembly.GetExecutingAssembly();
			using(Stream stream = assem.GetManifestResourceStream(resourceName))
			{
				using(StreamReader reader = new StreamReader(stream))
				{
					return reader.ReadToEnd();
				}
			}
		}
		
		#endregion
		
		public bool DimLogExists()
		{
			DataTable dataTable = new DataTable();
			MySqlDataAdapter adapter = new MySqlDataAdapter("SHOW TABLES LIKE 'dimfiles'", MySqlConnectionString);
			adapter.Fill(dataTable);
			return dataTable.Rows.Count > 0;
		}
		
		public bool IsConnectionOkay()
		{
			var okay = true;
			try 
			{
				MySqlConnection conn = new MySqlConnection(MySqlConnectionString);
				conn.Open();
			}
			catch (MySqlException ex)
			{
				okay = false;
				this.MySqlException = ex;
			}
			return okay;
		}
		
		public MySqlException MySqlException { get; set; }
		
		public string ExceptionMessage 
		{
			get
			{
				return this.MySqlException != null ? MySqlException.Message : "";
			}
		}
		
		void IDisposable.Dispose()
		{
			
		}
	}
}
