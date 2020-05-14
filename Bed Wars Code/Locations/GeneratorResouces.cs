using System;

namespace Bed_Wars_Code.Locations
{
	internal class GeneratorResouces
	{
		public int Iron { get; private set; }
		public int Gold { get; private set; }
		public int Diamond { get; private set; }
		public int Emerald { get; private set; }

		public void AddResouces(ResourceType type, int amount)
		{
			switch (type)
			{
				case ResourceType.IRON:
					Iron += amount;
					break;
				case ResourceType.GOLD:
					Gold += amount;
					break;
				case ResourceType.DIAMOND:
					Diamond += amount;
					break;
				case ResourceType.EMERALD:
					Emerald += amount;
					break;
			}
		}

		public void SetResources(ResourceType type, int value)
		{
			switch (type)
			{
				case ResourceType.IRON:
					Iron = value;
					break;
				case ResourceType.GOLD:
					Gold = value;
					break;
				case ResourceType.DIAMOND:
					Diamond = value;
					break;
				case ResourceType.EMERALD:
					Emerald = value;
					break;
			}
		}

		public void Add(GeneratorResouces r)
		{
			Iron += r.Iron;
			Gold += r.Gold;
			Diamond += r.Diamond;
			Emerald += r.Emerald;
		}

		public void Reset()
		{
			Iron = Gold = Diamond = Emerald = 0;
		}

		public void Collect(Player player)
		{
			GeneratorResouces r = this;
			player.GiveResources(r);
			Reset();
		}
	}
}