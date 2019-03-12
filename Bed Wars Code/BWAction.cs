using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;
using Bed_Wars_Code;
using Techcraft7_DLL_Pack;
using CCM = Techcraft7_DLL_Pack.ColorConsoleMethods;
namespace Bed_Wars_Code
{
	public class BWAction
	{
		public readonly string alias = "";

		public Action action;

		public BWAction(string name, Action action)
		{
			this.alias = name;
			this.action = action;
		}

		public void Execute()
		{
			action.Invoke();
		}
	}
}

