using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bed_Wars_Code.Combat
{
	using Bed_Wars_Code.Locations;
	using static Console;
	using static Techcraft7_DLL_Pack.ColorConsoleMethods;
	internal class CombatEngine
	{
		public static readonly List<string[]> ESCAPE_MESSAGES = new List<string[]>
		{
			new string[] { "%PLAYER% failed to escape from ", "%PLAYER%\n" },
			new string[] { "%PLAYER% just bearly escaped!\n" },
			new string[] { "%PLAYER% escaped!\n" },
			new string[] { "%PLAYER% missed ", "%PLAYER% by a mile!\n" }
		};
		public static readonly List<string[]> KILL_MESSAGES = new List<string[]>
		{
			new string[] { "%PLAYER% was stabbed in the back by ", "%PLAYER%\n" },
			new string[] { "%PLAYER% defeated his opponent!\n" },
			new string[] { "%PLAYER% won the battle!\n" },
			new string[] { "%PLAYER% was backstabbed by ", "%PLAYER%!\n" }
		};


		private readonly Random rng = new Random();
		private readonly Player p1;
		private readonly Player p2;
		private readonly Map m;
		public bool Running { get; private set; }
		private int counter = 0;
		public int TurnIndex
		{
			get => counter % 2;
			set => counter = value;
		}

		public CombatEngine(ref Player p1, ref Player p2, ref Map m)
		{
			this.p1 = p1;
			this.p2 = p2;
			this.m = m;
			Running = true;
		}

		public List<KeyValuePair<string, Func<Game, bool>>> GetOptions()
		{
			var list = new List<KeyValuePair<string, Func<Game, bool>>>
			{
				new KeyValuePair<string, Func<Game, bool>>("Attack", new Func<Game, bool>(Attack))
			};
			//add options for base
			list.AddRange(m.GetPlayerLocation(TurnIndex == 0 ? p1 : p2).GetOptions());
			//Remove "Leave"
			list.RemoveAll(x => x.Key.ToLower() == "leave");
			//replace with "Escape"
			list.Add(new KeyValuePair<string, Func<Game, bool>>("Escape", new Func<Game, bool>(Escape)));
			return list;
		}

		public static void PrintCombatMessages(Player killed, Player killer, int roll, List<string[]> messages)
		{
			switch (roll)
			{
				case 1:
				case 2:
					Utils.PrintPlayerNameInText(messages[0][0], killed);
					Utils.PrintPlayerNameInText(messages[0][1], killer);
					break;
				case 3:
					Utils.PrintPlayerNameInText(messages[1][0], killed);
					break;
				case 4:
					Utils.PrintPlayerNameInText(messages[2][0], killed);
					break;
				case 5:
				case 6:
					Utils.PrintPlayerNameInText(messages[3][0], killer);
					Utils.PrintPlayerNameInText(messages[3][1], killed);
					break;
				default:
					Utils.PrintPlayerNameInText("%PLAYER% was killed!\n", killed);
					break;
			}
		}

		public bool Escape(Game game)
		{
			int n = 1;
			WriteLine("Choose a place to escape to!");
			//execute escape routine
			Player escapee = TurnIndex == 0 ? p1 : p2;
			bool noPath = !m.GetPlayerLocation(escapee).GetOptions().Find(x => x.Key.ToLower() == "leave").Value.Invoke(game);
			if (!noPath)
			{
				WriteLineColor($"Rolling to escape...", ConsoleColor.Green);
				ReadLine();
				n = rng.Next(6) + 1;
				WriteLine($"You rolled: {n}");
			}
			else
			{
				WriteLineColor("You could not escape that way!", ConsoleColor.Red);
			}
			PrintCombatMessages(escapee, TurnIndex == 0 ? p2 : p1, n, ESCAPE_MESSAGES);
			if (noPath)
			{
				escapee.Inventory.Reset();
				m.SendPlayerToSpawn(escapee);
				Base b = m.GetPlayerBase(escapee);
				//print final kill message
				if (!b.HasBed)
				{
					ForegroundColor = ConsoleColor.DarkGray;
					Utils.PrintPlayerNameInText("%PLAYER% was slain! ", escapee);
					WriteLineColor("FINAL KILL!", ConsoleColor.Cyan);
					ForegroundColor = ConsoleColor.Gray;
					escapee.IsDead = true;
					b.IsDead = true;
				}
			}
			return true;
		}

		private bool Attack(Game game)
		{
			Player attacker = game.CurrentPlayer.Name == p1.Name ? p1 : p2;

			return true;
		}

		public void Advance(Game game)
		{
			var opts = GetOptions();
			Player[] players = new Player[2] { p1, p2 };
			bool res = false;
			do
			{
				if (!res)
				{
					Utils.PrintPlayerNameInText("%PLAYER%, your turn to FIGHT!\n", players[TurnIndex]);
					WriteLineColor("---------- COMBAT ----------", ConsoleColor.Magenta);
					for (int i = 0; i < opts.Count; i++)
					{
						WriteLineColor($"[{i}] {opts[i].Key}", ConsoleColor.Yellow);
					}
					WriteLineColor("----------------------------", ConsoleColor.Magenta);
				}
				int opt = Program.ReadInt("Pick an option: ", 0, opts.Count);
				res = opts[opt].Value.Invoke(game);
				if (res)
				{
					WriteLineColor("Press a key to continue", ConsoleColor.Cyan);
					_ = ReadKey(true);
					break;
				}
			}
			while (!res);
			game.Map.TickGenerators(); //generate resources while in combat
			counter++;
		}
	}
}
