using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;
using Bed_Wars_Code;
using Techcraft7_DLL_Pack;
using CCM = Techcraft7_DLL_Pack.ColorConsoleMethods;
namespace Bed_Wars_Code
{
	public class Game
	{		
		public Map BWMap;

		public bool Running = false;
		
		public int IndexOfTurn = 0;
		
		public List<Team> teams;

		public List<Player> Players = new List<Player>();

		public Game(List<Player> Players, Map map, List<Team> teams)
		{
			this.teams = teams;
			this.Players = Players;
			this.BWMap = map;
		}
		
		public void Start()
		{
			Running = true;
		Retry:
			for (int index = 0; index < Players.Count; index = index)
			{
				Player p = Players[index];
				#if DEBUG
				Console.WriteLine("testing player " + p.Name);
				#endif
				foreach (Location[] row in BWMap.LocCoords)
				{
					foreach (Location i in row)
					{
						#if DEBUG
						Console.WriteLine("testing " + i.name);
						#endif
						if (i.GetType() == typeof(Base))
						{
							Base b = (Base)i.map.GetLocationByCoords(i.Coords[0], i.Coords[1]);
							#if DEBUG
							Console.WriteLine(i.name + " is a base");
							Console.WriteLine("p.isatbase = {0}", p.IsAtBase);
							Console.WriteLine("p.team = {0}", p.CurrentTeam.Name);
							Console.WriteLine("b.team = {0}", b.Team.Name);
							#endif
							if (b.Team.Name == p.CurrentTeam.Name && p.IsAtBase == false)
							{
								p.loc = i;
								p.IsAtBase = true;
								#if DEBUG
								Utils.PrintPlayerNameWithFormattingPlusMoreText(p, " is at " + b.Team.DisplayColor + " base");
								#endif
								break;
							}
						}
					}
				}
				index++;
			}
			foreach (Player pl in Players)
			{
				if (!pl.IsAtBase)
				{
					goto Retry;
				}
			}
			foreach (Team t in teams)
			{
				if (t.Players.Count < 1)
				{
					t.Eliminated = true;
					t.SuppressEliminationMessage = true;
					t.Players.Add(new Player("NULL"));
				}
			}
		}
		
		public Location GetPlayerLoctionByIndex(int index)
		{
			return Players[index].loc;
		}
		
		public Player PlayerFromTurnIndex(int index)
		{
			return Players[index];
		}
		
		public void NextTurn()
		{
			IndexOfTurn = IndexOfTurn < Players.Count ? IndexOfTurn + 1 : 0;
			Console.WriteLine(IndexOfTurn);
			PlayerFromTurnIndex(IndexOfTurn).loc.ques.Ask(PlayerFromTurnIndex(IndexOfTurn));
			BWMap.Update();
		}
	}
}

