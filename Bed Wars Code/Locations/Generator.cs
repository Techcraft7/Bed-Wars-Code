using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techcraft7_DLL_Pack;

namespace Bed_Wars_Code.Locations
{
	using static ConsoleColor;
	using static ColorConsoleMethods;
	internal class Generator : Location
	{
		private const int GOLD_TIME = 4;
		private const int DIAMOND_TIME = 16;
		private const int EMERALD_TIME = 3;
		public readonly GeneratorType type;
		private GeneratorResouces resources = new GeneratorResouces();
		private int counter = 0;
		public int Upgrade { get; private set; }
		private bool isEmeraldForge;
		public bool IsEmeraldForge
		{
			get => isEmeraldForge && type == GeneratorType.TEAM; set => isEmeraldForge = value && type == GeneratorType.TEAM;
		}

		public Generator(GeneratorType type)
		{
			this.type = type;
			Upgrade = 1;
		}

		public override void GeneratorTick()
		{
			switch (type)
			{
				case GeneratorType.TEAM:
					resources.AddResouces(ResourceType.IRON, 1);
					if (counter % Math.Max(GOLD_TIME - Upgrade, 0) == 0)
					{
						resources.AddResouces(ResourceType.GOLD, 1);
						if (!isEmeraldForge)
						{
							counter = 0;
						}
					}
					if (counter % Math.Max(EMERALD_TIME - Upgrade, 0) == 0 && isEmeraldForge)
					{
						resources.AddResouces(ResourceType.EMERALD, 1);
						counter = 0;
					}
					break;
				case GeneratorType.DIAMOND:
					if (counter % Math.Max(DIAMOND_TIME / Upgrade, 0) == 0)
					{
						resources.AddResouces(ResourceType.GOLD, 1);
						counter = 0;
					}
					break;
				case GeneratorType.EMERALD:
					if (counter % Math.Max(EMERALD_TIME / Upgrade, 0) == 0)
					{
						resources.AddResouces(ResourceType.GOLD, 1);
						counter = 0;
					}
					break;
			}
			counter++;
		}

		public override KeyValuePair<char, ConsoleColor> GetMapSymbol()
		{

			ConsoleColor color = ConsoleColor.White;
			switch (type)
			{
				case GeneratorType.DIAMOND:
					color = ConsoleColor.Cyan;
					break;
				case GeneratorType.EMERALD:
					color = ConsoleColor.Green;
					break;
			}
			return new KeyValuePair<char, ConsoleColor>(type.ToString()[0], color);
		}

		public override List<KeyValuePair<string, Action<Game>>> GetOptions()
		{
			var list = base.GetOptions();
			list.Add(new KeyValuePair<string, Action<Game>>("Collect", new Action<Game>(CollectResources)));
			return list;
		}

		public void CollectResources(Game game)
		{
			WriteLineMultiColor(
				new string[] { "You collected: ", $"{resources.Iron} Iron"},
				new ConsoleColor[] { White, White }
			);
			WriteLineMultiColor(
				new string[] { "You collected: ", $"{resources.Gold} Gold" },
				new ConsoleColor[] { White, Yellow }
			);
			WriteLineMultiColor(
				new string[] { "You collected: ", $"{resources.Diamond} Diamond" },
				new ConsoleColor[] { White, Cyan }
			);
			WriteLineMultiColor(
				new string[] { "You collected: ", $"{resources.Emerald} Emerald" },
				new ConsoleColor[] { White, Green }
			);
			resources.Collect(game.CurrentPlayer);
		}

		public override string ToString() => $"Generator: {type}";
	}
}
