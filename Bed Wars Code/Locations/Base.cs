using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bed_Wars_Code.Locations
{
	internal class Base : Generator
	{
		public bool IsDead { get; private set; }
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

		public override List<KeyValuePair<string, Action<Game>>> GetOptions()
		{
			var list = base.GetOptions();
			list.Add(new KeyValuePair<string, Action<Game>>("Buy", new Action<Game>(BuyThings)));
			return list;
		}

		private void BuyThings(Game game)
		{
			throw new NotImplementedException();
		}

		public override string ToString() => $"Team Base: {Color}";
	}
}
