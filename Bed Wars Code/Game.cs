using System;
using System.Linq;
using System.Collections.Generic;
using Bed_Wars_Code.Combat;

namespace Bed_Wars_Code
{
	internal class Game
	{
		public bool Running { get; private set; }
		public bool InCombat { get; private set; }
		public Player CurrentPlayer
		{
			get => Running ? Players[TurnIndex] : null;
			private set => CurrentPlayer = value;
		}
		public readonly List<Player> Players;
		public Map Map { get; private set; }
		private int turnIndex = 0;
		public int TurnIndex
		{
			get => turnIndex % Players.Count;
		}

		public Game(List<Player> players)
		{
			Players = players ?? throw new ArgumentNullException(nameof(players));
			Utils.ThrowIfAnyNull(players);
			Map = new Map(players.Count, players);
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
			if (Players.Where(x => !x.IsDead).Count() == 1)
			{
				Console.ForegroundColor = ConsoleColor.DarkYellow;
				Utils.PrintPlayerNameInText("%PLAYER% wins!", Players.Where(x => !x.IsDead).First());
				Console.ForegroundColor = ConsoleColor.Gray;
				Running = false;
				return;
			}
			Map.TickGenerators();
			if (!Players[TurnIndex].IsDead)
			{
				Players[TurnIndex].ExecuteTurn(this);
				Console.WriteLine("Press any key to continue...");
				_ = Console.ReadKey(true);
			}
			turnIndex++;
		}

		public void StartCombat(ref Player other)
		{
			InCombat = true;
			Player v = CurrentPlayer;
			Map m = Map;
			CombatEngine combat = new CombatEngine(ref v, ref other, ref m);
			while (InCombat)
			{
				combat.Advance(this);
			}
		}
	}
}