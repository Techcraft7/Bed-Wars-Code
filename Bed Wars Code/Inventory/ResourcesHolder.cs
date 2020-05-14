using Bed_Wars_Code.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bed_Wars_Code.Inventory
{
	internal class ResourcesHolder
	{
		private GeneratorResouces resources = new GeneratorResouces();
		public int Iron { get => resources.Iron; set => resources.SetResources(ResourceType.IRON, value); }
		public int Gold { get => resources.Gold; set => resources.SetResources(ResourceType.GOLD, value); }
		public int Diamond { get => resources.Diamond; set => resources.SetResources(ResourceType.DIAMOND, value); }
		public int Emerald { get => resources.Emerald; set => resources.SetResources(ResourceType.EMERALD, value); }
		public void GiveResources(GeneratorResouces resources)
		{
			resources.Add(ref this.resources);
		}

		public bool HasItems(GeneratorResouces other)
		{
			return Iron >= other.Iron &&
				Gold >= other.Gold &&
				Diamond >= other.Diamond &&
				Emerald >= other.Emerald;
		}

		public void TakeItems(GeneratorResouces other)
		{
			Iron -= other.Iron;
			Gold -= other.Gold;
			Diamond -= other.Diamond;
			Emerald -= other.Emerald;
		}

		public override string ToString() => resources.ToString();
		public void Reset() => resources.Reset();
	}
}
