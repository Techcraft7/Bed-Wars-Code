using Bed_Wars_Code.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bed_Wars_Code.Inventory
{
	internal class PlayerInventory : ResourcesHolder
	{
		public SwordType Sword { get; private set; }
		public int Blocks { get; private set; }
	}
}
