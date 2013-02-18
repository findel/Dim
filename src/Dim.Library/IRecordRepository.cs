using System;
using System.Collections.Generic;

namespace Dim.Library
{
	public interface IRecordRepository
	{
		DimRecord FindByFileName(string fileName);
		
		List<DimRecord> GetAll();
		
		void Save(DimRecord record);
	}
}
