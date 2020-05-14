using Bed_Wars_Code.Locations;
using Bed_Wars_Code.Shop;
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
		public int EnderPearls { get; private set; }
		public int Blocks { get; private set; }

		public override string ToString() => $"{base.ToString()} Blocks: {Blocks} Sword: {Sword.ToString().ToLower()}";

		public void BuildBridge()
		{
			if (Blocks >= Location.BRIDGE_DISTANCE)
			{
				Blocks -= Location.BRIDGE_DISTANCE;
			}
			else
			{
				throw new InvalidOperationException("Not enough blocks!");
			}
		}

		public void GiveItem(ItemType type, int amount)
		{
			switch (type)
			{
				case ItemType.BLOCKS:
					Blocks += amount;
					break;
				case ItemType.STONE_SWORD:
					Sword = SwordType.STONE;
					break;
				case ItemType.IRON_SWORD:
					Sword = SwordType.IRON;
					break;
				case ItemType.DIAMOND_SWORD:
					Sword = SwordType.DIAMOND;
					break;
				case ItemType.ENDER_PEARL:
					EnderPearls += amount;
					break;

			}
		}

		public void Clear()
		{
			Blocks = EnderPearls = 0;
			Sword = SwordType.WOODEN;
			Reset();
		}
	}
}
