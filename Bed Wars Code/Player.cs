using System;
using System.Linq;
using System.Collections.Generic;
using Techcraft7_DLL_Pack;
using Bed_Wars_Code.Locations;
using Bed_Wars_Code.Inventory;

namespace Bed_Wars_Code
{
	internal class Player
	{
		public PlayerInventory Inventory { get; private set; }
		public Dictionary<UpgradeType, int> Upgrades = new Dictionary<UpgradeType, int>();
		public readonly string Name;
		public readonly ConsoleColor Color;
		public Tuple<int, int> Coords { get; set; }
		public bool IsDead { get; set; }

		public Player(string name, ConsoleColor color)
		{
			Name = name;
			Color = color;
			Inventory = new PlayerInventory();
		}

		public void SetCoords(int x, int y)
		{
			Coords = new Tuple<int, int>(x, y);
		}

		public void WriteToConsole()
		{
			ColorConsoleMethods.WriteColor($"[{Color.ToString().ToUpper()}] {Name}", Color);
		}

		public override string ToString() => $"Player {{ Name: {Name} Color: {Color} }}";

		public void ExecuteTurn(Game game)
		{
			Utils.PrintPlayerNameInText("%PLAYER%, your turn!\n", this);
			Location l = game.map.GetPlayerLocation(game.CurrentPlayer);
			if (l != null)
			{
				l.ExecuteTurn(game);
			}
			else
			{
				ColorConsoleMethods.WriteLineColor("Player is at a null location! Sending to base!", ConsoleColor.Red);
				game.map.SendPlayerToSpawn(game.CurrentPlayer);
			}
		}

		public void GiveResources(GeneratorResouces res)
		{
			Inventory.GiveResources(res);
		}
	}
}