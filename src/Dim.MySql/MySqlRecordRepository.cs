using System;
using System.Collections.Generic;
using Dim.Library;

namespace Dim.MySql
{
	/// <summary>
	/// Implementation of the IRecordRepository for the MySql database.
	/// </summary>
	public class MySqlRecordRepository : IRecordRepository
	{
		public MySqlRecordRepository()
		{
			this.connectionString = string.Format("server={0};port={1};uid={2};pwd={3};database={4};",
			                                 Local.ConfigFile.Host,
			                                 Local.ConfigFile.Port,
			                                 Local.ConfigFile.Username,
			                                 Local.ConfigFile.Password,
			                                 Local.ConfigFile.Schema);
			
			this.database = Simple.Data.Database.OpenConnection(this.connectionString);
		}
		
		private string connectionString;
		private dynamic database;
		
		public DimRecord FindByFileName(string fileName)
		{
			DimRecord record = this.database.DimFiles.FindByFileName(fileName);
			return record;
		}
		
		public List<DimRecord> GetAll()
		{
			List<DimRecord> records = this.database.DimFiles.All();
			return records;
		}
		
		public void Save(DimRecord record)
		{
			if(record.Id == 0)
				this.database.DimFiles.Insert(record);
			else
				this.database.DimFiles.Update(record);
		}
	}
}
