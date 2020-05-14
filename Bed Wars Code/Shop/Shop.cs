using Bed_Wars_Code.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemAmountKV = System.Collections.Generic.KeyValuePair<Bed_Wars_Code.Shop.ItemType, int>;

namespace Bed_Wars_Code.Shop
{
	internal static class Shop
	{
		public static readonly Dictionary<ItemAmountKV, GeneratorResouces> ItemCosts = new Dictionary<ItemAmountKV, GeneratorResouces>()
		{
			{ new ItemAmountKV(ItemType.BLOCKS, 16), new GeneratorResouces(4, 0, 0, 0) },
			{ new ItemAmountKV(ItemType.STONE_SWORD, 1), new GeneratorResouces(10, 0, 0, 0) },
			{ new ItemAmountKV(ItemType.IRON_SWORD, 1), new GeneratorResouces(0, 7, 0, 0) },
			{ new ItemAmountKV(ItemType.DIAMOND_SWORD, 1), new GeneratorResouces(0, 0, 0, 4) },
			{ new ItemAmountKV(ItemType.ENDER_PEARL, 1), new GeneratorResouces(0, 0, 0, 4) },
		};

		public static bool BuyItems(ItemType type, ref Player p)
		{
			var kv = ItemCosts.ToList().Find(x => x.Key.Key == type);
			if (p.Inventory.HasItems(kv.Value))
			{
				p.Inventory.TakeItems(kv.Value);
				p.Inventory.GiveItem(type, kv.Key.Value);
				return true;
			}
			else
			{
				return false;
			}
		}

	}
}
