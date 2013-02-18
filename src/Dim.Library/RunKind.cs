using System;
using System.Collections.Generic;
using System.IO;

namespace Dim.Library
{
	public enum RunKind
	{
		None = 0,
		RunOnce = 1,
		RunIfChanged = 2,
		RunAlways = 3
	}
}
