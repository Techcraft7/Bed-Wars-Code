using System;
using System.Linq;
using System.Collections.Generic;

namespace Bed_Wars_Code
{
	internal class Game
	{
		public bool Running { get; private set; }
		public Player CurrentPlayer
		{
			get => Running ? Players[TurnIndex] : null;
			private set => CurrentPlayer = value;
		}
		private readonly List<Player> Players;
		public readonly Map map;
		private int turnIndex = 0;
		public int TurnIndex
		{
			get => turnIndex % Players.Count;
		}

		public Game(List<Player> players)
		{
			Players = players ?? throw new ArgumentNullException(nameof(players));
			Utils.ThrowIfAnyNull(players);
			map = new Map(players.Count, players);
			Running = false;
		}

		public void Start()
		{
			Running = true;
			while (Running)
			{
				AdvanceTurn();
			}
		}

		public void AdvanceTurn()
		{
			Console.Clear();
			map.TickGenerators();
			if (!Players[TurnIndex].IsDead)
			{
				Players[TurnIndex].ExecuteTurn(this);
				Console.WriteLine("Press any key to continue...");
				_ = Console.ReadKey(true);
			}
			turnIndex++;
		}
	}
}