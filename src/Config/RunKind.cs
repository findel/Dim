using System;
using System.Collections.Generic;
using System.IO;

namespace Dim.Config
{
	public enum RunKind
	{
		None = 0,
		RunOnce = 1,
		RunIfChanged = 2,
		RunAlways = 3
	}
}
