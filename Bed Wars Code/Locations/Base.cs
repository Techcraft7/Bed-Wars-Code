using Bed_Wars_Code.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bed_Wars_Code.Locations
{
	using static Techcraft7_DLL_Pack.ColorConsoleMethods;
	internal class Base : Generator
	{
		public bool IsDead { get; set; }
		private bool hasBed;
		public bool HasBed
		{
			get => hasBed && !IsDead; //You cannot have a bed if you're dead!
			set => hasBed = value;
		}

		public readonly Player player;
		public ConsoleColor Color { get => player.Color; }

		public Base(Player p) : base(GeneratorType.TEAM)
		{
			IsDead = p == null;
			player = p;
		}

		public override KeyValuePair<char, ConsoleColor> GetMapSymbol()
		{
			if (player == null)
			{
				return base.GetMapSymbol();
			}
			return new KeyValuePair<char, ConsoleColor>('T', player.Color);
		}

		public override List<KeyValuePair<string, Func<Game, bool>>> GetOptions()
		{
			var list = base.GetOptions();
			list.Add(new KeyValuePair<string, Func<Game, bool>>("Buy", new Func<Game, bool>(BuyThings)));
			list.Add(new KeyValuePair<string, Func<Game, bool>>("Break Bed", new Func<Game, bool>(BreakBed)));
			return list;
		}

		private bool BreakBed(Game game)
		{
			if (game.Map.GetPlayerLocation(game.CurrentPlayer).GetType() != typeof(Base))
			{
				throw new InvalidOperationException("You cant break a bed if it is not a base!");
			}
			//check if player is trying to break their own bed
			if (game.CurrentPlayer.Color == (game.Map.GetPlayerLocation(game.CurrentPlayer) as Base).Color)
			{
				WriteLineColor("You cannot break your own bed!", ConsoleColor.Red);
				return false;
			}
			//just try to break the bed if in combat!
			if (!game.InCombat)
			{
				
			}
			else //otherwise roll to break!
			{

			}
			return true;
		}

		private bool BuyThings(Game game)
		{
			var vals = Enum.GetValues(typeof(ItemType));
			int item = -1;
			//big linq stuff
			var amount = Shop.Shop.ItemCosts
				.Select(x => new KeyValuePair<ItemType, KeyValuePair<int, GeneratorResouces>>(
					x.Key.Key,
					new KeyValuePair<int, GeneratorResouces>(x.Key.Value, x.Value))
			).ToDictionary(x => x.Key, x => x.Value);
			for (int i = 0; i < vals.Length; i++)
			{
				ItemType t = (ItemType)vals.GetValue(i);
				Console.WriteLine($"[{i}] {Utils.FormatEnumByUnderscores(t)} - {amount[t].Value}");
			}
			do
			{
				item = Program.ReadInt("Enter -1 to exit\nPick a to buy an item: ", -1, vals.Length - 1);
				if (item == -1)
				{
					break;
				}
				ItemType type = (ItemType)item;
				Player p = game.CurrentPlayer;
				if (Shop.Shop.BuyItems(type, ref p))
				{
					Techcraft7_DLL_Pack.ColorConsoleMethods.WriteLineColor($"You bought: {Utils.FormatEnumByUnderscores(type)} x {amount[type].Key}", ConsoleColor.Green);
					p.DrawInventory();
				}
				else
				{
					Techcraft7_DLL_Pack.ColorConsoleMethods.WriteLineColor($"You dont have enough resources!", ConsoleColor.Red);
				}
			}
			while (item > -1);
			return true;
		}

		public override string ToString() => $"Team Base: {Color}";
	}
}
